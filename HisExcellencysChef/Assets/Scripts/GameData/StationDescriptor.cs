/*
 * Author(s): Isaiah Mann
 * Description: 
 */

[System.Serializable]
public class StationDescriptor : HECData {
	public string Station;
	public int Slots;
	public string Processes;
	public bool LeaveDish;
	public bool Hot;

	public override string ToString () {
		return string.Format ("[StationDescriptor]{0}, {1}, {2}, {3}, {4}", Station, Slots, Processes, LeaveDish, Hot);
	}
}

public class StationDescriptorList : HECDataList<StationDescriptor> {}
