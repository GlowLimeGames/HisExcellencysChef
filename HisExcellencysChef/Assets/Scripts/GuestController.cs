using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestController : MonoBehaviour {

	Dictionary<string, GuestDescriptor> guestLookup;
	public List<GuestDescriptor> guestList = new List<GuestDescriptor> ();


	void Start(){
		guestLookup = CookingController.Instance.Guests;
	}

	public void GetGuest(string guestName){
		GuestDescriptor guest;
		guestLookup.TryGetValue (guestName, out guest);

		guestList.Add (guest);
	}

	bool pTrigger1;
	bool pTrigger2;
	bool pTrigger3; 
	bool pTrigger4;
	bool pTrigger5;
	bool nTrigger1;
	bool nTrigger2;
	bool nTrigger3; 
	bool nTrigger4;
	bool nTrigger5;

	string ReplacePhrase(string phrase){
		if (phrase == "None" || phrase == "") {
			return "";
		}

		if (phrase.Contains ("greater than")) {
			phrase = phrase.Replace ("greater than", ">");
		}
		if (phrase.Contains ("or more")) {
			phrase = phrase.Replace ("or more", ">=");
		}
		if (phrase.Contains ("less than")) {
			phrase = phrase.Replace ("less than", "<");
		}
		if (phrase.Contains ("or less")) {
			phrase = phrase.Replace ("or less", "<=");
		}

		if (phrase.Contains ("Below Green in Yellow")) {
			phrase = phrase.Replace ("Below Green in Yellow", "UYellow");
		}
		if (phrase.Contains ("Above Green in Yellow")) {
			phrase = phrase.Replace ("Above Green in Yellow", "OYellow");
		}
		if (phrase.Contains ("Below Green in Red")) {
			phrase = phrase.Replace ("Below Green in Red", "URed");
		}
		if (phrase.Contains ("Above Green in Red")) {
			phrase = phrase.Replace ("Above Green in Red", "ORed");
		}
		if (phrase.Contains ("Process in Green")) {
			phrase = phrase.Replace ("Process in Green", "Process Green");
		}
		if (phrase.Contains ("Process in DarkGreen")) {
			phrase = phrase.Replace ("Process in DarkGreen", "Process DarkGreen");
		}

		return phrase;
	}

	bool OperatePhrase(string phrase, Ingredients dish){
		string[] processes = new string[0];
		string aProcess = "";
		if (phrase.Contains("Heat Process")){
			processes = new string[3];
			processes[0] = "Roast";
			processes[1] = "Bake";
			processes[2] = "Fry";
			aProcess = "Heat Process ";
		}
		if (phrase.Contains("Grinding Process")){
			processes = new string[1];
			processes[0] = "Pound";
			aProcess = "Grinding Process ";
		}
		if (phrase.Contains("Cut Process") || phrase.Contains("Cutting Process")){
			processes = new string[3];
			processes[0] = "Chop";
			processes[1] = "Sliver";
			processes[2] = "Mince";
			if (phrase.Contains("Cut Process")){
				aProcess = "Grinding Process ";
			} else if (phrase.Contains("Cutting Process")){
				aProcess = "Cutting Process ";
			}
		}
		if (phrase.Contains("Seething") || phrase.Contains("Seethed")){
			processes = new string[1];
			processes[0] = "Seethe ";
			if (phrase.Contains ("Seething")){
				aProcess = "Seething ";
			} else if (phrase.Contains ("Seethed")){
				aProcess = "Seethed "; 
			}
		}
		if (phrase.Contains("Range Process")){
			processes = new string[2];
			processes[0] = "Fry";
			processes [1] = "Seethe";
			aProcess = "Range Process ";
		}

		if (aProcess != "") {
			phrase = phrase.Replace (aProcess, "");
		}

		for (int i = 0; i < dish.actionsDone.Count; i++) {
			foreach (string process in processes) {
				if (process == dish.actionsDone [i]) {
					if (dish.howCooked [i] == phrase) {
						return true;
					}
				}
			}
		}

		int xBound = 0;
		int yBound = 0;
		if (phrase.Contains ("AND")){
			if (phrase.Contains ("between") || phrase.Contains("Between")) {
				int.TryParse (phrase.Substring (29, 2), out xBound);
				int.TryParse (phrase.Substring (35, 2), out yBound);
				if ((dish.flavor.x >= xBound && dish.flavor.x <= yBound) &&
				   ((dish.flavor.y >= xBound && dish.flavor.y <= yBound))) {
					return true;
				}
			}
		} else if (phrase.Contains ("Heat Score")){
			if (phrase.Contains (">")) {
				int.TryParse(phrase.Substring(12, 2), out xBound);
				if (dish.flavor.x > xBound){
					return true;
				}
			}
			if (phrase.Contains (">=")) {
				int.TryParse(phrase.Substring(15, 2), out xBound);
				if (dish.flavor.x >= xBound){
					return true;
				}
			}
			if (phrase.Contains ("<")) {
				int.TryParse(phrase.Substring(12, 2), out xBound);
				if (dish.flavor.x < xBound){
					return true;
				}
			}
			if (phrase.Contains ("<=")) {
				int.TryParse(phrase.Substring(15, 2), out xBound);
				if (dish.flavor.x <= xBound){
					return true;
				}
			}
			if (phrase.Contains ("between") || phrase.Contains("Between")) {
				int.TryParse (phrase.Substring (19, 2), out xBound);
				int.TryParse (phrase.Substring (20, 2), out yBound);

				if (dish.flavor.x >= xBound && dish.flavor.x <= yBound) {
					return true;
				}
			}
		} else if (phrase.Contains ("Moisture Score")){
			if (phrase.Contains (">")) {
				int.TryParse(phrase.Substring(16, 2), out xBound);
				if (dish.flavor.y > xBound){
					return true;
				}
			}
			if (phrase.Contains (">=")) {
				int.TryParse(phrase.Substring(19, 2), out xBound);
				if (dish.flavor.y >= xBound){
					return true;
				}
			}
			if (phrase.Contains ("<")) {
				int.TryParse(phrase.Substring(16, 2), out xBound);
				if (dish.flavor.y > xBound){
					return true;
				}
			}
			if (phrase.Contains ("<=")) {
				int.TryParse(phrase.Substring(19, 2), out xBound);
				if (dish.flavor.y > xBound){
					return true;
				}
			}
			if (phrase.Contains ("between") || phrase.Contains("Between")) {
				int.TryParse (phrase.Substring (17, 2), out xBound);
				int.TryParse (phrase.Substring (19, 2), out yBound);
				if (dish.flavor.y >= xBound && dish.flavor.y <= yBound) {
					return true;
				}
			}
		}

		return false;


	}


	public string ParseTriggers(GuestDescriptor guest,Ingredients dish){
		guest.pTrigger1 = ReplacePhrase (guest.pTrigger1);
		guest.pTrigger2 = ReplacePhrase (guest.pTrigger2);
		guest.pTrigger3 = ReplacePhrase (guest.pTrigger3);
		guest.pTrigger4 = ReplacePhrase (guest.pTrigger4);
		guest.pTrigger5 = ReplacePhrase (guest.pTrigger5);

		guest.nTrigger1 = ReplacePhrase (guest.nTrigger1);
		guest.nTrigger2 = ReplacePhrase (guest.nTrigger2);
		guest.nTrigger3 = ReplacePhrase (guest.nTrigger3);
		guest.nTrigger4 = ReplacePhrase (guest.nTrigger4);
		guest.nTrigger5 = ReplacePhrase (guest.nTrigger5);

		pTrigger1 = OperatePhrase (guest.pTrigger1, dish);
		pTrigger2 = OperatePhrase (guest.pTrigger2, dish);
		pTrigger3 = OperatePhrase (guest.pTrigger3, dish);
		pTrigger4 = OperatePhrase (guest.pTrigger4, dish);
		pTrigger5 = OperatePhrase (guest.pTrigger5, dish);

		nTrigger1 = OperatePhrase (guest.nTrigger1, dish);
		nTrigger2 = OperatePhrase (guest.nTrigger2, dish);
		nTrigger3 = OperatePhrase (guest.nTrigger3, dish);
		nTrigger4 = OperatePhrase (guest.nTrigger4, dish);
		nTrigger5 = OperatePhrase (guest.nTrigger5, dish);


		string responseP = "";
		responseP = CheckPositiveTriggers (guest, dish);

		string responseN = "";
		responseN = CheckNegativeTriggers (guest, dish);

		if (responseN == "" && responseP != "") {
			return responseP;
		} else if (responseP == "" && responseN != "") {
			return responseN;
		}

		return "I have nothing to say to you.";
	}

	public string CheckPositiveTriggers(GuestDescriptor guest, Ingredients dish){
		string response = "";
		if (pTrigger5) {
			response = guest.pFeedback5;
		} else if (pTrigger4) {
			response = guest.pFeedback4;
		} else if (pTrigger3) {
			response = guest.pFeedback3;
		} else if (pTrigger2) {
			response = guest.pFeedback2;
		} else if (pTrigger1) {
			response = guest.pFeedback1;
		} 

		if (response.Contains ("<ingredient>")) {
			response = response.Replace ("<ingredient>", dish.primaryIngredientName);
		}
		return response;
	}

	public string CheckNegativeTriggers(GuestDescriptor guest, Ingredients dish){
		string response = "";
		if (nTrigger5) {
			response = guest.nFeedback5;
		} else if (nTrigger4) {
			response = guest.nFeedback4;
		} else if (nTrigger3) {
			response = guest.nFeedback3;
		} else if (nTrigger2) {
			response = guest.nFeedback2;
		} else if (nTrigger1) {
			response = guest.nFeedback1;
		} 

		if (response.Contains ("<ingredient>")) {
			response = response.Replace ("<ingredient>", dish.primaryIngredientName);
		}

		return response;
	}


}
