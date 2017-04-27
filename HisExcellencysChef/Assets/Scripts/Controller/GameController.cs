/*
 * Author(s): Isaiah Mann
 * Description: Controls the game logic
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class GameController : SingletonController<GameController> {
	public int Level;
	public int Fame;
	public int Infamy;
	public GameObject[] Underlings;
	public GameObject[] Ingredients;
	public InventoryUI inventory;
	public string[] level0Obsoletes;
	public bool tutorial0Part1;
	public bool tutorial0Part2;
	public bool tutorial0Part22;
	public bool tutorial0Part3;

	public bool tutorial1Part1;
	public bool clickedAemilia;
	public bool tutorial1Part2;
	public bool tutorial1Part3;
	public bool checkSpinach;
	public bool spinachTasted;
	public bool tutorial1Part4;
	public bool tutorial1Part5;
	public bool tutorial1Part6;

	public bool tutorial2Part1;
	public bool tutorial2Part2;
	public bool tutorial2Part3;
	public bool tutorial2Part4;
	public bool tutorial2Part5;

	public bool tutorial3Part1;
	public bool tutorial3Part2;
	public bool tutorial3Part3;

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
		guestController = GetComponent<GuestController> ();
		time = levelTime;
		if (Level == 0) {
			LadyDialogue.CharacterSpeak (@"“You there! You have helped in the kitchen before, have you not? We have a small emergency.”

“Count Philip, my husband, will be returning from the Duke’s manor much sooner than planned, and with news. He will be expecting a meal, but we are short on time. You are our only servant currently here who who knows their way around a kitchen.”

“He’s requested Boulettes. I’ve left some chicken in the Mortar for you. Click on the Mortar and choose a process to begin cooking.");			
		} else if (Level == 1) {
			LadyDialogue.CharacterSpeak (@"I have a plan and you're it’s key to its success. In a weeks time [Important Guest], the kings advisor/brother, will be passing through.  I intend to throw a feast such that he will be singing our praise high and low and to ear of the king himself.
				
			Until then you need to practice. You're a natural talent, but could do with some instruction.  We’ll make a good start of it today. I’ve hired some underlings for you.  Let us start with an exotically themed spinach salad with roasted Almonds. I’ve given some Almonds to your underling, so let us practice working with others.");
		} else if (Level == 2) {
			LadyDialogue.CharacterSpeak (@"Good morning. In preparation for the upcoming feast, you should work on understanding guest preferences. After all, we don’t want to be giving Master Simon food he dislikes.

Today Anna, our daughter, has volunteered to try some of your dishes.  She, like all guests, wants certain things more than others.");

			guestController.GetGuest ("Anna");
			canServe = true;
		} else if (Level == 3) {

			guestController.GetGuest ("Count Philip");
			guestController.GetGuest ("Lady Florine");
			guestController.GetGuest ("Anna");
			guestController.GetGuest ("Henri");
			guestController.GetGuest ("Master Roger of Bologna");
			guestController.GetGuest ("Renaud d'Oc");
			guestController.GetGuest ("Lady Anna de Chalons");
			guestController.GetGuest ("Sir Conrad the Swabian");
			guestController.GetGuest ("Master Simon de Paris");
			guestController.GetGuest ("Count Godfrey");
			Fame = 15;
			Infamy = 7;

			LadyDialogue.CharacterSpeak (@"Master Simon is here, tonight is the night! A chance for fame, for you and me both.  Remember, the guest list is quite long, so more food than before will be necessary. Try to serve at least 4 dishes per course, one of which per course should have at least five ingredients. Tonight we need you to be a cook fit for a king!”
				
These are the guests who will be attending our feast tonight, including Master Simon:
Count Philip
Lady Florine
Anna
Henri
Master Roger of Bologna
Renaud d'Oc
Lady Anna de Chalons
Sir Conrad the Swabian
Master Simon de Paris
Count Godfrey"); 
		} else {
			SceneController.Instance.Load ();
			GetNewIngredients ();
			GetNewUnderlings ();
			GetNewGuests ();

			if (Fame > 70) {
				float chance = 2.5f * (Fame - 70);
				int random = Random.Range (0, 100);
				if (random <= Mathf.CeilToInt (chance)) {
					KingLevel ();
				}
			}

			if (Infamy > 70) {
				float chance = 2.5f * (Infamy - 70);
				int random = Random.Range (0, 100);
				if (random <= Mathf.CeilToInt (chance)) {
					InfamyLevel ();
				}
			}

			if (!kingLevel && !infamyLevel) {
				LadyDialogue.CharacterSpeak (@"Hello, young cook!

To select a primary ingredient, click on a character, then click on the Pantry Door.

To cook that ingredient, click a station and select ""Put Down"" in the drop down menu, then select it again and pick the cooking process you want and wait for the character to pick it up again.

When you want to finish a dish, select the character carrying it and click on the gray square below.

Ingredients are Hot or Cold and Moist or Dry. Try to balance the flavors across your dishes.");
			
			}
		}

	}
		
	void GetNewIngredients(){
		int random = Random.Range (0, 4);
		if (random == 0){
			int amount = Random.Range (3, 5);
			inventory.foods [11].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 65){
			int amount = Random.Range (2, 4);
			inventory.foods [1].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 90){
			int amount = Random.Range (2, 4);
			inventory.foods [2].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 80){
			int amount = Random.Range (2, 6);
			inventory.foods [3].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 75){
			int amount = Random.Range (1, 3);
			inventory.foods [4].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 40){
			int amount = Random.Range (2, 4);
			inventory.foods [5].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 40){
			int amount = Random.Range (2, 4);
			inventory.foods [6].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 25){
			int amount = Random.Range (1, 3);
			inventory.foods [7].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 25){
			int amount = Random.Range (1, 3);
			inventory.foods [8].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 30){
			int amount = Random.Range (1, 4);
			inventory.foods [9].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 30){
			int amount = Random.Range (1, 4);
			inventory.foods [10].quantity += amount;
		}
		random = Random.Range (0, 100);
		if (random < 8){
			int amount = Random.Range (1, 3);
			inventory.foods [12].quantity += amount;
		}


	}

	public int underlingCount;
	public GameObject[] underlingPrefabs;
	public List<GameObject> currentUnderlings;
	public Transform[] underlingPositions;
	void GetNewUnderlings(){
		
	}

	void GetNewGuests(){
		GuestDescriptor[] newGuests = guestController.GetGuestList (guestController.guestLookup.Values.ToArray ());

		foreach (GuestDescriptor guest in newGuests) {
			guestController.GetGuest (guest.name);
		}
	}

	public bool kingLevel = false;
	void KingLevel(){
		kingLevel = true;

		inventory.foods [11].quantity += 3;
		inventory.foods [1].quantity += 3;
		inventory.foods [2].quantity += 4;
		inventory.foods [3].quantity += 3;
		inventory.foods [4].quantity += 3;
		inventory.foods [5].quantity += 2;
		inventory.foods [6].quantity += 4;
		inventory.foods [7].quantity += 2;
		inventory.foods [8].quantity += 4;
		inventory.foods [9].quantity += 4;
		inventory.foods [10].quantity += 4;

		LadyDialogue.CharacterSpeak ("The king has heard from those who you have pleased and is anxious to try your food himself! His eyes are now on all of us. He is, however, a man influenced by those around him. If he or any guest is served any dish that would cause them to increase your Infamy, he will lose interest, so serve only the finest dishes for this evening. Don’t forget that this is a feast, though. Remember to serve at least 4 dishes per course.");
			
		guestController.GetGuest ("King of France");

	}

	public bool infamyLevel = false;
	void InfamyLevel(){
		if (Fame < 33) {
			LadyDialogue.CharacterSpeak ("The many times you have served unto our family and guests inadequate meals has gotten us left out of many a social occasion. In the simplest form I want you to know that I am having second thoughts and you better not make me have more. I will be watching tonight. I expect at least two dishes per course with no ruined ingredients. If we do not get them, you will be relieved of your duties working for us.");
		} else if (Fame >= 33 && Fame < 66) {
			LadyDialogue.CharacterSpeak ("I see in you the potential for greatness, but I am beginning to have qualms. <If have poisoned a target before> People have not forgotten the accidents that have happened here.</> Often, guests leave our house feeling sickly.  I have suspicions that had better remain just suspicions and will be watching you closely tonight. I expect at least two healthy dishes per course with no ruined ingredients. If we do not get them, you will be relieved of your duties working for us.");
		} else if (Fame >= 66) {
			LadyDialogue.CharacterSpeak("You have done great things for our house it is true.  However, of late I have felt the burden of explaining accidents away, or explaining why a dish left a guest feeling sick.  I will be keeping a close watch over you today so I recommend doing your best work. I am starting to have second thoughts; show me that they are unfounded tonight. I expect at least two healthy dishes per course with no ruined ingredients. If things go awry, you will be relieved of your duties working for us.");
		}
	}

	void playMusic () {
		EventController.Event(Event.GAME_MUSIC);
	}

	public CharacterMovement chef;
	public CharacterMovement underling1;
	public CharacterMovement underling2;

	public bool timer = false;
	public float time;
	public Text countdown;

	public bool canServe = false;
	public int course = 0;
	public Vector2 courseFlavor0;
	public Vector2 courseFlavor1;
	public Vector2 courseFlavor2;
	public int course1count;
	public int course2count;
	public int course3count;
	public GameObject endScreen;
	public Text score;

	public Sprite[] sliders;
	public Slider chefSlider;

	public float dialogueTime = 5f;
	GuestController guestController;
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
		} else if (tutorial2Part3) {
			MakeTutorialeBox ("So… I could get famous off of this. The King could hear about me. I wonder what it would be like to cook for royalty.\n I should be careful what I wish for. Nobles can be temperamental. If I get too infamous and my Lady hears too many bad stories about me, I could get kicked out. Better be careful...");
		} else if (tutorial3Part1) {
			//underling move towards you
			tutorial3Part1 = false;
			tutorial3Part2 = true;
			LadyDialogue.CharacterSpeak ("You have received a note. It reads thusly:\n‘Attending your feast tonight will be one Lady Anna de Chalons, the Count’s political rival and mine. I would like you to poison her. Your Count, gracious host that he is, ensures each guest gets the largest portions of their favorite dishes. Create a dish that matches her tastes both in flavor and cooking style as strongly as possible, and include in it some of this essence of hemlock. She will accept his magnanimity, then mysteriously take ill and die. If you do this for me, I will ensure that word of your skill is heard by every ear in the Parisian court.’");
			inventory.foods [11].quantity += 1;
		} else if (tutorial3Part3) {
			if (guestController.poisonedList [0] != null) {
				if (guestController.poisonedList [0].name == "Lady Anna de Chalons") {
					MakeTutorialeBox ("Excellent work. Lady Anna graciously accepted a large portion of the <dish> and is beginning to look quite… uncomfortable.’");
				} else {
					MakeTutorialeBox ("You poisoned the wrong person! You can try to get another dish out with hemlock in it if you have any left, but be more careful next time!");
				}
			} else {
				MakeTutorialeBox ("Whatever you put the hemlock in didn’t match anyone’s tastes. Now we’re all a bit queasy.’");
			}
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

	public GameObject guestBox;
	void CloseGuest(){
		guestBox.SetActive (false);
	}

	public bool florineTriggered = false;
	Ingredients florineDish;
	public void ModifyGuests(){
		Ingredients lastDish = servedDishes [servedDishes.Count - 1].GetComponent<Ingredients>();

		int priorityEat = 0;
		GuestDescriptor priorityGuest = guestController.guestList[0];
		foreach (GuestDescriptor guest in guestController.guestList) {
			int currentPriorityEat = guestController.CheckForFame (guest, lastDish);
			if (currentPriorityEat > priorityEat) {
				priorityEat = currentPriorityEat;
				priorityGuest = guest;
			} else if (currentPriorityEat == priorityEat) {
				if (guest.priority > priorityGuest.priority) {
					priorityGuest = guest;
				}
			}
		}
		if (priorityEat != 0) {
			if (lastDish.ContainsIngredient ("Hemlock")) {
				if (priorityGuest.name == "Lady Florine") {
					florineTriggered = true;
					florineDish = lastDish;
					LadyDialogue.CharacterSpeak ("I have given you everything...  I made you what you are…and you repay me with disloyalty!  You are but my hand in the kitchen. So my suspicions were well-founded. And you thought I wouldn’t notice hemlock in my favorite foods. Guards, take this traitor! You are hereby exiled from these lands. May you starve in the wilds and may the wolves gnaw on your bones.");
				} else {
					if (infamyLevel) {
						LadyDialogue.CharacterSpeak ("Did you think I would not notice hemlock in our food? That I had not? You have terrorized my guests and family long enough. To poison a respected member of society is a nefarious act.  For this I declare a smooth death from the elements of the forest. Guards, escort this criminal to the edge of my domain.");
						return;
					}
					guestController.poisonedList.Add (priorityGuest);
					Invoke ("PoisonNotice", 30f);
				}
			}
		}
		if (priorityEat == 0) {
			Infamy += 30;
		}
	}

	void PoisonNotice(){
		string names = "";
		foreach (GuestDescriptor guest in guestController.poisonedList) {
			names += guest.name + " ";
		}
		string prep = "";
		if (guestController.poisonedList.Count > 1) {
			prep = "have";
		}else{
			prep = "has";
		}
		MakeTutorialeBox ( names + prep + " taken ill and retired to their lodgings.");
	}

	public void BannerOnClick()
	{
		GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
		GameObject food = activeDishes[int.Parse(thisButton.name)];

		string response = "";
		List<GuestDescriptor> validGuests = new List<GuestDescriptor> ();
		foreach (GuestDescriptor guest in guestController.guestList) {
			if (guestController.ParseTriggers (guest, food.GetComponent<Ingredients>()) != "I have nothing to say to you.") {
				validGuests.Add (guest);
			}
		}

		List<GuestDescriptor> sortedList = new List<GuestDescriptor>();
		int priority = 0;
		for (int i = 0; i < validGuests.Count; i++) {
			if (validGuests [i].priority > priority) {
				sortedList.Add(validGuests[i]);
			}
		}

		for (int j = 0; j < sortedList.Count; j++) {
			if (j < 3) {
				guestBox.transform.GetChild(0).GetComponent<Image>().sprite = guestController.GetGuestImage (sortedList [j]);
				response += guestController.ParseTriggers (sortedList [j], food.GetComponent<Ingredients> ()) + "\n";
			}
		}
		if (response == "") {
			response = "I have nothing to say to you";
		}
		if (GameController.Instance.tutorial2Part2) {
			response = @"Anna loves the dish! When you make a guest especially happy, they will tell others about you, spreading your fame. This is what we are going for. If you make something they hate, they will complain about it, increasing your infamy. Fame and infamy are both fleeting though, and old fame will slowly fade as the days go by. Fame is represented by the yellow bar at the top of your screen, while infamy is represented by the purple.
			You will never be able to make a dish that perfectly suits everyone’s tastes. Why don’t you try to serve a course for Anna and her brother Henri both.";
			MakeTutorialeBox (response);
			guestController.GetGuest ("Henri");
			inventory.foods [0].quantity += 1;
			Inventory.InventoryNode[] array = new Inventory.InventoryNode[10];
			for (int i = 0; i < inventory.foods.Length; i++) {
				array [i] = inventory.foods [i];
			}
			array [7].obj = Ingredients [6];
			array [7].quantity = 2;
			array [8].obj = Ingredients [1];
			array [8].quantity = 1;
			array [9].obj = Ingredients [40];
			array [9].quantity = 2;

			inventory.foods = array;
			timerOn = true;
			tutorial2Part2 = false;
			tutorial2Part3 = true;
			Invoke ("NextTutorialStep", 5f + course1time * .16f);

		} else {
			MakeGuestBox (response);
		}

//		GuestController guest = GetComponent<GuestController> ();
//		guest.GetGuest ("Count Philip");
//		guest.GetGuest ("Master Simon de Paris");
//		Debug.Log (guest.ParseTriggers(guest.guestList[0], food.GetComponent<Ingredients>()));
//		Debug.Log (guest.ParseTriggers(guest.guestList[1], food.GetComponent<Ingredients>()));
		//if food was served then display this information in a text box
//		Debug.Log (food.GetComponent<Ingredients>().primaryIngredientName);
//		Debug.Log (food.GetComponent<Ingredients> ().flavor);
	}

	public void MakeGuestBox(string whatToSay){
		guestBox.SetActive (true);
		guestBox.GetComponent<DialogueController> ().CharacterSpeak (whatToSay);

		CancelInvoke ("CloseGuest");
		Invoke ("CloseGuest", dialogueTime);
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

		if (tutorial2Part4) {
			response += "\n" + "I hope you feel ready, the time for the feast is fast approaching.";
		}

		LadyDialogue.button.GetComponentInChildren<Text> ().text = "RESET";
		if (florineTriggered) {
			response = "Your " + florineDish.name + " was… off. I’ve got my eye on you.";
		}

		LadyDialogue.CharacterSpeak(response);

		Fame -= 10;
		Infamy -= 5;
	}

	public bool timerOn = false;
	public bool hitNegative = false;
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
							if (tutorial2Part3) {
								MakeTutorialeBox ("It looks like Philip brought back some friends of ours. Since you’re still learning, we’ll go with a two-course meal instead of the usual three courses. You’ll have only a little time between courses, so make sure you’re well prepared for both. One final factor to keep in mind with regards to flavors and courses: while individuals may prefer strong tastes, courses themselves should have overall balanced flavors for health reasons. If you, for example, serve a very moist dish, make sure to serve one or more dry dishes to balance it out.");
								inventory.foods [0].quantity += 2;
								inventory.foods [7].quantity += 2;
								inventory.foods [1].quantity += 3;
								inventory.foods [2].quantity += 2;
								inventory.foods [3].quantity += 1;
								inventory.foods [4].quantity += 1;
								inventory.foods [8].quantity += 2;
								inventory.foods [5].quantity += 1;
								inventory.foods [9].quantity += 1;
								inventory.foods [6].quantity += 1;

								guestController.guestList.Clear ();
								guestController.GetGuest ("Count Philip");
								guestController.GetGuest ("Lady Florine");
								guestController.GetGuest ("Master Roger of Bologna");
								guestController.GetGuest ("Sir Conrad the Swabian");
								guestController.GetGuest ("Count Godfrey");

								courseFlavor0 = Vector2.zero;
								courseFlavor1 = Vector2.zero;
								tutorial2Part3 = false;
								tutorial2Part4 = true;
								course = 0;
							}
						} else if (course == 1) {
							if (tutorial2Part4) {
								LadyFeedback ();
							}
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
							if (tutorial3Part3) {
								if (servedDishes.Count >= 4) {
									bool done = false;
									foreach (GameObject dish in servedDishes) {
										if (dish.GetComponent<Ingredients> ().addOnIngridents.Count >= 4) {
											done = true;
										}
									}
									if (done) {
										Fame += 25;
										LadyDialogue.CharacterSpeak (@"You did wonderfully! Master Simon was quite impressed with the feast. If we gather enough fame from people like him, I wonder if perhaps we can attract the King some day. Are you ambitious?

											Now, about the courses...");
									} else {
										Fame += 10;
										Infamy += 10;
										LadyDialogue.CharacterSpeak (@"We have finished for the night. Your service was sufficient, but the lack of complexity rather failed to impress Master Simon. Perhaps if we continue to work together and can spread your fame better, we can still attract royal attention.”

Regarding the flavors of the courses...");
									}
								} else if (servedDishes.Count <= 3) {
									bool done = false;
									foreach (GameObject dish in servedDishes) {
										if (dish.GetComponent<Ingredients> ().addOnIngridents.Count >= 4) {
											done = true;
										}
									}
									if (done) {
										Fame += 10;
										Infamy += 10;
										LadyDialogue.CharacterSpeak (@"The dishes were nicely complex, but in case you’ve forgotten, this was supposed to be a feast. Not enough food was served! We will have difficulty attracting attention and fame this way.

Regarding the courses, such as was there to eat.");
									} 
								} else if (servedDishes.Count == 0) {
									Infamy += 20;
									LadyDialogue.CharacterSpeak (@"Do you refuse to feed us?? Is there something wholly wrong with you? Go, get out! I shall see you gone from my lands by midmorning!");
									tutorial3Part3 = false;
								}
								else {
									Fame -= 5;
									Infamy += 10;
									LadyDialogue.CharacterSpeak (@"This was a disappointment. You followed neither of my instructions, and I begin to question if you are at all fit for leading the kitchen. Have you no ambition?");
									tutorial3Part3 = false;
								}
							} else {
								LadyFeedback ();
							}
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

	public bool beatenKing = false;
	public void KingFeedback(){
		if (kingLevel) {
			bool poisonedKing = false;
			foreach (GuestDescriptor guest in guestController.poisonedList) {
				if (guest.name == "King of France") {
					poisonedKing = true;
				}
			}
			if (poisonedKing) {
				LadyDialogue.CharacterSpeak ("The king was found dead in his chambers. Did you not think you would be suspected?  At the very least you are complacent in your vigilance over his food.”\n<continue break>\n“Guards! Prepare for my cook a last meal of half an orange and gingerbread. Prepare your neck for the axe!");
				kingLevel = false;
				return;
			}

			if (servedDishes.Count > 4) {
				if (!hitNegative) {
					LadyDialogue.CharacterSpeak ("I am both delighted and, I must confess... shocked. It seems you have placed your last meal onto our now beloved table. Even the more critical guests are speaking your praises out there.\n\nWhat am I saying? Let me explain. His Majesty told me that with you serving dishes he would be able to keep any number of guests happy and that he is quite taken with your food himself. He has requested that I yield you to him. We will miss your amazing cooking and you have already brought us so far in social circles because of it. You have achieved the highest possible honor: the king’s mouth and ear are yours, and you will get to work with the freshest ingredients and best underlings money can hire. Congratulations.");
					kingLevel = false;
					beatenKing = true;
				} else {
					LadyDialogue.CharacterSpeak ("The King has departed. He did not say much about the meal except that he was perhaps interested in returning another day. Perhaps you will get another chance to impress him.");
					kingLevel = false;
				}
			}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       			
		}
	}

	public void InfamyFeedback(){
		if (infamyLevel) {
			if (course1count >= 3 && course2count >= 3 && course3count >= 3) {
				foreach (GameObject dish in servedDishes) {
					foreach (string action in dish.GetComponent<Ingredients>().actionsDone) {
						if (action != "Green" || action != "DarkGreen") {
							LadyDialogue.CharacterSpeak ("I must apologize for my suspicious ways. Of course we will keep you on after tonight.  Well done.");
							Infamy -= 20;
						} else {
							LadyDialogue.CharacterSpeak ("Your dishes leave a lack of distinction to them.  I will not tolerate such disgusting food being fed to my family and guests.  You have disgraced us.  We need someone who will look out for us better than you have.  Guards, send this man away and see that he does not return. Exiled to the forest, perhaps starvation will be a fitting end to a terrible cook.");
						}
					}
				}
			} else if (course1count == 2 && course2count == 2 && course3count == 2) {
				foreach (GameObject dish in servedDishes) {
					foreach (string action in dish.GetComponent<Ingredients>().actionsDone) {
						if (action != "Green" || action != "DarkGreen") {
							LadyDialogue.CharacterSpeak ("You have done adequately, and I do not have room to complain.  We are fed well enough.  You shall remain on here.  I appreciate your service tonight.");
							Infamy -= 5;
						} else {
							LadyDialogue.CharacterSpeak ("You not only feed less than what my family needs but you feed us this drivel!  This kitchen is no place for someone who seeks to starve my family and guests and feed them naught but discolored refuse!  This is why we have been pushed so far from the social gatherings we seek to attend.  You have failed us and I will see that you suffer for it.  Guards, escort this cook to the wild edges of my lands. A death of thirst should ensure the next cook does a better job of it.");
						}
					}
				}
			} else if (course1count <= 2 || course2count <= 2 || course3count <= 2) {
				LadyDialogue.CharacterSpeak ("You not only feed less than what my family needs but you feed us this drivel!  This kitchen is no place for someone who seeks to starve my family and guests and feed them naught but discolored refuse!  This is why we have been pushed so far from the social gatherings we seek to attend.  You have failed us and I will see that you suffer for it.  Guards, escort this cook to the wild edges of my lands. A death of thirst should ensure the next cook does a better job of it.");
			} else if (servedDishes.Count == 0) {
				LadyDialogue.CharacterSpeak ("You would starve us. Your audacity ends here. Get out.");
			}
		}
	}

	public void EndCourse(){
		time = 0;
	}
	public void Reset(){
		new WaitForSeconds (2);
		SceneController.Instance.Save ();
		SceneManager.LoadScene (0);
	}

}