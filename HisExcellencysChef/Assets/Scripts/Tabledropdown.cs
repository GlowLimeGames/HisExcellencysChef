using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tabledropdown : MonoBehaviour {
	public Dropdown menu;
	public GameObject station;
	// Use this for initialization
	void Start () {
		menu = GetComponent<Dropdown> ();
		transform.gameObject.SetActive (false);
	}

	public void CheckSelectedBox(){
		//menu.value if value == 0 do function 0
		if (menu.value == 1) {
			PutDown ();
		} else if (menu.value == 2) {
			PickUp ();
		} else if (menu.value == 3) {
			AddTo ();
		} else {
			station.GetComponent<Station> ().Cancel ();
		}
	}

	public void AddTo(){
		Debug.Log ("AddTo");
		if (station.GetComponent<Station> ().dish != null) {
			station.GetComponent<Station> ().AddTo ();
		} else {
			station.GetComponent<Station> ().Cancel ();
		}
	}

	public void PickUp(){
		Debug.Log ("PickUp");
		if (station.GetComponent<Station> ().dish != null) {
			station.GetComponent<Station> ().PickUp ();
		} else {
			station.GetComponent<Station> ().Cancel ();
		}
	}

	public void PutDown(){
		Debug.Log ("PutDown");
		if (station.GetComponent<Station> ().dish == null) {
			station.GetComponent<Station> ().PutDown ();
		} else {
			station.GetComponent<Station> ().Cancel ();
		}
	}
}
