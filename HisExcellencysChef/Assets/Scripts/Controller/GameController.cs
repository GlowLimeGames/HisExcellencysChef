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

	public int course = 0;
	public Vector2 courseFlavor;
	public GameObject endScreen;
	public Text score;

	public Sprite[] sliders;
	public Slider chefSlider;

	public Transform DishesPanel;
	public Button UIDishPrefab;
	public List<GameObject> activeDishes = new List<GameObject>();

	//something something keep track of active ingredients for UI and Reset
	public void MakeUIDish(GameObject food){
		Button dish = (Button)Instantiate (UIDishPrefab, DishesPanel);
		dish.transform.localScale = Vector3.one;
		dish.GetComponent<Image> ().sprite = food.GetComponent<SpriteRenderer> ().sprite;
	}

	public Ingredients currentlyCooking;
	public void EditSlider(string action, Ingredients ingredient){
		currentlyCooking = ingredient;
		chefSlider.gameObject.SetActive (true);
		chefSlider.maxValue = cooking.GetProcess (action).YellowRed;
		foreach (Sprite bar in sliders) {
			if (bar.name == cooking.GetProcess (action).ProgressBarPNG) {
				chefSlider.GetComponentInChildren<Image>().sprite = bar;
			}
		}
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
				if (course == 0) {
					time = 540f;
					course += 1;
				} else if (course == 1) {
					time = 600f;
					course += 1;
				} else if (course == 2) {
					endScreen.SetActive (true);
					score.text = string.Format ("{0}:{0}", courseFlavor.x, courseFlavor.y);
					timer = false;
					if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().activeDropdown != null) {
						GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().activeDropdown.GetComponent<Station> ().Cancel ();
					}
				}
			}
		}
	}

	public void EndCourse(){
		time = 0;
	}
	public void Reset(){
		SceneManager.LoadScene (0);
		time = 480f;
	}

}