using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StationDropdown : MonoBehaviour {
	public GameObject station;
	public GameObject functionButton;

	public bool active = false;
	List<GameObject> functionList = new List<GameObject> ();
	string[] dropDownOptions;
	// Use this for initialization
	void Start () {
		transform.parent.gameObject.SetActive (false);
	}

	public void OnClicked(GameObject clickedObject){
		if (clickedObject.tag == "Station") {
			active = true;
			station = clickedObject;
			dropDownOptions = GetPossibleFunctions (clickedObject);
			MakePossibleFunctions (clickedObject);
			BlockOutObsoletes (clickedObject);
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
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
		if (clickedObject.name == "Range" || clickedObject.name == "Table") {
			if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterProperties> ().heldDish != null) {
				process = "PutDown";
				GameObject aButton = (GameObject) Instantiate (functionButton,transform.position,Quaternion.identity);
				aButton.transform.SetParent(transform);
				aButton.transform.Rotate(Vector3.zero);
				aButton.name = process;
				functionList.Add (aButton);
				if (station.GetComponent<Station> ().dish != null) {
					process = "AddTo";
					aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
					aButton.transform.SetParent (transform);
					aButton.transform.Rotate (Vector3.zero);
					aButton.name = process;
					functionList.Add (aButton);
				}
			} else {
				if (station.GetComponent<Station> ().dish != null) {
					process = "PickUp";
					GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
					aButton.transform.SetParent (transform);
					aButton.transform.Rotate (Vector3.zero);
					aButton.name = process;
					functionList.Add (aButton);
				}
			}
			if (station.GetComponent<Station> ().dish != null) {
				process = "Discard";
				GameObject aButton  = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
				aButton.transform.SetParent (transform);
				aButton.transform.Rotate (Vector3.zero);
				aButton.name = process;
				functionList.Add (aButton);
			}
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
				functionList.Add (aButton);
			}
		} else if (station.GetComponent<Station> ().dish != null) {
			foreach (string aProcess in dropDownOptions) {
				GameObject aButton = (GameObject)Instantiate (functionButton, transform.position, Quaternion.identity);
				aButton.transform.SetParent (transform);
				aButton.transform.Rotate (Vector3.zero);
				aButton.name = aProcess;
				functionList.Add (aButton);
			}
		}
	}

	void BlockOutObsoletes(GameObject clickedObject){
		//Check dish find obsoletes and gray out
		if (station.GetComponent<Station> ().dish != null) {
			string[] obsoletes;
			obsoletes = station.GetComponent<Station> ().dish.GetComponent<Ingredients> ().obsoletes;
			foreach (string obsolete in obsoletes) {
				for (int i = 0; i < functionList.Count; i++) {
					if (functionList [i].transform.name == obsolete) {
						functionList.RemoveAt (i);
						break;
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

	public void Taste(){

	}

	public void Discard(){

	}

	public void Do(Button clickedButton){
		if (!GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter.GetComponent<CharacterMovement> ().isCooking) {
			if (clickedButton.name == "PutDown") {
				PutDown ();
			} else if (clickedButton.name == "Taste") {
				Taste ();
			} else if (clickedButton.name == "Discard") {
				Discard ();
			} else if (clickedButton.name == "PickUp") {
				PickUp ();
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
