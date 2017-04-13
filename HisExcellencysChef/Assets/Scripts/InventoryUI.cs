/*
 * Author(s): Paul Calande
 * Description: Handles the UI functionality of the pantry.
 */

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // For referencing the inventory button prefab.
    public GameObject inventoryButton;
    // Reference to the inventory data object below the inventory.
    public GameObject inventoryData;

    // A class instance that handles the actual data structure storing the ingredients.
    [System.NonSerialized]
    public Inventory inv;

    // For use in the Inspector.
    // It's an array of elements, where each element contains a GameObject and an integer quantity.
    [SerializeField]
    public Inventory.InventoryNode[] foods;

    private void Awake()
    {
        SetEnabled(false);
    }

    private void Start()
    {
        inv = GetComponent<Inventory>();
        // Populate the inventory with the foods defined in the Inspector.
        for (int i = 0; i < foods.Length; ++i)
        {
            foods[i].obj.GetComponent<Ingredients>().NonInstanceDoReferences();
            //Debug.Log("InventoryUI.cs: "+foods[i].obj.transform.name);
            inv.Add(foods[i].obj, foods[i].quantity);
        }
        // For every inventory slot...
        //int ii = 0;
        foreach (KeyValuePair<string, Inventory.InventoryNode> entry in inv.dict)
        {
            // Instantiate an inventory item button.
            GameObject btn = (GameObject)Instantiate(inventoryButton, transform.position, transform.rotation);
            // Set the new button's parent to the current object.
            btn.transform.SetParent(transform, false);
            InventoryButton btninv = btn.GetComponent<InventoryButton>();
            btninv.SetGameObject(entry.Value.obj);
            btninv.quantity = entry.Value.quantity;
            // Set the button image to the sprite image.
            btninv.GetComponent<Image>().sprite = entry.Value.obj.GetComponent<SpriteRenderer>().sprite;
            // Set the text child to display the number of ingredients.
            btninv.UpdateText();
            //Debug.Log("Key/Value Loop: "+ii++);
        }
    }

    // Use this method to enable/disable the inventory and its associated components.
    public void SetEnabled(bool enable)
    {
        gameObject.SetActive(enable);
        inventoryData.SetActive(enable);
    }
}