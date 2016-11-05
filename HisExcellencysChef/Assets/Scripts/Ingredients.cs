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
	}

    public void cookSelf(string tag , GameObject character)
    {
		Countdown ("", character);
//        if (tag == "cut")
//        {
//            if (foodName == "Almond")
//            {
//                Countdown("newFoodName");
//            }
//			if (foodName == "Saffron")
//            {
//                
//            }
//			if (foodName== "")
//            {
//
//            }
//			if (foodName== "Water")
//            {
//
//            }
//        }
//
//        //add more functions for the various tags
//        
//        if (tag == "fry")
//        {
//			if (foodName == "Almond")
//            {
//                Countdown("newFoodName");
//            }
//			if (foodName == "Saffron")
//            {
//
//            }
//			if (foodName == "")
//            {
//
//            }
//			if (foodName == "Water")
//            {
//
//            }
//        }
//        if (tag == "bake")
//        {
//			if (foodName == "Almond")
//            {
//                Countdown("newFoodName");
//            }
//			if (foodName == "Saffron")
//            {
//
//            }
//			if (foodName == "")
//            {
//
//            }
//			if (foodName == "Water")
//            {
//
//            }
//        }
//        if (tag == "boil")
//        {
//			if (foodName == "Almond")
//            {
//                Countdown("newFoodName");
//            }
//			if (foodName == "Saffron")
//            {
//
//            }
//			if (foodName == "")
//            {
//
//            }
//			if (foodName == "Water")
//            {
//
//            }
//        }
//        if (tag == "sliver")
//        {
//			if (foodName == "Almond")
//            {
//                Countdown("newFoodName");
//            }
//			if (foodName == "Saffron")
//            {
//
//            }
//			if (foodName == "")
//            {
//
//            }
//			if (foodName == "Water")
//            {
//
//            }
//        }
//        if (tag == "pound")
//        {
//			if (foodName == "Almond")
//            {
//                Countdown("newFoodName");
//            }
//			if (foodName == "Saffron")
//            {
//
//            }
//			if (foodName == "")
//            {
//
//            }
//			if (foodName == "Water")
//            {
//
//            }
//        }
//        if (tag == "press")
//        {
//			if (foodName == "Almond")
//            {
//                Countdown("newFoodName");
//            }
//			if (foodName == "Saffron")
//            {
//
//            }
//			if (foodName == "")
//            {
//
//            }
//			if (foodName == "Water")
//            {
//
//            }
//        }
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
