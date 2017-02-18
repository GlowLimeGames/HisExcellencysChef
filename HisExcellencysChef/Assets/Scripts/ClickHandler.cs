using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {

    public GameObject currentCharacter; // The currently selected character. Use null for no selected character.
    public GameObject pantryInventoryUI; // Pantry inventory object.
    public GameObject dialogueWindow; // Dialogue window object.
	public GameObject activeDropdown;

    private DialogueController dc; // Dialogue controller component.

    void Awake()
    {
        dc = dialogueWindow.GetComponent<DialogueController>();
    }

	void Start ()
    {
        SelectCook(); // Start by selecting the Cook automatically.
    }
	
	void Update ()
	{	
		if (Input.GetMouseButtonDown (1)) { // If we right click.
			if (CanSelectCharacter ()) { // If we can select a character...
				Vector3 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (worldPoint, Vector3.down, out hit)) { // If we clicked an object...
					GameObject clickedObject = hit.collider.gameObject; // Store the object that was just clicked inside clickedObject.
					Debug.Log (clickedObject.name + " was clicked."); // Print the object name to the console.
					if (clickedObject.tag.Equals ("PlayerChar")) { // If a playable character is clicked...
						//Do Something
					}

					if (activeDropdown != null) {
						if (clickedObject != activeDropdown && clickedObject != activeDropdown.GetComponent<Station> ().dropDown) {
							activeDropdown.GetComponent<Station> ().Cancel ();
							activeDropdown.GetComponent<Station> ().dropDown.SetActive (false);
							currentCharacter.GetComponent<CharacterMovement> ().Cancel();
							activeDropdown = null;
						}
					}
				}
			}
		}
        if (Input.GetMouseButtonDown(0)) // If we left click...
        {
            if (CanSelectCharacter()) // If we can select a character...
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(worldPoint, Vector3.down, out hit)) // If we clicked an object...
                {
                    GameObject clickedObject = hit.collider.gameObject; // Store the object that was just clicked inside clickedObject.
                    Debug.Log(clickedObject.name + " was clicked."); // Print the object name to the console.
                    if (clickedObject.tag.Equals("PlayerChar")) // If a playable character is clicked...
                    {
                        SelectCharacter(clickedObject); // Select the character.
                    }


                    if ((currentCharacter != null)) // If we have a playable character and click on a station...
                    {
                        CharacterMovement charMover = currentCharacter.GetComponent<CharacterMovement>();
						CharacterProperties cp = currentCharacter.GetComponent<CharacterProperties> ();
						if (!cp.isCook) {
							if (charMover.isCooking) {
								return;
							}
						}
                        if (clickedObject.name == "Pantry")
                        {
							if (activeDropdown != null) {
								if (clickedObject != activeDropdown && clickedObject != activeDropdown.GetComponent<Station> ().dropDown) {
									activeDropdown.GetComponent<Station> ().Cancel ();
									activeDropdown.GetComponent<Station> ().dropDown.SetActive (false);
									activeDropdown = null;
								}
							}
                            InventoryUI piui = pantryInventoryUI.GetComponent<InventoryUI>();
                            piui.SetEnabled(true);
                        }
						else 
                        {
                            charMover.Move(hit.point, clickedObject); // Make the character move to that station.
                            //SelectCook(); // Select the Cook, which in turn will deselect any other character.
                        }
                    }
                }
            }
        }
    }

    public bool CanSelectCharacter()
    {
        // Only return true when the pantry, dialogue window, etc isn't open.
        return !pantryInventoryUI.activeSelf && !dc.active;
    }

    public void SelectCharacter(GameObject obj)
    {
        //Debug.Log("Selected "+obj.name);
        if (currentCharacter != null) // If a character is already selected...
        {
            currentCharacter.GetComponent<CharacterProperties>().Deselect(); // Tell the old character that it has been deselected.
        }
        currentCharacter = obj; // Set the current character to the new character.
        currentCharacter.GetComponent<CharacterProperties>().Select(); // Tell the new character that it has been selected.
    }

    public void DeselectCharacter()
    {
        currentCharacter.GetComponent<CharacterProperties>().Deselect(); // Tell the character that it has been deselected.
        currentCharacter = null; // Change the selection to no character.
    }

    public void SelectCook()
    {
        SelectCharacter(GameObject.Find("Cook")); // Select the cook.
    }
}
