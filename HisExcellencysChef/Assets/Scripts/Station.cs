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
		dropDown.GetComponent<StationDropdown> ().OnClicked (this.gameObject);
		activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
		GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().activeDropdown = this.gameObject;
	}

	public void Cancel(){
//		activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
		dropDown.GetComponent<StationDropdown>().Clear();
		GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().activeDropdown = null;
		dropDown.transform.parent.gameObject.SetActive(false);
	}

    public void AddTo()
    {
		if (activeCharacter.GetComponent<CharacterProperties> ().heldDish != null)
		{
			if (dish != null) {
				if (dish.GetComponent<Ingredients> ().TryAddIngredient (activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients>().primaryIngredientName)) {					
//				Vector2 flavor = activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().flavor;
				dish.GetComponent<Ingredients> ().TryAddIngredient(activeCharacter.GetComponent<CharacterProperties> ().heldDish.name);
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
			if (dish.GetComponent<Ingredients> ().isCooking) {
				dish.GetComponent<Ingredients> ().isCooking = false;
				dish.GetComponent<Ingredients> ().ChangeFlavor();
				dish.GetComponent<Ingredients> ().StopAllCoroutines ();
			}
			dish.transform.parent = activeCharacter.transform;
			dish.transform.position = activeCharacter.transform.position + new Vector3(0, 0, 3f);
			activeCharacter.GetComponent<CharacterProperties> ().heldDish = dish;
			activeCharacter.GetComponent<CharacterMovement> ().isCooking = false;
			dish = null;
		}

		Cancel ();
    }

    public void PutDown()
    {
		if (activeCharacter.GetComponent<CharacterProperties> ().heldDish != null) {
			activeCharacter.GetComponent<CharacterProperties> ().heldDish = null;
			dish = activeCharacter.transform.GetChild (1).gameObject;
			dish.transform.parent = transform;
			dish.transform.position = transform.position;
		}

		Cancel ();
    }

	public void Cook(string action){
		if (activeCharacter.GetComponent<CharacterMovement> ().isCooking == false) {
			if (dish == null) {
				activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().cookSelf (action, activeCharacter);
			} else {
				dish.GetComponent<Ingredients> ().cookSelf (action, activeCharacter);
			}
		}

		Cancel ();
	}
        
}
