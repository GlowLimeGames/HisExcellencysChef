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
    public int quantity;

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
			GameObject food = (GameObject)Instantiate (item, cp.transform.position + new Vector3 (0, 0, 3f), Quaternion.identity);
			food.transform.rotation = item.transform.rotation;
			food.GetComponent<Ingredients>().primaryIngredientName = food.name.Replace ("(Clone)", "").Trim();
			food.name = food.name.Replace ("(Clone)", "").Trim();
			if (GameController.Instance.tutorial1Part4) {
				if (food.name != "Rice") {
					return;
				}
			}

			if (food.GetComponent<Ingredients>().primaryIngredient.ResultsInDish) {
				GameController.Instance.MakeUIDish (food);
			}
			if (GameController.Instance.tutorial0Part2) {
				string nextProcess = food.GetComponent<Ingredients> ().primaryIngredient.Processes [0];
				string foodName = food.GetComponent<Ingredients> ().primaryIngredientName;
				string station;
				if (nextProcess == "Fry" || nextProcess == "Seethe") {
					station = "Range";
				} else if (nextProcess == "Mince" || nextProcess == "Sliver") {
					station = "Table";
				} else if (nextProcess == "Roast" || nextProcess == "Bake") {
					station = "Oven";
					if (nextProcess == "Bake") {
						nextProcess = "Bak";
					}
				} else if (nextProcess == "Boil") {
					station = "Cauldron";
				} else if (nextProcess == "Pound") {
					station = "Mortar";
				} else if (nextProcess == "Chop") {
					station = "Table";
					nextProcess = "Chopp";
				}else {
					station = "Table";
				}
				GameController.Instance.MakeTutorialeBox("You can either add that directly to the chicken or process it first. " +
					"Try " + nextProcess + "ing your " + foodName + " at the " + station + ", if you like.");
				GameController.Instance.tutorial0Part22 = true;
				GameController.Instance.tutorial0Part2 = false;
			}
            // Make the player have this food as the held dish.
			cp.heldDish = food;
            // Set the player as the food's parent.
			food.transform.parent = cp.transform;
            // Disable the inventory window.
            invui.SetEnabled (false);
            // If the ingredient quantity is not infinite...
            if (quantity != -2)
            {
                // Remove the item from the inventory.
                invui.inv.RemoveOne(food);
                --quantity;
                // If there are no items of this type left, destroy this button.
                if (quantity == 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    UpdateText();
                }
            }
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

    public void UpdateText()
    {
        Text txt = GetComponentInChildren<Text>();
        // If the number of an ingredient is -2, the supply of that ingredient is infinite.
        if (quantity == -2)
        {
            txt.text = "Inf";
        }
        else
        {
            txt.text = quantity.ToString();
        }
    }
}
