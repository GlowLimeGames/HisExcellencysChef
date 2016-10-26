using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject owner; // The cooking station that's connected to the inventory UI.
    public GameObject inventoryButton; // For referencing the inventory button prefab.
    public GameObject character; // The character that opened the inventory menu.

    void Start()
    {
        gameObject.SetActive(false);
        owner = GameObject.Find("Pantry");
        character = GameObject.Find("Cook"); // By default.
        Inventory inv = owner.GetComponent<Inventory>();
        for (int i=0; i<inv.inventorySize; ++i) // For every inventory slot...
        {
            GameObject btn = (GameObject) Instantiate(inventoryButton, transform.position, transform.rotation); // Instantiate an inventory item button.
            btn.transform.SetParent(transform, false); // Set the new button's parent to the current object.
            InventoryButton btninv = btn.GetComponent<InventoryButton>();
            btninv.itemNum = i;
            btninv.inv = inv;
            btninv.character = character;
            btninv.GetComponentInChildren<Text>().text = "memes";
        }
    }

    public void Open(GameObject opener)
    {
        character = opener;
        gameObject.SetActive(true);
    }
}