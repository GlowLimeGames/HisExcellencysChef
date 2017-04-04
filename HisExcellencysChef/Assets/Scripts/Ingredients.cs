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

    public string primaryIngredientName;

	public IngredientDescriptor primaryIngredient = new IngredientDescriptor();
	List<IngredientDescriptor> addOnIngridents = new List<IngredientDescriptor>();
	Dictionary<string, IngredientDescriptor> ingredientLookup;

	public float cookTime;
    public Vector2 flavor;

    public bool isCooking;
	public GameObject character;
	public string newFood;

	public List<string> howCooked = new List<string>();
	public List<string> actionsDone = new List<string>();
	public string[] obsoletes;
	public GameObject UIButton;

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
		IngredientDescriptor primary;
		if (ingredientLookup.TryGetValue(primaryIngredientName, out primary)){
			this.primaryIngredient = primary.Copy ();
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

	public bool TryPerformAction (string actionName, float howRaw) {
		bool wasSuccessful;
		if (wasSuccessful = controller.SupportsAction(primaryIngredientName, actionName)) {
			controller.TryModifyWithAction(primaryIngredient, actionName, howRaw);
//			primaryIngredientName = controller.Result(primaryIngredientName, actionName);
		}
		return wasSuccessful;
	}

    public void cookSelf(string action, GameObject activeChar)
    {
		//Keep track of obsolete processes and check them here
		foreach (string obsolete in obsoletes) {
			if (action == obsolete) {
				return;
			}
		}
		Countdown (action, activeChar);
    }

	void Countdown(string action, GameObject activeChar)
    {
		if (!isCooking) {
			character = activeChar;
			if (activeChar.GetComponent<CharacterProperties> ().isCook) {
				StartCoroutine ("CookTimeStartChef", action);
			} else {
				StartCoroutine ("CookTimeStartUnderling", action);
			}
		}
    }

	public void RefreshFlavor(){
		flavor.x = primaryIngredient.TasteHeat;
		flavor.y = primaryIngredient.TasteMoisture;
	}

	public string actionDone;
	public float howRaw = 0f;
	public float percentCooked = 0f;
	// Sorts color boundary and gives percent cooked changing taste accordingly
	public void ChangeFlavor(){
		percentCooked = PercentCooked(actionDone, howRaw); 
		TryPerformAction(actionDone, percentCooked);
		howCooked.Add(SortBoundaries (actionDone, howRaw));

		if (character.GetComponent<CharacterProperties> ().isCook) {
			GameController.Instance.chefSlider.gameObject.SetActive (false);
		}
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		sprite.color = new Color(.7f, .7f, .7f, 1);

		character.GetComponent<CharacterMovement>().Cancel();
		RefreshFlavor ();
		actionsDone.Add (actionDone);

		string [] newObsoletes = controller.GetProcess (actionDone).Obsoletes;
		string [] oldObsoletes = obsoletes;
//
//		for (int i = 0; i < newObsoletes.Length; i++) {
//			foreach (string function in oldObsoletes) {
//				if (newObsoletes [i] == function) {
//					newObsoletes [i] = "";
//				}
//			}
//		}

		int increaseSize = 0;
		foreach (string function in newObsoletes) {
			if (function != "") {
				increaseSize += 1;
			}
		}

		obsoletes = new string[oldObsoletes.Length + increaseSize];

		for (int i = 0; i < obsoletes.Length; i++) {
			if (i < oldObsoletes.Length) {
				obsoletes [i] = oldObsoletes [i];
				Debug.Log (oldObsoletes [i]);
			} else {
				for (int j = 0; j < newObsoletes.Length; j++) {
					if (newObsoletes[j] != "") {
						bool skip = false;
						foreach (string function in obsoletes) {
							if (function == newObsoletes [j]) {
								skip = true;
								break;
							}
						}
						if (!skip) {
							obsoletes [i] = newObsoletes [j];
							Debug.Log (newObsoletes [j]);
							break;
						}
					}
				}
			}
		}


		//Add Obsolete processes to ingredient
		//Add to side panel and store information before resetting
		//Change sprite if needed
		//Do things after cooking here
	}

	IEnumerator CookTimeStartChef(string action)
	{
		isCooking = true;
		float timeIn = 0f;
		bool done = false;
		bool active = controller.GetProcess (action).Active;
		if (active) {
			character.GetComponent<CharacterMovement> ().isCooking = true;
			character.GetComponent<CharacterProperties> ().atStation = true;
			GameController.Instance.EditSlider (action, this);
		}
//		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		actionDone = action;
			while (!done)
			{
			
			timeIn += Time.deltaTime;
			howRaw = timeIn; 
			if (active) {
				if (Input.GetMouseButtonUp (1)) {
					done = true;
				}
			}
//			sprite.color = new Color(1 - howRaw / 50 ,1 - howRaw / 50,1 - howRaw / 50, 1);
				//Progress bar + change flavor +/- based on underling
				yield return null;
			}
		ChangeFlavor ();
		RefreshFlavor();
		character.GetComponent<CharacterMovement>().isCooking = false;
		isCooking = false;
				//change icon to new one
	}

	IEnumerator CookTimeStartUnderling(string action)
	{
		isCooking = true;
		float timeIn = 0f;
		bool done = false;
		bool active = controller.GetProcess (action).Active;
		if (active) {
			character.GetComponent<CharacterMovement>().isCooking = true;
			character.GetComponent<UnderlingController> ().SetTimer (action);
		}
		actionDone = action;
//		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		while (!done)
		{

			timeIn += Time.deltaTime;
			howRaw = timeIn; 
//			sprite.color = new Color(1 - howRaw / 50 ,1 - howRaw / 50,1 - howRaw / 50, 1);
//			if (active) {
//				if (Input.GetMouseButtonUp (1)) {
//					done = true;
//				}
//			}
			//Progress bar + change flavor +/- based on underling
			yield return null;
		}
		ChangeFlavor ();
		RefreshFlavor();
		character.GetComponent<CharacterMovement>().isCooking = false;
		isCooking = false;
		//change icon to new one
	}

	float PercentCooked(string action, float timeCooked){
		ProcessDescriptor process = controller.GetProcess (action);

		if (timeCooked > process.IdealTimeMax) {
			return Mathf.Round (10f * (process.IdealTimeMax / timeCooked)) / 10f;
		} else if (timeCooked < process.IdealTimeMin) {
			return Mathf.Round (10f * (timeCooked / process.IdealTimeMin)) / 10f;
		} else {
			return 1f;
		}

	}
	string SortBoundaries(string action, float timeCooked){
		ProcessDescriptor process = controller.GetProcess (action);
		// U under, O over
		if (timeCooked < process.RedYellow) {
			return "URed";
		} else if (timeCooked < process.YellowGreen) {
			return "UYellow";
		} else if (timeCooked < process.GreenDarkGreen) {
			return "Green";
		} else if (timeCooked < process.DarkGreenGreen) {
			return "DarkGreen";
		} else if (timeCooked < process.GreenYellow) {
			return "Green";
		} else if (timeCooked < process.YellowRed) {
			return "OYellow";
		} else {
			return "ORed";
		}
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

    // For non-instantiated ingredients to get what they need.
    // This is necessary because Awake() is not called on uninstantiated objects.
    public void NonInstanceDoReferences()
    {
        SetReferences();
        SubscribeEvents();
        FetchReferences();
    }
}
