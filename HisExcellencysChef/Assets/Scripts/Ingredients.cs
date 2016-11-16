using UnityEngine;
using System.Collections;

public class Ingredients : MonoBehaviour {
	public int numI= 4; //four ingredients that we have listed so far

    public string foodName;
	public float cookTime;
    public Vector2 flavor;

    public bool isCooking;

	// Use this for initialization
	void Start () {
		foodName = transform.name;
	}


    public void cookSelf(string tag , GameObject character)
    {
		//Countdown ("", character);

		switch (tag){
		case "Pound":
			if (foodName == "Chicken") {
			}
			if (foodName == "Mustard") {
			}
			if (foodName == "Almonds") {
			}
			break;

		case "Chop":
			if (foodName == "Spinach") {
			}
			break;

		case "Sliver":
			if (foodName == "Chicken") {
				
			}
			if (foodName == "Almonds") {
			}
			if (foodName == "Lamprey") {
			}
			break;

		case "Mince":
			if (foodName == "Spinach") {
			}
			if (foodName == "Chicken") {
			}
			break;

		case "Fry":
			if (foodName == "Spinach") {
			}
			if (foodName == "Chicken") {
			}
			if (foodName == "Eggs") {
			}
			if (foodName == "Mustard") {
			}
			if (foodName == "Lamprey") {
			}
			break;

		case "Seethe":
			if (foodName == "Spinach") {
			}
			if (foodName == "Chicken") {
			}
			if (foodName == "Eggs") {
			}
			if (foodName == "Almonds") {
			}
			if (foodName == "Rice") {
			}
			break;

		case "Roast":
			if (foodName == "Chicken") {
			}
			if (foodName == "Almonds") {
			}
			if (foodName == "Lamprey") {
			}
			break;

		case "Bake":
			if (foodName == "Wheat Flour") {
			}
			if (foodName == "Lamprey") {
			}
			break;

		case "Boil":
			if (foodName == "Water") {
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
