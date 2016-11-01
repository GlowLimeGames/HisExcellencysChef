using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tabledropdown : MonoBehaviour {
	public Dropdown menu;
	// Use this for initialization
	void Start () {
		menu = GetComponent<Dropdown> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CheckSelectedBox(){
		
		//menu.value if value == 0 do function 0
		if (menu.value == 0) {
			PutDown ();
		} else if (menu.value == 1) {
			PickUp ();
		} else if (menu.value == 2) {
			AddTo ();
		}

		gameObject.SetActive (false);
	}

	public void AddTo(){
		Debug.Log ("AddTo");
	}

	public void PickUp(){
		Debug.Log ("PickUp");
	}

	public void PutDown(){
		Debug.Log ("PutDown");
	}
}
