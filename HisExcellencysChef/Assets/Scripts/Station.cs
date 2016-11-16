using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Station : MonoBehaviour {
    public GameObject dish; //the dishes at the Station
    public string stationType;
	public GameObject dropDown;
	public GameObject activeCharacter;
    
//    public void CookRoutine (GameObject ingredient) //how the dishes are cooked
//    {
//        if (stationType == "cut")
//        {
//            dish.GetComponent<Ingredients>().cookSelf(tag);
//        }
//    }

	public void Clicked(GameObject character){
		dropDown.SetActive (true);
		activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
	}

	public void Cancel(){
		activeCharacter = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
		activeCharacter.GetComponent<CharacterMovement> ().isMoving = false;
		dropDown.SetActive (false);
		dropDown.GetComponent<Dropdown> ().value = 0;
	}

    public void AddTo()
    {

		Cancel ();
//		if (dish != null)
//        {
//            //GameObject newDish = dish.isCombinable(dish);
//            //if (newDish != null){ }
//            //character.getcomponent<characterProperties>().remove(dish);
//            dish = null;
//        } 
    }

    public void PickUp()
    {
		if (activeCharacter.GetComponent<CharacterProperties> ().heldDish == null) {
			dish.transform.parent = activeCharacter.transform;
			dish.transform.position = activeCharacter.transform.position + new Vector3(0, 1.5f, 0);
			activeCharacter.GetComponent<CharacterProperties> ().heldDish = dish;
			dish = null;
		}
		Cancel ();
//        if (dish != null)
//        {
//            //character.getcomponent<characterProperties>().addDish(dish);
//            dish = null;
//        }
    }

    public void PutDown()
    {
		if (activeCharacter.GetComponent<CharacterProperties> ().heldDish != null) {
			activeCharacter.GetComponent<CharacterProperties> ().heldDish = null;
			dish = activeCharacter.transform.GetChild (0).gameObject;
			dish.transform.parent = transform;
			dish.transform.position = transform.position;

			dish.GetComponent<Ingredients> ().cookSelf ("", activeCharacter);
		}
		Cancel ();
		//      if (range1 is full go to range 2)  
//        if (dish == null)
//        {
//            //character.getcomponent<characterProperties>().remove(dish);
//            dish = newDish;
//        }
    }
        
}
