using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float speed = 5f;

	private bool isMoving = false;

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
