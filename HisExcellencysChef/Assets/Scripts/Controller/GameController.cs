/*
 * Author(s): Isaiah Mann
 * Description: Controls the game logic
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : SingletonController<GameController> {
	public int Level;
	public GameObject[] Underlings;
	public GameObject[] Ingredients;
	public InventoryUI inventory;
	public string[] level0Obsoletes;
	public bool tutorial0Part1;
	public bool tutorial0Part2;
	public bool tutorial0Part22;
	public bool tutorial0Part3;

	public bool tutorial1Part1;
	public bool tutorial1Part2;
	public bool tutorial1Part3;
	public bool checkSpinach;
	public bool spinachTasted;
	public bool tutorial1Part4;
	public bool tutorial1Part5;
	public bool tutorial1Part6;

	public float course1time;
	public float course2time;
	public float course3time;
	public float levelTime;

	[SerializeField]
	bool playMusicOnStart = true;
	CookingController cooking;

	protected override void FetchReferences () {
		base.FetchReferences ();
		if (playMusicOnStart) {
			playMusic();
		}
		cooking = CookingController.Instance;
		chef = GameObject.Find ("Cook").GetComponent<CharacterMovement>();
		time = levelTime;
		if (Level == 0) {
			LadyDialogue.CharacterSpeak (@"“You there! You have helped in the kitchen before, have you not? We have a small emergency.”

“Count Philip, my husband, will be returning from the Duke’s manor much sooner than planned, and with news. He will be expecting a meal, but we are short on time. You are our only servant currently here who who knows their way around a kitchen.”

“He’s requested Boulettes. I’ve left some chicken in the Mortar for you. Click on the Mortar and choose a process to begin cooking.");			
		} else if (Level == 1) {
			LadyDialogue.CharacterSpeak(@"I have a plan and you're it’s key to its success. In a weeks time [Important Guest], the kings advisor/brother, will be passing through.  I intend to throw a feast such that he will be singing our praise high and low and to ear of the king himself.
				
			Until then you need to practice. You're a natural talent, but could do with some instruction.  We’ll make a good start of it today. I’ve hired some underlings for you.  Let us start with an exotically themed spinach salad with roasted Almonds. I’ve given some Almonds to your underling, so let us practice working with others.");
		} else if (Level == 2) {

		} else if (Level == 3) {

		} else {
			LadyDialogue.CharacterSpeak (@"Hello, young cook!

To select a primary ingredient, click on a character, then click on the Pantry Door.

To cook that ingredient, click a station and select ""Put Down"" in the drop down menu, then select it again and pick the cooking process you want and wait for the character to pick it up again.

When you want to finish a dish, select the character carrying it and click on the gray square below.

Ingredients are Hot or Cold and Moist or Dry. Try to balance the flavors across your dishes.");
			
		}
	}
		
	void playMusic () {
		EventController.Event(Event.GAME_MUSIC);
	}

	public CharacterMovement chef;
	public CharacterMovement underling1;

	public bool timer = false;
	public float time;
	public Text countdown;

	public bool canServe = false;
	public int course = 0;
	public Vector2 courseFlavor0;
	public Vector2 courseFlavor1;
	public Vector2 courseFlavor2;
	public GameObject endScreen;
	public Text score;

	public Sprite[] sliders;
	public Slider chefSlider;

	public float dialogueTime = 5f;
	public DialogueController LadyDialogue;
	public DialogueController TutorialDialogue;
	public GameObject UIDialogueBox;
	public Transform DishesPanel;
	public Button UIDishPrefab;
	public List<GameObject> activeDishes = new List<GameObject>();
	public List<GameObject> servedDishes = new List<GameObject>();

	//something something keep track of active ingredients for UI and Reset
	public void MakeUIDish(GameObject food){
		Button dish = (Button)Instantiate (UIDishPrefab, DishesPanel);
		dish.transform.localScale = Vector3.one;
		dish.GetComponent<Image> ().sprite = food.GetComponent<SpriteRenderer> ().sprite;
		dish.name = activeDishes.Count.ToString();
		activeDishes.Add (food);
		dish.onClick.AddListener (BannerOnClick);
		food.GetComponent<Ingredients> ().UIButton = dish.gameObject;

		//Make a child attached to each one to display other information and create a image update script to change image based on a target
	}

	public void RemoveUIDish(GameObject food){
		Destroy(food.GetComponent<Ingredients> ().UIButton);
		activeDishes.Remove (food);
		Destroy (food);
		//Make a child attached to each one to display other information and create a image update script to change image based on a target
	}

	public void MakeDialogueBox(string whatToSay){
		UIDialogueBox.SetActive (true);
		UIDialogueBox.GetComponent<DialogueController> ().CharacterSpeak (whatToSay);

		CancelInvoke ("CloseDialogue");
		Invoke ("CloseDialogue", dialogueTime);
	}

	public void NextTutorialStep(){
		if (tutorial0Part22) {
			MakeTutorialeBox ("He is well fed and satisfied.  It seems you weren’t all talk after all, thank you for your work.");
			tutorial0Part22 = false;
			tutorial0Part3 = true;
			Invoke ("NextTutorialStep", dialogueTime);
		} else if (tutorial0Part3) {
			time = 0;
			MakeTutorialeBox ("The rest of the household will be returning in a few hours. Please prepare at least two dishes to serve to us by then.");
		} else if (tutorial1Part1) {
			tutorial1Part1 = false;
			tutorial1Part2 = true;
			Invoke ("NextTutorialStep", dialogueTime);
		} else if (tutorial1Part2) {
			MakeTutorialeBox ("In the meantime, however, you might want to prepare some of the rest of the Salad. In keeping with its exotic theme, let us add some Rice. Fetch some Rice and cook it at the Range.");				
		} else if (tutorial1Part3) {
			MakeTutorialeBox ("I see you learned more than just the motions from old Jean. Excellent. Yes, those educated in the works of the Philosopher know that flavors are reducible to two essential qualities: Heat and Moisture. But enough of that for now. I am ready for this Salad when you are ready to serve it.");
			timerOn = true;
		} else if (tutorial1Part4) {
			timerOn = false;
			inventory.foods = new Inventory.InventoryNode[10];
			inventory.foods [0].obj = Ingredients [7];
			inventory.foods [0].quantity = -2;
			inventory.foods [1].obj = Ingredients [6];
			inventory.foods [1].quantity = 4;
			inventory.foods [2].obj = Ingredients [3];
			inventory.foods [2].quantity = 2;
			inventory.foods [3].obj = Ingredients [4];
			inventory.foods [3].quantity = 4;
			inventory.foods [4].obj = Ingredients [8];
			inventory.foods [4].quantity = 1;
			inventory.foods [5].obj = Ingredients [3];
			inventory.foods [5].quantity = 3;
			inventory.foods [6].obj = Ingredients [0];
			inventory.foods [6].quantity = 1;
			inventory.foods [7].obj = Ingredients [2];
			inventory.foods [7].quantity = 2;
			inventory.foods [8].obj = Ingredients [42];
			inventory.foods [8].quantity = 2;
			inventory.foods [9].obj = Ingredients [40];
			inventory.foods [9].quantity = 2;
		} else if (tutorial1Part5) {
			GameController.Instance.tutorial1Part5 = false;
			GameController.Instance.tutorial1Part6 = true;
			Invoke ("NextTutorialStep", 5f);
		} else if (tutorial1Part6) {
			MakeTutorialeBox ("We are almost done for today. I’ve hung another bell in your window. Please create for me four dishes by then. Each dish should be strongest in a different flavor.");
			time = 342f;
			course = 1;
			timerOn = true;
		}
	}

	public void MakeTutorialeBox(string whatToSay){
		TutorialDialogue.gameObject.SetActive (true);
		TutorialDialogue.CharacterSpeak (whatToSay);

		CancelInvoke ("CloseTutorial");
		Invoke ("CloseTutorial", dialogueTime);
	}

	void CloseDialogue(){
		UIDialogueBox.SetActive (false);
	}

	void CloseTutorial(){
		TutorialDialogue.gameObject.SetActive (false);
	}

	void BannerOnClick()
	{
		GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
		GameObject food = activeDishes[int.Parse(thisButton.name)];

//		GuestController guest = GetComponent<GuestController> ();
//		guest.GetGuest ("Count Philip");
//		guest.GetGuest ("Master Simon de Paris");
//		Debug.Log (guest.ParseTriggers(guest.guestList[0], food.GetComponent<Ingredients>()));
//		Debug.Log (guest.ParseTriggers(guest.guestList[1], food.GetComponent<Ingredients>()));
		//if food was served then display this information in a text box
//		Debug.Log (food.GetComponent<Ingredients>().primaryIngredientName);
//		Debug.Log (food.GetComponent<Ingredients> ().flavor);
	}

	public Ingredients currentlyCooking;
	public void EditSlider(string action, Ingredients ingredient){
		currentlyCooking = ingredient;
		chefSlider.gameObject.SetActive (true);
		if (cooking.GetProcess (action).YellowRed != 0) {
			chefSlider.maxValue = cooking.GetProcess (action).YellowRed;
		} else if (cooking.GetProcess (action).GreenYellow != 0) {
			chefSlider.maxValue = cooking.GetProcess (action).GreenYellow;
		} else if (cooking.GetProcess (action).DarkGreenGreen != 0) {
			chefSlider.maxValue = cooking.GetProcess (action).DarkGreenGreen;
		}

		foreach (Sprite bar in sliders) {
			if (bar.name == cooking.GetProcess (action).ProgressBarPNG) {
				chefSlider.GetComponentInChildren<Image>().sprite = bar;
			}
		}
	}

//	public List<GuestController> guests = new List<GuestController>();
	public void MakeGuestList(){
		//always 5 + random number 2-3


	}

	public void LadyFeedback(){
		string response0 = "Mmmmmmm that's pretty guud!";
		LadyFeedback tasteClass = new LadyFeedback (courseFlavor0.x, courseFlavor0.y);
		response0 = tasteClass.tasteTest ();
		response0 = response0.Replace ("<course name>", "First Course");

		string response1 = "Mmmmmmm that's pretty guud!";
		tasteClass = new LadyFeedback (courseFlavor1.x, courseFlavor1.y);
		response1 = response0 + "\n" + "\n" + "\n" + tasteClass.tasteTest ();
		response1 = response1.Replace ("<course name>", "Second Course");

		string response = "Mmmmmmm that's pretty guud!";
		tasteClass = new LadyFeedback (courseFlavor2.x, courseFlavor2.y);
		response = response1 + "\n" + "\n" + "\n" + tasteClass.tasteTest ();
		response = response.Replace ("<course name>", "Third Course");

		LadyDialogue.button.GetComponentInChildren<Text> ().text = "RESET";
		LadyDialogue.CharacterSpeak(response);
	}

	public bool timerOn = false;
	void Update(){
		if (chef.isCooking) {
			chefSlider.value = currentlyCooking.howRaw;
		}

		if (tutorial1Part3) {
			if (!checkSpinach) {
				if (activeDishes.Count == 1) {
					if (activeDishes [0].name == "Spinach") {
						if (spinachTasted) {
							checkSpinach = true;
							MakeTutorialeBox ("Go ahead and taste the Spinach!");
						}
					}
				}
			}
		}


		if (timerOn) {
			if (timer) {
				time -= Time.deltaTime;

				float minutes = Mathf.Floor (time / 60f);
				float seconds = (time % 60);

				countdown.text = string.Format ("{0:0}:{1:00}", minutes, seconds);
				if (time <= 0) {
					if (canServe) {
						canServe = false;
						if (course == 0) {
							time = course2time;

							if (tutorial0Part22) {
								time = course2time * .9f;
							}
							course += 1;
						} else if (course == 1) {
							time = course3time;
							course += 1;
							if (tutorial0Part3) {
								timerOn = false;
								if (servedDishes.Count >= 2) {
									LadyDialogue.CharacterSpeak ("You have done quite well. My lord and I appreciate the service you have rendered us this night. If the news my lord brought home is right, we may have need of a larger kitchen crew soon.");
								} else if (servedDishes.Count == 1) {
									LadyDialogue.CharacterSpeak ("Your work has been serviceable. Please do try to manage your time better in the future.");
								} else {
									LadyDialogue.CharacterSpeak ("You would let us starve? Get ye gone, scoundrel!");
								}
							}

							if (tutorial1Part6) {
								if (servedDishes.Count == 1) {
									LadyDialogue.CharacterSpeak ("You are proving to be very unreliable. I don’t know if I should be leaving a plan like this in hands as rough as yours.");
								} else {
									string response = "";


									bool found = false;
									GameObject hottest = servedDishes[1];
									GameObject coldest = servedDishes[1];
									GameObject moistest = servedDishes[1];
									GameObject dryest = servedDishes[1];

									for (int i = 1; i < servedDishes.Count; i++) {
										if (servedDishes [i].GetComponent<Ingredients> ().flavor.x > hottest.GetComponent<Ingredients>().flavor.x) {
											hottest = servedDishes [i];
										}
										if (servedDishes [i].GetComponent<Ingredients> ().flavor.x < hottest.GetComponent<Ingredients>().flavor.x) {
											coldest = servedDishes [i];
										}
										if (servedDishes [i].GetComponent<Ingredients> ().flavor.y > hottest.GetComponent<Ingredients>().flavor.y) {
											moistest = servedDishes [i];
										}
										if (servedDishes [i].GetComponent<Ingredients> ().flavor.y < hottest.GetComponent<Ingredients>().flavor.y) {
											dryest = servedDishes [i];
										}
									}

									for (int i = 1; i < servedDishes.Count; i++) {
										if (servedDishes [i].GetComponent<Ingredients> ().flavor.x < 0
											&& Mathf.Abs(servedDishes[i].GetComponent<Ingredients>().flavor.y) < Mathf.Abs(servedDishes[i].GetComponent<Ingredients>().flavor.x)) {
											response += "Your " + servedDishes[i].name + " was notably cold. My favorite flavor I must say.";
											found = true;
										}
									}
									if (!found) {
										response += "\n" + "I was disappointed with the lack of cold-humoured dishes. Perhaps that was your intent with " + coldest.name + "?";
									}

									found = false;
									for (int i = 1; i < servedDishes.Count; i++) {
										if (servedDishes [i].GetComponent<Ingredients> ().flavor.x > 0
											&& Mathf.Abs(servedDishes[i].GetComponent<Ingredients>().flavor.y) < Mathf.Abs(servedDishes[i].GetComponent<Ingredients>().flavor.x)) {
											response += "The " + servedDishes[i].name + " was indeed stronger in heat than any other flavor. Not to my personal taste, but some like it that way.";
											found = true;
										}
									}
									if (!found) {
										response += "\n" + "While warm flavors are not my favorite, they are a healthy part of good eating. Where was the dish that was stronger in its heat than any other flavor? Was that your objective with" + hottest.name + " ?";
									}

									found = false;
									for (int i = 1; i < servedDishes.Count; i++) {
										if (servedDishes [i].GetComponent<Ingredients> ().flavor.y < 0
											&& Mathf.Abs(servedDishes[i].GetComponent<Ingredients>().flavor.x) < Mathf.Abs(servedDishes[i].GetComponent<Ingredients>().flavor.y)) {
											response += "\n" + "You cooked the " + servedDishes[i].name + " to the dryness I asked for, thank you.";
											found = true;
										}
									}
									if (!found) {
										response += "\n" + "There was a sad deficit of strongly dry dishes. I believe the " + dryest.name + " could have served that purpose.";
									}

									found = false;
									for (int i = 1; i < servedDishes.Count; i++) {
										if (servedDishes [i].GetComponent<Ingredients> ().flavor.y > 0
											&& Mathf.Abs(servedDishes[i].GetComponent<Ingredients>().flavor.x) < Mathf.Abs(servedDishes[i].GetComponent<Ingredients>().flavor.y)) {
											response += "\n" + "The moisture of your " + servedDishes[i] + " was its strongest flavor. I’m sure my young Henri would have loved it.";
											found = true;
										}
									}
									if (!found) {
										response += "\n" + "Why were there no dishes with mostly Moist flavors? Could you not have used the " + moistest.name + " for that purpose?";
									}

								}

							}

						} else if (course == 2) {
							LadyFeedback ();
							timer = false;
							if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().activeDropdown != null) {
								GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().activeDropdown.GetComponent<Station> ().Cancel ();
							}
						}
					} else {
						time = 60f;
						canServe = true;
//					EventController.Event (Event.START_MUSIC);
						if (!tutorial0Part22 && !tutorial0Part3) {
							MakeDialogueBox ("SERVE IT FORTH!");
						} else if (tutorial0Part22) {
							MakeTutorialeBox ("He’s here and he’s hungry! To serve forth your creation, take it to the door labeled 'Serve it Forth!'");
						} else if (tutorial0Part3) {
							MakeTutorialeBox ("Supper is Served");
						}

					}
				}
			}
		}
	}

	public void EndCourse(){
		time = 0;
	}
	public void Reset(){
		new WaitForSeconds (2);

		SceneManager.LoadScene (0);


		time = 480f;
	}

}