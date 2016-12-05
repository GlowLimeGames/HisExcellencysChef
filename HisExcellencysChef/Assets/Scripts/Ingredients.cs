/*
 * Author(s): Joel Esquilin, Isaiah Mann, Paul Calande
 * Description:
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ingredients : MannBehaviour {
	public int numI= 4; //four ingredients that we have listed so far
	CookingController controller;

    //[System.NonSerialized]
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
        Debug.Log("Ingredients.cs: SetReferences(): Why is this function not getting called by the prefab instances in food[] in InventoryUI.cs?");
        //Debug.Log("Ingredients.cs: "+transform.name);
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

	public void RefreshFlavor(){
		flavor.x = primaryIngredient.TasteHeat;
		flavor.y = primaryIngredient.TasteMoisture;
	}

    IEnumerator CookTimeStartGo(string action)
    {
        character.GetComponent<CharacterMovement>().isCooking = true;
        isCooking = true;
        float percentComplete = 0f;
        TryPerformAction(action);
        while (percentComplete <= 1)
        {
            percentComplete += Time.deltaTime / cookTime;
            //Progress bar + change flavor +/- based on underling
            yield return null;
        }
        Debug.Log("Ding");
        RefreshFlavor();
        character.GetComponent<CharacterMovement>().isCooking = false;
        isCooking = false;
        GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
        //change icon to new one
    }

    public string HeatToString()
    {
        int swish = (int)flavor.x;
        switch (swish)
        {
            case -4: return "Very Cold";
            case -3: return "Cold";
            case -2: return "Quite Cool";
            case -1: return "Cool";
            case 0: return "";
            case 1: return "Warm";
            case 2: return "Very Warm";
            case 3: return "Hot";
            case 4: return "Very Hot";
        }
        Debug.LogError("Ingredient has invalid heat " + swish + "; cannot convert to string.");
        return "ERROR";
    }

    public string MoistureToString()
    {
        int swish = (int)flavor.y;
        switch (swish)
        {
            case -4: return "Wet";
            case -3: return "Very Moist";
            case -2: return "Moist";
            case -1: return "Damp";
            case 0: return "";
            case 1: return "Arid";
            case 2: return "Dry";
            case 3: return "Very Dry";
            case 4: return "Parched";
        }
        Debug.LogError("Ingredient has invalid moisture " + swish + "; cannot convert to string.");
        return "ERROR";
    }

    public string HeatAndMoistureToString()
    {
        int heat = (int)flavor.x;
        int moisture = (int)flavor.y;
        // If both heat and moisture are 0...
        if (heat == 0 && moisture == 0)
        {
            return "Balanced";
        }
        else
        {
            if (heat == 0)
            {
                return MoistureToString();
            }
            else if (moisture == 0)
            {
                return HeatToString();
            }
            else
            {
                return HeatToString() + " and " + MoistureToString();
            }
        }
    }
}
