/*
 * Author(s): Isaiah Mann
 * Description: Controls the cooking in the game
 */

using UnityEngine;
using System.Collections.Generic;

public class CookingController : SingletonController<CookingController> {
	[SerializeField]
	TextAsset stationsJSON;
	[SerializeField]
	TextAsset processesJSON;
	[SerializeField]
	TextAsset ingredientsJSON;
	[SerializeField]
	TextAsset dishesJSON;

	public StationDescriptorList Stations;
	public ProcessDescriptorList Processes;
	public IngredientDescriptorList Ingredients;
	public DishDescriptorList Dishes;

	protected override void SetReferences () {
		base.SetReferences ();
		parseJSON();
	}

	void parseJSON () {
		Stations = JsonUtility.FromJson<StationDescriptorList>(stationsJSON.text);
		Processes = JsonUtility.FromJson<ProcessDescriptorList>(processesJSON.text);
		Ingredients = JsonUtility.FromJson<IngredientDescriptorList>(ingredientsJSON.text);
		Dishes = JsonUtility.FromJson<DishDescriptorList>(dishesJSON.text);
	}
}