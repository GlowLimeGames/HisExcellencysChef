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
	public GameObject character;
	public string newFood;


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
			RefreshFlavor ();
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
			controller.TryModifyWithAction(primaryIngredient, actionName);
			primaryIngredientName = controller.Result(primaryIngredientName, actionName);
		}
		return wasSuccessful;
	}

    public void cookSelf(string action, GameObject activeChar)
    {
		Countdown (action, activeChar);
    }

	void Countdown(string action, GameObject activeChar)
    {
        if (!isCooking)
        {
			character = activeChar;
            StartCoroutine("CookTimeStartGo", action);
        }
    }

	void RefreshFlavor(){
		flavor.x = primaryIngredient.TasteHeat;
		flavor.y = primaryIngredient.TasteMoisture;
	}

    IEnumerator CookTimeStartGo(string action)
    {
		character.GetComponent<CharacterMovement> ().isCooking = true;
		isCooking = true;
        float percentComplete = 0f;
		TryPerformAction (action);
        while (percentComplete <= 1)
        {
            percentComplete += Time.deltaTime / cookTime;
			//Progress bar + change flavor +/- based on underling
            yield return null;
        }
		Debug.Log ("Ding");
		RefreshFlavor ();
		character.GetComponent<CharacterMovement> ().isCooking = false;
        isCooking = false;
		GetComponent<SpriteRenderer> ().color = new Color32 (150, 150, 150, 255);
		//change icon to new one
    }

}
