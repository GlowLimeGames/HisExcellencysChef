using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour {

	public float speed = 5f;

	public bool isCooking = false;

	private NavMeshAgent nav;
//	private bool toStation = false;
	public GameObject atStation;

	CharacterProperties cp;

	void Awake(){
		cp = GetComponent<CharacterProperties> ();
		nav = GetComponent<NavMeshAgent> ();
	}

	public void Move(Vector3 position, GameObject clickedObject){
		if (!isCooking && !cp.atStation) {
			if (clickedObject.tag != "PlayerChar") {
				if (!cp.atStation) {
					Cancel ();
					nav.destination = position;
					nav.Resume ();
				}
			}
			if ((clickedObject.tag == "Station")) {
//				toStation = true;
				cp.atStation = true;
				atStation = clickedObject;
				atStation.GetComponent<Station> ().Clicked ();
			} 
		}
	}

	public void Cancel(){
		StopAllCoroutines ();
		nav.Stop ();
		cp.atStation = false;
		atStation = null;
	}

//	IEnumerator MoveTo (Vector3 position) {
//		nav.destination = position;
//		nav.Resume ();

//		if (toStation) {
//			while (Vector3.Distance(atStation.transform.position, transform.position) > nav.stoppingDistance) {
//				yield return new WaitForFixedUpdate();
//			}	
//			atStation.GetComponent<Station> ().Clicked ();
//			toStation = false;
//		}
		//if clicked on station
		//station -> attached dropdown 
		//open dropdown.setactive
//	}
}
