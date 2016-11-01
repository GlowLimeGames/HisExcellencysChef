using UnityEngine;
using System.Collections;

public class Station : MonoBehaviour {
    public GameObject dish; //the dishes at the Station
    public string tag;
    
    public void CookRoutine (GameObject ingredient) //how the dishes are cooked
    {
        if (tag == "cut")
        {
            dish.GetComponent<Ingredients>().cookSelf(tag);
        }
    }

    public void AddTo()
    {
        if (dish != null)
        {
            //GameObject newDish = dish.isCombinable(dish);
            //if (newDish != null){ }
            //character.getcomponent<characterProperties>().remove(dish);
            dish = null;
        }
    }

    public void PickUp(GameObject character)
    {
        if (dish != null)
        {
            //character.getcomponent<characterProperties>().addDish(dish);
            dish = null;
        }
    }

    public void PutDown(GameObject newDish)
    {
        if (dish == null)
        {
            //character.getcomponent<characterProperties>().remove(dish);
            dish = newDish;
        }
    }
        
	// Use this for initialization
	void Start () {
	    
	}
}
