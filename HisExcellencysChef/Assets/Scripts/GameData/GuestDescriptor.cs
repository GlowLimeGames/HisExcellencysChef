/*
 * Author(s): Isaiah Mann
 * Description: 
 */
using System.Collections.Generic;
using System.Reflection;

[System.Serializable]
public class GuestDescriptor : HECData {
	public string name;
	public string ladyDescription;
	public string description;
	public int chance;
	public int priority;
	public string pTrigger1;
	public string pFeedback1;
	public string pTrigger2;
	public string pFeedback2;
	public string pTrigger3;
	public string pFeedback3;
	public string pTrigger4;
	public string pFeedback4;
	public string pTrigger5;
	public string pFeedback5;
	public string nTrigger1;
	public string nFeedback1;
	public string nTrigger2;
	public string nFeedback2;
	public string nTrigger3;
	public string nFeedback3;
	public string nTrigger4;
	public string nFeedback4;
	public string nTrigger5;
	public string nFeedback5;
	public int fpos3;
	public int fpos4;
	public int infneg;
	public int infpoison;	

}

[System.Serializable]
public class GuestDescriptorList : HECDataList<GuestDescriptor> {
	public Dictionary<string, GuestDescriptor> GuestByName {
		get {
			Dictionary<string, GuestDescriptor> hash = new Dictionary<string, GuestDescriptor>();
			foreach (GuestDescriptor guest in Elements) {
				hash.Add(guest.name, guest);
				System.Console.WriteLine (guest);
			}
			return hash;
		}
	}
}
