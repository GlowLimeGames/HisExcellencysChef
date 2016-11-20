/*
 * Author(s): Isaiah Mann
 * Description: Controls the cooking in the game
 */

using UnityEngine;

public class CookingController : SingletonController<CookingController> {
	[SerializeField]
	TextAsset stationsJSON;
	public StationDescriptorList Stations;

	protected override void SetReferences () {
		base.SetReferences ();
		Stations = JsonUtility.FromJson<StationDescriptorList>(stationsJSON.text);
	}
}