using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Station : MonoBehaviour {
    public GameObject dish; //the dishes at the Station
	public GameObject dropDown;
	public GameObject activeCharacter;

	public void Clicked(){
		if (transform.name == "ServeItForth") {
			activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
			if (activeCharacter.GetComponent<CharacterProperties> ().heldDish != null)
			{
				Vector2 flavor = activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().flavor;
				GameController.Instance.courseFlavor += flavor;
				Destroy (activeCharacter.GetComponent<CharacterProperties> ().heldDish);
			}
			return;
		}
		dropDown.SetActive (true);
		activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
		if (GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().activeDropdown == null) {
			GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().activeDropdown = this.gameObject;
		}
	}

	public void Cancel(){
		if (dropDown == null) {
			activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
			activeCharacter.GetComponent<CharacterMovement> ().isMoving = false;
			return;
		}

		if (dropDown.GetComponent<Dropdown> ().value != 0) {
			activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
			activeCharacter.GetComponent<CharacterMovement> ().isMoving = false;
			dropDown.GetComponent<Dropdown> ().value = 0;
		} else {
			activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
			activeCharacter.GetComponent<CharacterMovement> ().isMoving = false;
		}
	}

    public void AddTo()
    {
		if (activeCharacter.GetComponent<CharacterProperties> ().heldDish != null)
		{
			if (dish != null) {
				if (dish.GetComponent<Ingredients> ().TryAddIngredient (activeCharacter.GetComponent<CharacterProperties> ().heldDish.name)) {					Vector2 flavor = activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().flavor;
					dish.GetComponent<Ingredients> ().RefreshFlavor ();
					Destroy (activeCharacter.GetComponent<CharacterProperties> ().heldDish);
				}
			}
		}

		Cancel ();
    }

    public void PickUp()
    {
		if (activeCharacter.GetComponent<CharacterProperties> ().heldDish == null) {
			dish.transform.parent = activeCharacter.transform;
			dish.transform.position = activeCharacter.transform.position + new Vector3(0, 1.5f, 0);
			activeCharacter.GetComponent<CharacterProperties> ().heldDish = dish;
			dish = null;
			dropDown.GetComponent<StationDropdown> ().ChangeOptions (false);
		}

		Cancel ();
    }

    public void PutDown()
    {
		if (activeCharacter.GetComponent<CharacterProperties> ().heldDish != null) {
			activeCharacter.GetComponent<CharacterProperties> ().heldDish = null;
			dish = activeCharacter.transform.GetChild (0).gameObject;
			dish.transform.parent = transform;
			dish.transform.position = transform.position;
			dropDown.GetComponent<StationDropdown> ().ChangeOptions (true);
		}

		Cancel ();
    }

	public void Cook(string action){
		if (activeCharacter.GetComponent<CharacterMovement> ().isCooking == false) {
			dish.GetComponent<Ingredients> ().cookSelf (action, activeCharacter);
		}

		Cancel ();
	}
        
}
