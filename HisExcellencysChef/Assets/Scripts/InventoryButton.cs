/*
 * Author(s): Paul Calande, Joel Esquilin
 * Description: Inventory button class.
 */

using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    private GameObject item;
    private InventoryUI invui;

    private void Start()
    {
        //Debug.Log("InventoryButton.cs: I'm real.");
        invui = transform.parent.gameObject.GetComponent<InventoryUI>();
    }

    public void ButtonClick()
    {
        // Get the currently-selected character.
		GameObject character = GameObject.Find ("ClickHandler").GetComponent<ClickHandler> ().currentCharacter;
        // Then, get their CharacterProperties component.
        CharacterProperties cp = character.GetComponent<CharacterProperties>();
        // If the character isn't holding anything...
		if (cp.heldDish == null)
        {
            // Instantiate the food above the player's head.
			GameObject food = (GameObject)Instantiate (item, cp.transform.position + new Vector3 (0, 1.5f, 0), Quaternion.identity);
			food.GetComponent<Ingredients>().primaryIngredientName = food.name.Replace ("(Clone)", "").Trim();
            // Make the player have this food as the held dish.
			cp.heldDish = food;
            // Set the player as the food's parent.
			food.transform.parent = cp.transform;
            // Disable the inventory window.
            invui.SetEnabled (false);
            // Remove the item from the inventory.
            // [later]
		}
        // If the character IS holding something...
        else
        {
			invui.SetEnabled (false);
		}
    }

    // Sets the ingredient GameObject to which the inventory button refers.
    public void SetGameObject(GameObject obj)
    {
        item = obj;
    }

    public void MouseOver()
    {
        //Debug.Log("MOUSE OVER!");
        // Get the text component of the inventory data window's child, which is a text object.
        Text txt = invui.inventoryData.transform.GetChild(0).GetComponent<Text>();
        // Get the ingredient data of the button's food.
        Ingredients ingred = item.GetComponent<Ingredients>();
        string name = item.name;
        string moistheat = ingred.HeatAndMoistureToString();
        // Change the text.
        txt.text = name + '\n' + moistheat;
    }
}
