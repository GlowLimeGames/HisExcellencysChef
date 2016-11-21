/*
 * Author(s): Isaiah Mann
 * Description: 
 */

using System.Collections.Generic;

[System.Serializable]
public class StationDescriptor : HECData {
	public string Station;
	public int Slots;
	public string[] Processes;
	public bool LeaveDish;
	public bool Hot;

	public override string ToString () {
		return string.Format ("[StationDescriptor]{0}, {1}, {2}, {3}, {4}", Station, Slots, Processes, LeaveDish, Hot);
	}
}

[System.Serializable]
public class StationDescriptorList : HECDataList<StationDescriptor> {
	Dictionary<string, StationDescriptor> stationLookup;

	public void RefreshLookup () {
		stationLookup = new Dictionary<string, StationDescriptor>();
		foreach (StationDescriptor station in Elements) {
			stationLookup.Add(station.Station, station);
		}
	}

	public bool ContainsStation (string stationName) {
		return stationLookup.ContainsKey(stationName);
	}

	public string[] GetProcessesForStation (string stationName) {
		return stationLookup[stationName].Processes;
	}
		
}
