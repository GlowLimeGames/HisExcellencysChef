/*
 * Author(s): Isaiah Mann
 * Description: Controls the cooking in the game
 */

using UnityEngine;

public class CookingController : SingletonController<CookingController> {
	[SerializeField]
	TextAsset stationsJSON;
	[SerializeField]
	TextAsset processesJSON;

	public StationDescriptorList Stations;
	public ProcessDescriptorList Processes;

	protected override void SetReferences () {
		base.SetReferences ();
		Stations = JsonUtility.FromJson<StationDescriptorList>(stationsJSON.text);
		Processes = JsonUtility.FromJson<ProcessDescriptorList>(processesJSON.text);
	}
}