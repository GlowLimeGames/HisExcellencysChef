using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float speed = 5f;

	public bool isMoving = false;
	public bool isCooking = false;

	private bool toStation = false;
	private GameObject atStation;

	public void Move(Vector2 position, GameObject clickedStation){
		if (!isMoving && !isCooking) {
			position = clickedStation.transform.position;
			StopAllCoroutines ();
			StartCoroutine ("MoveTo", position);
			if (clickedStation != null) {
				toStation = true;
				atStation = clickedStation;
			}
		}
	}

	IEnumerator MoveTo (Vector2 position) {
		isMoving = true;
		while (Vector2.Distance(transform.position, position) > 0.1f) {
			transform.position = Vector2.MoveTowards (transform.position, position, speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}
		if (toStation) {
			atStation.GetComponent<Station> ().Clicked (transform.gameObject);
			toStation = false;
		} else {
			isMoving = false;
		}
		//if clicked on station
		//station -> attached dropdown 
		//open dropdown.setactive
	}
}
