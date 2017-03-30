/*
 * Author(s): Isaiah Mann
 * Description: Controls the game logic
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : SingletonController<GameController> {
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
		underling1 = GameObject.Find ("Aemilia").GetComponent<CharacterMovement>();
	}
		
	void playMusic () {
		EventController.Event(Event.GAME_MUSIC);
	}

	public CharacterMovement chef;
	public CharacterMovement underling1;

	public bool timer = false;
	public float time = 480f;
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
	public GameObject UIDialogueBox;
	public Transform DishesPanel;
	public Button UIDishPrefab;
	public List<GameObject> activeDishes = new List<GameObject>();

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

	void CloseDialogue(){
		UIDialogueBox.SetActive (false);
	}

	void BannerOnClick()
	{
		GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
		GameObject food = activeDishes[int.Parse(thisButton.name)];

		//if food was served then display this information in a text box
		Debug.Log (food.GetComponent<Ingredients>().primaryIngredientName);
		Debug.Log (food.GetComponent<Ingredients> ().flavor);
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

		LadyDialogue.button.GetComponentInChildren<Text> ().text = "RESET";
		LadyDialogue.CharacterSpeak(response);
	}

	void Update(){
		if (chef.isCooking) {
			chefSlider.value = currentlyCooking.howRaw;
		}

		if (timer) {
			time -= Time.deltaTime;

			float minutes = Mathf.Floor (time / 60f);
			float seconds = (time % 60);

			countdown.text = string.Format ("{0:0}:{1:00}", minutes, seconds);
			if (time <= 0) {
				if (canServe) {
					canServe = false;
					if (course == 0) {
						time = 540f;
						course += 1;
					} else if (course == 1) {
						time = 600f;
						course += 1;
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
					MakeDialogueBox ("SERVE IT FORTH!");
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