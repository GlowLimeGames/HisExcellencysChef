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
				process = "PutDown";
				GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
				aButton.transform.SetParent (transform);
				aButton.transform.Rotate (Vector3.zero);
				aButton.name = process;
				aButton.GetComponentInChildren<Text> ().text = process;
				functionList.Add (aButton);
			}
			if (station.GetComponent<Station> ().dish != null) {
				process = "AddTo";
				GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
				aButton.transform.SetParent (transform);
				aButton.transform.Rotate (Vector3.zero);
				aButton.name = process;
				aButton.GetComponentInChildren<Text> ().text = process;
				functionList.Add (aButton);
			}
		} else {
			if (station.GetComponent<Station> ().dish != null) {
				process = "PickUp";
				GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
				aButton.transform.SetParent (transform);
				aButton.transform.Rotate (Vector3.zero);
				aButton.name = process;
				aButton.GetComponentInChildren<Text> ().text = process;
				functionList.Add (aButton);
			}
		}
		if (station.GetComponent<Station> ().dish != null) {
			process = "Discard";
			GameObject aButton  = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
			aButton.transform.SetParent (transform);
			aButton.transform.Rotate (Vector3.zero);
			aButton.name = process;
			aButton.GetComponentInChildren<Text> ().text = process;
			functionList.Add (aButton);
		}

		//Make Discard Function here

		//Chef only Functions
		if (GameObject.Find("ClickHandler").GetComponent<ClickHandler>().currentCharacter.name == "Cook"){
			if (station.GetComponent<Station> ().dish != null) {
				process = "Taste";
				GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
				aButton.transform.SetParent (transform);
				aButton.transform.Rotate (Vector3.zero);
				aButton.name = process;
				aButton.GetComponentInChildren<Text> ().text = process;
				functionList.Add (aButton);
			}
		}
		//Make Buttons based on Processes
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

	void ShowIngredientProcesses(GameObject clickedObject){

		string[] processes = new string[0];

		if (station.GetComponent<Station> ().dish != null) {
			processes = station.GetComponent<Station> ().dish.GetComponent<Ingredients> ().primaryIngredient.Processes;
		} else {
			if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish != null){
				processes = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().primaryIngredient.Processes;
			}
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
		//Check dish find obsoletes and gray out
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

		if (GameController.Instance.time > 30f) {
			ProcessDescriptor process;
			if (food.actionDone != "") {
				process = CookingController.Instance.GetProcess (food.actionDone);
			} else {
				process = CookingController.Instance.GetProcess ("Fry");
			}

			if (tasteClass == null) {
				tasteClass = new TasteTesting (food.flavor.x, food.flavor.y, food.howCooked, process.IdealTimeMin, process.IdealTimeMax, food.howRaw);
			} else {
				tasteClass.updateAll (food.flavor.x, food.flavor.y, food.howCooked, process.IdealTimeMin, process.IdealTimeMax, food.howRaw);
			}


			if (food.actionDone != "") {
				response = tasteClass.tasteTest ();
			} else if (food.actionDone == "") {
				response = tasteClass.tasteHeat ();
			}
		} else {
			response = "We don't have much time to waste!";
		}

		GameController.Instance.MakeDialogueBox (response);
	}

	public void Do(Button clickedButton){
		if (!GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterMovement> ().isCooking) {
			GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().selectedAction = true;
			if (clickedButton.name == "PutDown") {
				PutDown ();
			} else if (clickedButton.name == "Taste") {
				Taste ();
			} else if (clickedButton.name == "Discard") {
				Discard ();
			} else if (clickedButton.name == "PickUp") {
				PickUp ();
			} else if (clickedButton.name == "AddTo") {
				AddTo ();
			} else if (station.GetComponent<Station>().dish == null){
				if (CookingController.Instance.SupportsAction (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().primaryIngredientName, clickedButton.name)) {
					station.GetComponent<Station> ().Cook (clickedButton.name);
					PutDown ();
				}
			} else if (station.GetComponent<Station>().dish != null){
				if (CookingController.Instance.SupportsAction (station.GetComponent<Station>().dish.GetComponent<Ingredients> ().primaryIngredientName, clickedButton.name)) {
					station.GetComponent<Station> ().Cook (clickedButton.name);
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
