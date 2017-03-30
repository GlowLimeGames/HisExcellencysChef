// Script by Paul Calande, 11/29/16
// Handles dialogue.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    // Reference to the dialogue text UI object.
    [SerializeField]
    Text textObject;
    // Reference to the portrait object.
    [SerializeField]
    Image portrait;
	public Button button;
    // Reference to CanvasGroup component.
    CanvasGroup cg;
    // Whether the dialogue window is active.
    public bool active = true;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        // If the dialogue box isn't set as active in the scene editor, hide it.
        SetActive(active);
        // First message!
		if (active) {
			CharacterSpeak (@"Hello, young cook!

To select a primary ingredient, click on a character, then click on the Pantry Door.

To cook that ingredient, click a station and select ""Put Down"" in the drop down menu, then select it again and pick the cooking process you want and wait for the character to pick it up again.

When you want to finish a dish, select the character carrying it and click on the gray square below.

Ingredients are Hot or Cold and Moist or Dry. Try to balance the flavors across your dishes.");
		}
    }

    // Enable or disable the dialogue box.
    public void SetActive(bool cond)
    {
        active = cond;
        if (cond == true)
        {
            // Make the UI visible.
            cg.alpha = 1f;
            // Allow it to receive input events.
            cg.blocksRaycasts = true;
        }
        else
        {
            // Make the UI invisible.
            cg.alpha = 0f;
            // Prevent it from receiving input events.
            cg.blocksRaycasts = false;
        }
    }

    // Make a character talk using the message box.
    public void CharacterSpeak(string msg)
    {
        SetActive(true);
        StartCoroutine(DialogueTypeOut(msg));
    }

    public void PressOK()
    {
        // Close the dialogue box.
		if (GameController.Instance.course == 0) {
			GameController.Instance.timer = true;
			SetActive (false);
		} else {
			GameController.Instance.Reset ();
		}
		StopAllCoroutines ();
    }

    // This is the coroutine that actually makes the character type out their message letter-by-letter.
    // It uses rich text to hide the text. This way, the word wrap looks natural.
    IEnumerator DialogueTypeOut(string msg)
    {
        // Number of seconds the message waits between typing each letter.
        const float LETTER_WAIT_SECONDS = 0.01f;
        // This color has an alpha of 00, so it hides the text.
        const string richcolor = "<color=#00000000>";
        // Get length of string.
        int len = msg.Length + 1;
        for (int i = 0; i < len; ++i)
        {
            // Insert the rich text color code into the text.
            // This will hide all of the text after position i.
            string typedOut = msg.Insert(i, richcolor) + "</color>";
            // Update the text field with the resulting rich text.
            textObject.text = typedOut;
            // Pause after each letter.
            yield return new WaitForSeconds(LETTER_WAIT_SECONDS);
        }
    }
}
