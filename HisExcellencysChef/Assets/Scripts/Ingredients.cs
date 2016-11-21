/*
 * Author(s): Joel Esquilin, Isaiah Mann
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ingredients : MannBehaviour {
	public int numI= 4; //four ingredients that we have listed so far
	CookingController controller;

    public string primaryIngredientName;

	IngredientDescriptor primaryIngredient;
	List<IngredientDescriptor> addOnIngridents = new List<IngredientDescriptor>();
	Dictionary<string, IngredientDescriptor> ingredientLookup;

	public float cookTime;
    public Vector2 flavor;

    public bool isCooking;

	// Use this for initialization
	protected override void SetReferences () {
		primaryIngredientName = transform.name;
	}

	protected override void FetchReferences () {
		controller = CookingController.Instance;
		ingredientLookup = controller.Ingredients;
		setPrimaryIngredient();
	}

	void setPrimaryIngredient () {
		IngredientDescriptor primaryIngredient;
		if (ingredientLookup.TryGetValue(primaryIngredientName, out primaryIngredient)) {
			this.primaryIngredient = primaryIngredient;
		} else {
			Debug.LogErrorFormat("Primary ingredient {0} DNE in Lookup", primaryIngredientName);
		}
	}

	protected override void CleanupReferences () {	
		// NOTHING
	}

	protected override void HandleNamedEvent (string eventName) {
		// NOTHING
	}

	public bool TryAddIngredient (string ingredientName) {
		IngredientDescriptor ingredient;
		bool wasSuccessful;
		if (wasSuccessful = ingredientLookup.TryGetValue(ingredientName, out ingredient)) {
			addOnIngridents.Add(ingredient);
			primaryIngredient.ModifyWithAddOn(ingredient);
		}
		return wasSuccessful;
	}

	public bool TryPerformAction (string actionName) {
		bool wasSuccessful;
		if (wasSuccessful = controller.SupportsAction(primaryIngredientName, actionName)) {
			primaryIngredientName = controller.Result(primaryIngredientName, actionName);
		}
		return wasSuccessful;
	}

    public void cookSelf(string tag , GameObject character)
    {
		//Countdown ("", character);

		switch (tag){
		case "Pound":
			if (primaryIngredientName == "Chicken") {
			}
			if (primaryIngredientName == "Mustard") {
			}
			if (primaryIngredientName == "Almonds") {
			}
			break;

		case "Chop":
			if (primaryIngredientName == "Spinach") {
			}
			break;

		case "Sliver":
			if (primaryIngredientName == "Chicken") {
				
			}
			if (primaryIngredientName == "Almonds") {
			}
			if (primaryIngredientName == "Lamprey") {
			}
			break;

		case "Mince":
			if (primaryIngredientName == "Spinach") {
			}
			if (primaryIngredientName == "Chicken") {
			}
			break;

		case "Fry":
			if (primaryIngredientName == "Spinach") {
			}
			if (primaryIngredientName == "Chicken") {
			}
			if (primaryIngredientName == "Eggs") {
			}
			if (primaryIngredientName == "Mustard") {
			}
			if (primaryIngredientName == "Lamprey") {
			}
			break;

		case "Seethe":
			if (primaryIngredientName == "Spinach") {
			}
			if (primaryIngredientName == "Chicken") {
			}
			if (primaryIngredientName == "Eggs") {
			}
			if (primaryIngredientName == "Almonds") {
			}
			if (primaryIngredientName == "Rice") {
			}
			break;

		case "Roast":
			if (primaryIngredientName == "Chicken") {
			}
			if (primaryIngredientName == "Almonds") {
			}
			if (primaryIngredientName == "Lamprey") {
			}
			break;

		case "Bake":
			if (primaryIngredientName == "Wheat Flour") {
			}
			if (primaryIngredientName == "Lamprey") {
			}
			break;

		case "Boil":
			if (primaryIngredientName == "Water") {
			}
			break;
		}
    }

    void Countdown(string newFood, GameObject character)
    {
        if (!isCooking)
        {
            StartCoroutine("CookTimeStartGo", character);
        }
    }

    IEnumerator CookTimeStartGo(GameObject character)
    {
		character.GetComponent<CharacterMovement> ().isCooking = true;
		isCooking = true;
        float percentComplete = 0f;
        while (percentComplete <= 1)
        {
            percentComplete += Time.deltaTime / cookTime;

            yield return null;
        }
        Debug.Log("Ding");
		character.GetComponent<CharacterMovement> ().isCooking = false;
        isCooking = false;
		GetComponent<SpriteRenderer> ().color = new Color32 (150, 150, 150, 255);
		//Instantiate(newFood);
        //Destroy(thisFood)

    }

    public void Update() {
    }
}
