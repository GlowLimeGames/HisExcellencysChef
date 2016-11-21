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
	[SerializeField]
	TextAsset ingredientsJSON;

	public StationDescriptorList Stations;
	public ProcessDescriptorList Processes;
	public IngredientDescriptorList Ingredients;


	protected override void SetReferences () {
		base.SetReferences ();
		parseJSON();
	}

	void parseJSON () {
		Stations = JsonUtility.FromJson<StationDescriptorList>(stationsJSON.text);
		Processes = JsonUtility.FromJson<ProcessDescriptorList>(processesJSON.text);
		Ingredients = JsonUtility.FromJson<IngredientDescriptorList>(ingredientsJSON.text);
	}
}