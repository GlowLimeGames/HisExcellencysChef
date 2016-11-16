using UnityEngine;
using System.Collections;

//using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public int itemNum;
    public Inventory inv;
    public GameObject character;
	public GameObject[] foods = new GameObject[4];

    public void ButtonClick()
    {
		character = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
        CharacterProperties cp = character.GetComponent<CharacterProperties>();
		if (cp.heldDish == null) {
			GameObject food = (GameObject)Instantiate (foods [itemNum], cp.transform.position + new Vector3 (0, 1.5f, 0), Quaternion.identity);
			cp.heldDish = food;
			food.transform.parent = cp.transform;
			transform.parent.gameObject.SetActive (false); // Disable the inventory window.
		} else {
			transform.parent.gameObject.SetActive (false);
		}
    }
}
