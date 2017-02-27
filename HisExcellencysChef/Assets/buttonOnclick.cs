using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonOnclick : MonoBehaviour {

	Button button;
	public void Start(){
		button = GetComponent<Button> ();
		button.onClick.AddListener (Do);
	}

	public void Do(){
		StationDropdown dropdown = GetComponentInParent<StationDropdown> ();
		dropdown.Do (button);
	}

}
