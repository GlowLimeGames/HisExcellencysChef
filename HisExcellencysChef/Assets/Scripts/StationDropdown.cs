using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StationDropdown : MonoBehaviour {
	public GameObject station;
	public GameObject functionButton;

	public bool active = false;
	public List<GameObject> functionList = new List<GameObject> ();
	string[] dropDownOptions;
	Image selectedImage;
	public Image pickUp;
	public Image putDown;
	public Image addTo;
	public Image discard;
	public Image taste;
	public TasteTesting tasteClass; 

	// Use this for initialization
	void Start () {
		transform.parent.gameObject.SetActive (false);
		selectedImage = GetComponent<Image> ();
	}

	public void OnClicked(GameObject clickedObject){
		if (clickedObject.tag == "Station") {
			active = true;
			station = clickedObject;
			dropDownOptions = GetPossibleFunctions (clickedObject);
			MakePossibleFunctions (clickedObject);
			ShowIngredientProcesses (clickedObject);
			BlockOutObsoletes (clickedObject);
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (clickedObject.transform.parent.name != "Environment") {
				selectedImage.sprite = clickedObject.transform.parent.GetComponent<SpriteRenderer> ().sprite;
			} else {
				selectedImage.sprite = clickedObject.GetComponent<SpriteRenderer> ().sprite;
			}
			transform.parent.position = new Vector3 (mousePos.x, 0f, mousePos.z);
			transform.parent.gameObject.SetActive (true);
		}
	}

	string[] GetPossibleFunctions(GameObject clickedObject){
		return CookingController.Instance.GetProcessesForStation (clickedObject.name);
	}



	void MakePossibleFunctions(GameObject clickedObject){
		//Make Basic Buttons
		string process;

		if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish != null) {
			if (clickedObject.name == "Range" || clickedObject.name == "Table") {
				if (!GameController.Instance.tutorial0Part1) {
					process = "PutDown";
					GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
					aButton.transform.SetParent (transform);
					aButton.transform.Rotate (Vector3.zero);
					aButton.name = process;
					aButton.GetComponentInChildren<Text> ().text = process;
					functionList.Add (aButton);
				}
			}
			if (station.GetComponent<Station> ().dish != null) {
				if (!GameController.Instance.tutorial0Part1) {
					if (!GameController.Instance.tutorial1Part1 && !GameController.Instance.tutorial1Part5 &&!GameController.Instance.tutorial1Part2) {
						process = "AddTo";
						GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
						aButton.transform.SetParent (transform);
						aButton.transform.Rotate (Vector3.zero);
						aButton.name = process;
						aButton.GetComponentInChildren<Text> ().text = process;
						functionList.Add (aButton);
					}
				}
			}
		} else {
			if (station.GetComponent<Station> ().dish != null) {
				if (!GameController.Instance.tutorial0Part1 && !GameController.Instance.tutorial1Part1) {
					if (station.GetComponent<Station> ().cookingCharacter == null || !station.GetComponent<Station> ().cookingCharacter.GetComponent<CharacterMovement> ().isCooking || GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().isCook) {
						process = "PickUp";
						GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
						aButton.transform.SetParent (transform);
						aButton.transform.Rotate (Vector3.zero);
						aButton.name = process;
						aButton.GetComponentInChildren<Text> ().text = process;
						functionList.Add (aButton);
					}
				}
			}
		}
		if (station.GetComponent<Station> ().dish != null) {
			if (!GameController.Instance.tutorial0Part1 && !GameController.Instance.tutorial1Part1 && !GameController.Instance.tutorial1Part5) {
				if (station.GetComponent<Station> ().cookingCharacter == null || !station.GetComponent<Station> ().cookingCharacter.GetComponent<CharacterMovement> ().isCooking) {
					process = "Discard";
					GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
					aButton.transform.SetParent (transform);
					aButton.transform.Rotate (Vector3.zero);
					aButton.name = process;
					aButton.GetComponentInChildren<Text> ().text = process;
					functionList.Add (aButton);
				}
			}
		}

		//Make Discard Function here

		//Chef only Functions
		if (GameObject.Find("ClickHandler").GetComponent<ClickHandler>().currentCharacter.name == "Cook"){
			if (station.GetComponent<Station> ().dish != null) {
				if (!GameController.Instance.tutorial0Part1) {
					process = "Taste";
					GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
					aButton.transform.SetParent (transform);
					aButton.transform.Rotate (Vector3.zero);
					aButton.name = process;
					aButton.GetComponentInChildren<Text> ().text = process;
					functionList.Add (aButton);
				}
			}
		}
		//Make Buttons based on Processes
		if (!GameController.Instance.tutorial1Part5) {
			if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish != null) {
				foreach (string aProcess in dropDownOptions) {
					GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
					aButton.transform.SetParent (transform);
					aButton.transform.Rotate (Vector3.zero);
					aButton.name = aProcess;
					aButton.GetComponentInChildren<Text> ().text = aProcess;
					functionList.Add (aButton);
				}
			} else if (station.GetComponent<Station> ().dish != null) {
				foreach (string aProcess in dropDownOptions) {
					if (station.GetComponent<Station> ().dish.GetComponent<Ingredients> ().actionDone == aProcess) {
						return;
					}
					GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
					aButton.transform.SetParent (transform);
					aButton.transform.Rotate (Vector3.zero);
					aButton.name = aProcess;
					aButton.GetComponentInChildren<Text> ().text = aProcess;
					functionList.Add (aButton);
				}
			}
		}
	}

	void ShowIngredientProcesses(GameObject clickedObject){

		string[] processes = new string[0];

		if (station.GetComponent<Station> ().dish != null) {
			processes = station.GetComponent<Station> ().dish.GetComponent<Ingredients> ().primaryIngredient.Processes;
		} else {
			if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish != null){
				processes = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().primaryIngredient.Processes;
			}
		}

		if (processes == null) {
			return;
		}

		if (station.GetComponent<Station> ().dish != null) {
			for (int i = 0; i < functionList.Count; i++) {
				bool contains = false;
				foreach (string process in processes) {
					if (functionList [i].transform.name == process) {
						contains = true;
					}
				}
				if (contains == false) {
					if (functionList [i].transform.name != "PickUp" &&
					    functionList [i].transform.name != "Discard" &&
					    functionList [i].transform.name != "Taste" &&
					    functionList [i].transform.name != "PutDown" &&
					    functionList [i].transform.name != "AddTo") {
						Destroy (functionList [i]);
					}
				}
			}
		} else {
			if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish != null){
				for (int i = 0; i < functionList.Count; i++) {
					bool contains = false;
					foreach (string process in processes) {
						if (functionList [i] != null) {
							if (functionList [i].transform.name == process) {
								contains = true;
							}
						}
					}
					if (contains == false) {
						if (functionList [i].transform.name != "PickUp" && 
							functionList [i].transform.name != "Discard" &&
							functionList [i].transform.name != "Taste" &&
							functionList [i].transform.name != "PutDown" &&
							functionList [i].transform.name != "AddTo") {
							Destroy (functionList [i]);
						}
					}
				}
			}
		}
	}

	void BlockOutObsoletes(GameObject clickedObject){
		
		if (station.GetComponent<Station> ().dish != null) {
			string[] obsoletes;
			obsoletes = station.GetComponent<Station> ().dish.GetComponent<Ingredients> ().obsoletes;
			for (int i = 0; i < functionList.Count; i++) {
				foreach (string obsolete in obsoletes) {
					if (functionList [i].transform.name == obsolete) {
						Destroy(functionList [i]);
						break;
					}
				}
			}
		} else {
			if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish != null){
				string[] obsoletes;
				obsoletes = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().obsoletes;
				for (int i = 0; i < functionList.Count; i++) {
					foreach (string obsolete in obsoletes) {
						if (functionList [i].transform.name == obsolete) {
							Destroy(functionList [i]);
							break;
						}
					}
				}
			}
		}

		if (GameController.Instance.Level == 0) {
			if (station.GetComponent<Station> ().dish != null) {
				string[] obsoletes = GameController.Instance.level0Obsoletes;
				for (int i = 0; i < functionList.Count; i++) {
					foreach (string obsolete in obsoletes) {
						if (functionList [i].transform.name == obsolete) {
							Destroy(functionList [i]);
							break;
						}
					}
				}
			} else {
				if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish != null){
					string[] obsoletes = GameController.Instance.level0Obsoletes;
					for (int i = 0; i < functionList.Count; i++) {
						foreach (string obsolete in obsoletes) {
							if (functionList [i].transform.name == obsolete) {
								Destroy(functionList [i]);
								break;
							}
						}
					}
				}
			}
		}
	}

	public void Clear(){
		functionList.Clear ();
		for (int i = 0; i < transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	public void AddTo(){
		if (station.GetComponent<Station> ().dish != null) {
			station.GetComponent<Station> ().AddTo ();
		}
	}

	public void PickUp(){
		if (station.GetComponent<Station> ().dish != null) {
			station.GetComponent<Station> ().PickUp ();
		}
	}

	public void PutDown(){
		if (station.GetComponent<Station> ().dish == null) {
			station.GetComponent<Station> ().PutDown ();
		}
	}

	public void Discard(){
		if (station.GetComponent<Station> ().dish != null) {
			GameController.Instance.RemoveUIDish (station.GetComponent<Station> ().dish);
		}
	}

	public void Taste(){
		string response = "Mmmmmmm that's pretty guud!";
		Ingredients food = station.GetComponent<Station> ().dish.GetComponent<Ingredients> ();
		// if ingredient was Red keep track of the processes that burnt it and randomly have a chance to say that instead of doing anything below
		//"Something in this was badly overdone"
		//"Oh no, we did not finish something in this."

		if (!food.isCooking && (GameController.Instance.time > 30f || GameController.Instance.Level != 5)) {
			ProcessDescriptor process;
			if (food.actionDone != "") {
				process = CookingController.Instance.GetProcess (food.actionDone);
			} else {
				process = CookingController.Instance.GetProcess ("Fry");
			}

			if (food.howCooked.Count == 0) {
				food.howCooked.Add ("URed");
			}

			if (tasteClass == null) {
				tasteClass = new TasteTesting (food.flavor.x, food.flavor.y, food.howCooked [food.howCooked.Count - 1], process.IdealTimeMin, process.IdealTimeMax, food.howRaw);
			} else {
				tasteClass.updateAll (food.flavor.x, food.flavor.y, food.howCooked [food.howCooked.Count - 1], process.IdealTimeMin, process.IdealTimeMax, food.howRaw);
			}


			if (food.actionDone != "") {
				response = tasteClass.tasteTest ();
			} else if (food.actionDone == "") {
				response = tasteClass.tasteHeat ();
			}
		} else if (food.isCooking) {
			string howDone = food.SortBoundaries (food.actionDone, food.howRaw);
			if (howDone == "URed") {
				response = "Not at all ready yet. Best let it be for now.";
			} else if (howDone == "UYellow") {
				response = "Needs a little more time.";
			} else if (howDone == "Green") {
				response = "This is well done. I should take it off.";
			} else if (howDone == "DarkGreen") {
				response = "It's perfect! Take it off now!";
			} else if (howDone == "OYellow") {
				response = "This is overdone. We should take it off.";
			} else if (howDone == "ORed") {
				response = "Disastrously overdone!";
			}
		}else {
			response = "We don't have much time to waste!";
		}

		if (GameController.Instance.tutorial1Part1) {
			GameController.Instance.Invoke("NextTutorialStep", GameController.Instance.dialogueTime);
		}

		if (GameController.Instance.tutorial1Part3) {
			if (food.name == "Spinach") {
				GameController.Instance.spinachTasted = true;
				GameController.Instance.Invoke("NextTutorialStep", GameController.Instance.dialogueTime);
			} 
		}

		if (GameController.Instance.tutorial1Part5) {
			if (food.name == "Rice") {
				GameController.Instance.MakeTutorialeBox ("Cooking processes normally only change the flavor a little, depending on how well-done they were. The closer to an ideal time, the greater the flavor change. Adding ingredients, on the other hand, usually changes the flavor of a mixture in a slightly stronger, but more predictable manner.");
				GameController.Instance.NextTutorialStep ();
			}
		}

		GameController.Instance.MakeDialogueBox (response);
	}

	bool tut0p3 = false;
	public void Do(Button clickedButton){
		if (GameController.Instance.tutorial0Part22 && !GameController.Instance.timerOn) {
			GameController.Instance.MakeTutorialeBox ("My lord will be here soon! I’ve hung a bell in the window to indicate when you can serve him. When the sun reaches it, it will be time. Try adding some more ingredients or mixtures to the chicken, and don’t forget to fry it.");
			GameController.Instance.time = GameController.Instance.course1time * .5f;
			GameController.Instance.timerOn = true;
		} 
		if (GameController.Instance.tutorial0Part3 && !tut0p3) {
			GameController.Instance.MakeTutorialeBox ("What kind of dish you are making is determined by its first ingredient and the first process used. So for example, our Pounded Chicken mixture from before became Boulettes. When you add a mixture to another, the new mixture is treated as an ingredient added to the base mixture. Remember: you cannot serve a dish without having processed it in some way at least once.");
			tut0p3 = true;
		}

		if (GameController.Instance.tutorial1Part1) {
			if (clickedButton.name == "Roast") {
				GameController.Instance.MakeTutorialeBox ("Because you are not Aemilia yourself, you cannot directly control how well she cooks or see how well-done the mixture seems to her. You can, however, taste test her Almonds yourself by clicking on them, even as she cooks.");
			}
		}
		if (GameController.Instance.tutorial1Part3) {
			if (clickedButton.name == "Seethe") {
				GameController.Instance.MakeTutorialeBox ("Notice that seething does not trigger a timer. As a Passive process, it does not need your attention, so you can do other things while it cooks. Just don’t forget to Taste Test it and Pick it Up, or it will burn. For now, though feel free to cut up the Spinach for the Salad and add some other ingredients to it.");
			}
		}

		if (GameController.Instance.tutorial1Part4) {
			if (clickedButton.name == "Seethe") {
				GameController.Instance.MakeTutorialeBox ("Cooking food in different ways changes its flavor. seething, for example, makes it taste more moist and slightly hotter. Mincing, on the other hand, makes your mixture a taste a bit colder and more moist. You can see a process’ flavors under their menu listing when you go to process a mixture. Go ahead and taste it when you’re done.");
			}
		}
		if (!GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterMovement> ().isCooking) {
			GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().selectedAction = true;
			if (clickedButton.name == "PutDown") {
				if (station.name == "Mortar" || station.name == "Range") {
					GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<SpriteRenderer> ().sortingOrder = 2;
				} else {
					GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<SpriteRenderer> ().sortingOrder = 5;
				}
				PutDown ();
			} else if (clickedButton.name == "Taste") {
				Taste ();
			} else if (clickedButton.name == "Discard") {
				Discard ();
			} else if (clickedButton.name == "PickUp") {
				PickUp ();
				if (station.name == "Mortar" || station.name == "Range") {
					GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<SpriteRenderer> ().sortingOrder = 5;
				}
			} else if (clickedButton.name == "AddTo") {
				if (GameController.Instance.tutorial3Part2) {
					if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.name == "Hemlock"){
						GameController.Instance.MakeTutorialeBox("*It is done. If the dish is still flavored and cooked to Lady Anna’s preference, I can send it out to remove her as an obstacle.*");
					}
				}
				AddTo ();
			} else if (station.GetComponent<Station>().dish == null){
				if (CookingController.Instance.GetProcess(clickedButton.name).SupportsIngredient (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().primaryIngredient)) {
					station.GetComponent<Station> ().Cook (clickedButton.name);
					PutDown ();
				} else {
				}
			} else if (station.GetComponent<Station>().dish != null){
				if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish == null) {
					if (CookingController.Instance.GetProcess (clickedButton.name).SupportsIngredient (station.GetComponent<Station> ().dish.GetComponent<Ingredients> ().primaryIngredient)) {
						station.GetComponent<Station> ().Cook (clickedButton.name);
					}
				}
			}
		}else if (clickedButton.name == "PickUp") {
			PickUp ();
		}
	}

	public void Finish(){
		Debug.Log ("Finish");

	}
}
