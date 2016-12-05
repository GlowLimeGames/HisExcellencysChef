/*
 * Author(s): Paul Calande
 * Description: Inventory class for the pantry.
 */

using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    // Serializing the inventory node allows us to access it in the Inspector!
    [System.Serializable]
    public struct InventoryNode
    {
        public GameObject obj;
        public int quantity;

        public InventoryNode(GameObject o, int q)
        {
            //Debug.Log("Inventory.cs: InventoryNode 2-argument constructor");
            obj = o;
            quantity = q;
        }
    }
    // The inventory uses a dictionary with string keys, each of which
    // maps onto an inventory node with a GameObject and a quantity.
    public Dictionary<string, InventoryNode> dict = new Dictionary<string, InventoryNode>();

    // Adds a number of ingredients to the inventory.
    // Use -2 for an infinite quantity of ingredients.
    public void Add(GameObject obj, int quantity)
    {
        // Try to retrieve the Ingredients component from the GameObject.
        Ingredients ingred = obj.GetComponent<Ingredients>();
        // If the GameObject does not have an Ingredients component...
        if (ingred == null)
        {
            // We are so screwed.
            Debug.LogError("Inventory.cs: Tried to add a GameObject that wasn't an ingredient to dictionary!");
            return;
        }
        // Get the ingredient name.
        string name = GetIngredientName(ingred);
        //Debug.Log("Inventory.cs: component is: " + ingred + ", and ingredient name is: " + name);
        InventoryNode val;
        // If the dictionary already contains this type of ingredient...
        if (dict.TryGetValue(name, out val))
        {
            // Add to the quantity.
            val.quantity += quantity;
        }
        // Otherwise, if this ingredient isn't in the dictionary...
        else
        {
            // Add an new entry to the dictionary.
            dict.Add(name, new InventoryNode(obj, quantity));
        }
    }

    // Gets the quantity of an ingredient. Returns -1 if nothing was found.
    public int GetQuantity(GameObject obj)
    {
        Ingredients ingred = obj.GetComponent<Ingredients>();
        if (ingred == null)
        {
            Debug.LogError("Inventory.cs: Tried to access dictionary with non-ingredient GameObject!");
            return -1;
        }
        string name = GetIngredientName(ingred);
        InventoryNode val;
        // If the ingredient is in the dictionary...
        if (dict.TryGetValue(name, out val))
        {
            // Return the quantity of that ingredient.
            return val.quantity;
        }
        // If the ingredient isn't there...
        else
        {
            return -1;
        }
    }

    // Removes one ingredient from the inventory.
    public void RemoveOne(GameObject obj)
    {
        Ingredients ingred = obj.GetComponent<Ingredients>();
        if (ingred == null)
        {
            Debug.LogError("Inventory.cs: Tried to access dictionary with non-ingredient GameObject!");
            return;
        }
        string name = GetIngredientName(ingred);
        InventoryNode val;
        if (dict.TryGetValue(name, out val))
        {
            if (val.quantity == -2)
            {
                // A quantity of -2 means infinite.
                // No point in continuing this function from here.
                return;
            }
            // Reduce quantity by one.
            --val.quantity;
            if (val.quantity == 0)
            {
                // We're out of this ingredient... Remove it from the dictionary.
                dict.Remove(name);
            }
        }
        else
        {
            Debug.LogError("Inventory.cs: Tried to remove an ingredient that wasn't in the inventory: " + name);
        }
    }

    // Returns the length of the inventory/dictionary.
    public int Length()
    {
        return dict.Count;
    }

    private string GetIngredientName(Ingredients ingred)
    {
        return ingred.primaryIngredientName;
    }
}