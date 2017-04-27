using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Station : MonoBehaviour {
    public GameObject dish; //the dishes at the Station
	public GameObject dropDown;
	public GameObject activeCharacter;

	public void Clicked(){
		if (transform.name == "ServeItForth") {
			if (GameController.Instance.canServe) {
				activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
				if (activeCharacter.GetComponent<CharacterProperties> ().heldDish != null
					&& activeCharacter.GetComponent<CharacterProperties>().heldDish.GetComponent<Ingredients>().actionsDone != null) {
					if (GameController.Instance.tutorial2Part2) {
						if (!GameController.Instance.GetComponent<GuestController> ().CheckPos3and4 (GameController.Instance.GetComponent<GuestController> ().guestList [0], activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ())) {
							return;
						} else {
							GameController.Instance.MakeTutorialeBox ("Wonderful. As you can see, dishes on your banner here are moved to the appropriate course section when served. Let’s click over the listing here to see what Anna thought.");
						}
					}
					Vector2 flavor = activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().flavor;
					if (GameController.Instance.course == 0) {
						GameController.Instance.courseFlavor0 += flavor;
						GameController.Instance.course1count += 1;
					} else if (GameController.Instance.course == 1) {
						GameController.Instance.courseFlavor1 += flavor;
						GameController.Instance.course2count += 1;
					} else if (GameController.Instance.course == 2) {
						GameController.Instance.courseFlavor2 += flavor;
						GameController.Instance.course3count += 1;
					}
					if (GameController.Instance.tutorial3Part2) {
						if (activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().ContainsIngredient ("Hemlock")) {
							GameController.Instance.tutorial3Part2 = false;
							GameController.Instance.tutorial3Part3 = true;
							GameController.Instance.Invoke ("NextTutorialStep", 5f);
						}
					}
					GameController.Instance.servedDishes.Add (activeCharacter.GetComponent<CharacterProperties> ().heldDish);
					activeCharacter.GetComponent<CharacterProperties> ().heldDish.SetActive(false);
					activeCharacter.GetComponent<CharacterProperties> ().heldDish = null;
					GameController.Instance.ModifyGuests ();
				}
				if (GameController.Instance.tutorial0Part22) {
					GameController.Instance.Invoke ("NextTutorialStep", 5f);
				}
				if (GameController.Instance.tutorial1Part3) {
					GameController.Instance.MakeTutorialeBox ("You have done as I asked. Let us continue.\n Since you clearly are aware of advanced concepts of flavors, let us experiment with them a little. I’ve added some new ingredients to your pantry. Go have a look at them.");
					GameController.Instance.tutorial1Part3 = false;
					GameController.Instance.tutorial1Part4 = true;
					GameController.Instance.time = 0;
					GameController.Instance.NextTutorialStep ();
				}


				return;
			}
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
				if (dish.GetComponent<Ingredients> ().TryAddIngredient (activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().primaryIngredientName)) {					
	//				Vector2 flavor = activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().flavor;
	//				dish.GetComponent<Ingredients> ().TryAddIngredient(activeCharacter.GetComponent<CharacterProperties> ().heldDish.name);
					dish.GetComponent<Ingredients> ().RefreshFlavor ();

					GameController.Instance.RemoveUIDish (activeCharacter.GetComponent<CharacterProperties> ().heldDish);
				}
			}
		}

		Cancel ();
    }

    public void PickUp()
    {
		if (activeCharacter.GetComponent<CharacterProperties> ().heldDish == null) {
			if (dish.GetComponent<Ingredients> ().isCooking) {
				dish.GetComponent<Ingredients> ().done = true;
				if (!GameController.Instance.tutorial0Part1) {
					dish.GetComponent<Ingredients> ().ChangeFlavor ();
				}
				dish.GetComponent<Ingredients> ().StopAllCoroutines ();
			}
			dish.transform.parent = activeCharacter.transform;
			dish.transform.position = activeCharacter.transform.position + new Vector3 (0, 0, 3f);
			activeCharacter.GetComponent<CharacterProperties> ().heldDish = dish;
			if (cookingCharacter != null) {
				cookingCharacter.GetComponent<CharacterMovement> ().isCooking = false;
				cookingCharacter = null;
			}
			dish = null;
			EventController.Event (Event.PICKUP);
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
			EventController.Event (Event.PUTDOWN);
		}

		Cancel ();
    }

	public GameObject cookingCharacter;
	public void Cook(string action){
		if (activeCharacter.GetComponent<CharacterMovement> ().isCooking == false) {
			if (dish == null) {
				activeCharacter.GetComponent<CharacterProperties> ().heldDish.GetComponent<Ingredients> ().cookSelf (action, activeCharacter,gameObject);
				cookingCharacter = activeCharacter;
			} else {
				cookingCharacter = activeCharacter;
				dish.GetComponent<Ingredients> ().cookSelf (action, activeCharacter,gameObject);
			}
			if (action == "Fry") {
				EventController.Event (Event.FRY);
				EventController.Event (Event.FRYLOOP);
			} else if (action == "Pound") {
				EventController.Event (Event.POUND);
			} else if (action == "Seethe") {
				EventController.Event (Event.SEETHE);
			} else if (action == "Sliver") {
				EventController.Event (Event.SLIVER);
			} else if (action == "Mince") {
				EventController.Event (Event.MINCE);
			} else if (action == "Bake") {
				EventController.Event (Event.BAKE);
			} else if (action == "Roast") {
				EventController.Event (Event.ROAST);
			} else if (action == "Boil") {
				EventController.Event (Event.BOIL);
			}
		}

		Cancel ();
	}
        
}
