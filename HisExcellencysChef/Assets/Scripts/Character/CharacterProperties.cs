using UnityEngine;
using System.Collections;

public class CharacterProperties : MonoBehaviour {
    
    public bool isSelected = false;
    public bool isCook = false;
    public bool atStation = false;
    public GameObject heldDish = null; // Use null for no dish.

    private Outline ol; // A variable for the Outline script component.

    void Start()
    {
        ol = GetComponentInChildren<Outline>(); // Get the Outline script component.
        ol.enabled = false; // Turn the outline off.
    }

    public void Select()
    {
        isSelected = true;
        ol.enabled = true; // Enable outline.
    }

    public void Deselect()
    {
        isSelected = false;
        ol.enabled = false; // Disable outline.
    }
}
