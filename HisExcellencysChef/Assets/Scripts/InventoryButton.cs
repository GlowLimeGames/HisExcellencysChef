using UnityEngine;
using System.Collections;

//using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public int itemNum;
    public Inventory inv;
    public GameObject character;

    public void ButtonClick()
    {
        CharacterProperties cp = character.GetComponent<CharacterProperties>();
        cp.heldDish = inv.SwapItem(itemNum, cp.heldDish);
        transform.parent.gameObject.SetActive(false); // Disable the inventory window.
    }
}
