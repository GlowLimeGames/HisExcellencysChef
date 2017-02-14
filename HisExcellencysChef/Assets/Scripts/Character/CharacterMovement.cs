using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour {

	public float speed = 5f;

	public bool isCooking = false;

	private NavMeshAgent nav;
	private bool toStation = false;
	private GameObject atStation;

	void Awake(){
		nav = GetComponent<NavMeshAgent> ();
	}

	public void Move(Vector3 position, GameObject clickedObject){
		if (!isCooking) {
			if (clickedObject.name != "Ground") {
				if (atStation != null && clickedObject == atStation) {
					atStation.GetComponent<Station> ().Clicked ();
					return;
				}
			}
			Cancel ();
			StartCoroutine ("MoveTo", position);
			if (clickedObject.name != "Ground") {
				toStation = true;
				atStation = clickedObject;
			}
		}
	}

	public void Cancel(){
		if (!isCooking){
			StopAllCoroutines ();
			nav.Stop ();
		}
	}

	IEnumerator MoveTo (Vector3 position) {
		nav.destination = position;
		nav.Resume ();

		if (toStation) {
			while (Vector3.Distance(atStation.transform.position, transform.position) > nav.stoppingDistance) {
				yield return new WaitForFixedUpdate();
			}	
			atStation.GetComponent<Station> ().Clicked ();
			toStation = false;
		}
		//if clicked on station
		//station -> attached dropdown 
		//open dropdown.setactive
	}
}
