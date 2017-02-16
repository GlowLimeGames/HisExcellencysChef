/*
 * Author(s): Isaiah Mann
 * Description: 
 */
using System.Collections.Generic;
using System.Reflection;

[System.Serializable]
public class UnderlingDescriptor : HECData {
	public string Name;
	public string Gender;
	public string[] Traits;
	public float AvgPound;
	public float DistPound;
	public float AvgChop;
	public float DistChop;
	public float AvgSliver;
	public float DistSliver;
	public float AvgMince;
	public float DistMince;
	public float AvgFry;
	public float DistFry;
	public float AvgRoast;
	public float DistRoast;

	public float GetProcessAvg (string action) {
		action = "Avg" + action;
		return (float)this.GetType ().GetField (action).GetValue (this);
	}

	public float GetProcessDist (string action) {
		string Dist = "Dist" + action;
		return (float)this.GetType ().GetField (Dist).GetValue (this);
	}
}

[System.Serializable]
public class UnderlingDescriptorList : HECDataList<UnderlingDescriptor> {
	public Dictionary<string, UnderlingDescriptor> UnderlingByName {
		get {
			Dictionary<string, UnderlingDescriptor> hash = new Dictionary<string, UnderlingDescriptor>();
			foreach (UnderlingDescriptor underling in Elements) {
				hash.Add(underling.Name, underling);
			}
			return hash;
		}
	}
}
