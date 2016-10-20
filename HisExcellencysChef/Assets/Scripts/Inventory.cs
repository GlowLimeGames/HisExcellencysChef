using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public int inventorySize = 10; // Number of elements in the inventory.

    public GameObject[] inventory; // The inventory is an array of GameObjects.

	void Start () {
        inventory = new GameObject[inventorySize]; // Initialize the inventory.
        for (int i=0; i<inventorySize; ++i)
        {
            inventory[i] = null; // Set every element in the inventory to null. Use null for empty slots.
        }
	}

    public int GetFirstEmpty() // Returns the index of the first empty element in the inventory. Returns -1 if the inventory is full.
    {
        for (int i=0; i<inventorySize; ++i)
        {
            if (inventory[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public bool IsFull() // Returns whether the inventory is full.
    {
        return GetFirstEmpty() == -1;
    }

    public bool IsElementEmpty(int index) // Returns whether an element in the inventory is empty (i.e. whether the element doesn't have an item).
    {
        return inventory[index] == null;
    }

    public GameObject TakeItem(int index) // Returns an item from a certain index and removes that item from the inventory.
    {
        if (index >= inventorySize || index < 0)
        {
            Debug.Log("<color=red>"+name+" INVENTORY WARNING: Tried to take item from invalid index " + index + "</color>");
            return null;
        }
        if (inventory[index] == null)
        {
            return null;
        }
        else
        {
            GameObject item = inventory[index];
            inventory[index] = null;
            return item;
        }
    }

    public bool PutItem(int index, GameObject item) // Puts an item into the inventory if the index is empty. Returns true if successful.
    {
        if (index >= inventorySize || index < 0)
        {
            Debug.Log("<color=red>" + name + " INVENTORY WARNING: Tried to put item into invalid index " + index + "</color>");
            return false;
        }
        if (IsElementEmpty(index))
        {
            inventory[index] = item;
            return true;
        }
        return false;
    }

    public bool PutItem(GameObject item) // Dumps an item into the inventory at the first available index.
    {
        int firstEmpty = GetFirstEmpty(); // Get the first empty element.
        if (firstEmpty == -1) // If the inventory is full...
        {
            return false; // You fail.
        }
        else // Otherwise...
        {
            inventory[firstEmpty] = item; // Put the item into the inventory.
            return true; // You're a winner at life, among other things.
        }
    }

    public bool SetItem(int index, GameObject item) // Changes an item in the inventory into a different item. Returns true if successful.
    {
        if (index >= inventorySize || index < 0)
        {
            Debug.Log("<color=red>" + name + " INVENTORY WARNING: Tried to set item in invalid index " + index + "</color>");
            return false;
        }
        inventory[index] = item;
        return true;
    }

    public GameObject SwapItem(int index, GameObject item) // Replaces an item in the inventory and returns the item that was there previously.
    {
        if (index >= inventorySize || index < 0)
        {
            Debug.Log("<color=red>" + name + " INVENTORY WARNING: Tried to swap item at invalid index " + index + "</color>");
            return null;
        }
        GameObject oldItem = inventory[index];
        inventory[index] = item;
        return oldItem;
    }
}
