// Script by Paul Calande, 11/29/16
// Handles dialogue.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
		StopAllCoroutines ();
        StartCoroutine(DialogueTypeOut(msg));
    }

	public List<string> messageList = new List<string> ();
	public void AddMessage(string msg){
		SetActive (true);
		StopAllCoroutines ();
		StartCoroutine (DialogueTypeOut (msg));

		messageList.Add (msg);
		msgIndex = messageList.Count - 1;
	}

	public void ContinueMsg(string msg){
		messageList.Add (msg);
	}

	public GameObject nextButton;
	public void NextMsg(){
		msgIndex += 1;
		textObject.text = messageList [msgIndex];
	}

	public GameObject previousButton;
	public void PreviousMsg(){
		msgIndex -= 1;
		textObject.text = messageList [msgIndex];
	}

	int msgIndex = 0;
	public bool dialogueBox = false;
	void Update(){
		if (dialogueBox) {
			if (msgIndex == messageList.Count - 1) {
				nextButton.SetActive (false);
			} else {
				nextButton.SetActive (true);
			}
			if (msgIndex == 0) {
				previousButton.SetActive (false);
			} else {
				previousButton.SetActive (true);
			}
				
			if (Input.GetKeyUp(KeyCode.Escape)) {
				if (cg.alpha == 1) {
					SetActive (false);
				} else {
					SetActive (true);
				}
			}
		}
	}

    public void PressOK()
    {

		if (GameController.Instance.tutorial1Part1) {
			GameController.Instance.MakeTutorialeBox("This is Aemilia. To order her to Roast the Almonds, you must first select her by clicking on her or her portrait at the bottom of the screen. Note that she will become outlined in green when selected.");
		}
		if (GameController.Instance.tutorial2Part1) {
			GameController.Instance.MakeTutorialeBox ("It is a good idea to keep a written guest list handy, so you can quickly check the preferences of our guests as you cook. For now, I’ve written you a guest list myself. Click here and open it.");
		}
		if (GameController.Instance.tutorial3Part1) {
			GameController.Instance.Invoke ("NextTutorialStep", 20f);
		}
		if (GameController.Instance.tutorial3Part3) {
			GameController.Instance.LadyFeedback();
		}

		if (GameController.Instance.kingLevel && GameController.Instance.course < 1) {
			GameController.Instance.MakeTutorialeBox ("All that I have achieved may finally bear fruit. Whatever milady wants out of the king is her business, but if I can get him to think this meal is perfect, I might just become the most powerful cook in France.");
		} else if (GameController.Instance.kingLevel && GameController.Instance.course >= 2) {
			GameController.Instance.KingFeedback ();
		}

		if (GameController.Instance.infamyLevel) {
			if (GameController.Instance.florineTriggered) {
				GameController.Instance.Reset ();
			}
		}
                                                                                                           // Close the dialogue box.
		if (GameController.Instance.course == 0) {
			GameController.Instance.timer = true;
			SetActive (false);
		} else if (GameController.Instance.course >= 2){
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
			if (Input.GetMouseButtonUp (0)) {
				typedOut = msg;
				i = msg.Length;
			}
            textObject.text = typedOut;
            // Pause after each letter.
            yield return new WaitForSeconds(LETTER_WAIT_SECONDS);
        }
    }
}
