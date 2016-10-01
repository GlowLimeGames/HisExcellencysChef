using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float speed = 5f;
	public bool isSelected;

	private bool isMoving = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if (hit) {
				GameObject clickedObject = hit.collider.gameObject;
				Debug.Log (clickedObject.name + " was clicked.");
				if (isSelected && clickedObject.tag.Equals("Station")) {
					Move (hit.point);
					isSelected = false;
				} else if (clickedObject.name.Equals(this.name)) {
					isSelected = true;
				}
			}
		}

	}

	public void Move(Vector2 position){
		if (!isMoving) {
			StopAllCoroutines ();
			StartCoroutine ("MoveTo", position);
		}
	}

	IEnumerator MoveTo (Vector2 position) {
		isMoving = true;
		while (Vector2.Distance(transform.position, position) > 0.1f) {
			transform.position = Vector2.MoveTowards (transform.position, position, speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}
		isMoving = false;
	}

	
}
