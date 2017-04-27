	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderlingController : MonoBehaviour {
	
	Dictionary<string, UnderlingDescriptor> underlingLookup;
	public Slider slider;
	public float timer;

	CharacterMovement cm;
//	CharacterProperties cp;

	UnderlingDescriptor underling;
	void Start(){
		cm = GetComponent <CharacterMovement> ();
//		cp = GetComponent<CharacterProperties> ();
		underlingLookup = CookingController.Instance.Underlings;
		underlingLookup.TryGetValue (transform.name, out underling);
	}

	float GetRandomTime(string action){
		float time = underling.GetProcessAvg(action);
		float distance = underling.GetProcessDist (action);

		float finalTime = time + Random.Range (-distance, distance);

		if (finalTime < time - distance / 2) {
			finalTime += distance / 4;
		} else if (finalTime > time + distance / 2) {
			finalTime -= distance / 4;
		}

		return finalTime;
	}

	public void SetTimer(string action){
		slider.gameObject.SetActive (true);
		timer = GetRandomTime (action);
		slider.maxValue = timer;
	}

	// Update is called once per frame
	void Update () {
		if (cm != null) {
			if (cm.isCooking) {
				timer -= Time.deltaTime;
				slider.value = timer;
				if (timer < 0) {
					cm.atStation.GetComponent<Station> ().PickUp ();
					slider.gameObject.SetActive (false);
				}
			} else {
				if (slider.gameObject.activeInHierarchy) {
					slider.gameObject.SetActive (false);
				}
			}
		}
	}
}
