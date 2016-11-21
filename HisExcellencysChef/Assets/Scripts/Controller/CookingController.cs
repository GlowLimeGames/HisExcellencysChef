/*
 * Author(s): Isaiah Mann
 * Description: Controls the cooking in the game
 */

using UnityEngine;
using System.Collections.Generic;

public class CookingController : SingletonController<CookingController> {

	#region JSON

	[SerializeField]
	TextAsset stationsJSON;
	[SerializeField]
	TextAsset processesJSON;
	[SerializeField]
	TextAsset ingredientsJSON;
	[SerializeField]
	TextAsset dishesJSON;
	[SerializeField]
	TextAsset recipesJSON;
	[SerializeField]
	TextAsset skillsJSON;
	[SerializeField]
	TextAsset underlingsJSON;

	#endregion

	#region Tuning Data

	[SerializeField]
	StationDescriptorList stations;
	[SerializeField]
	ProcessDescriptorList processes;
	[SerializeField]
	IngredientDescriptorList ingredients;
	[SerializeField]
	DishDescriptorList dishes;
	[SerializeField]
	RecipeDescriptorList recipes;
	[SerializeField]
	SkillDescriptorList skills;
	[SerializeField]
	UnderlingDescriptorList underlings;

	#endregion

	public string[] GetProcessesForStation (string stationName) {
		if (stations.ContainsStation(stationName)) {
			return stations.GetProcessesForStation(stationName);
		} else {
			Debug.LogErrorFormat("{0} is not a known station", stationName);
			return new string[0];
		}
	}

	public Dictionary<string, IngredientDescriptor> Ingredients {
		get {
			return ingredients.IngredientsByName;
		}
	}

	public bool SupportsAction (string ingredient, string processName) {
		return dishes.SupportsAction(ingredient, processName);
	}

	public string Result (string ingredient, string processName) {
		return dishes.Result(ingredient, processName);
	}

	public bool TryModifyWithAction (IngredientDescriptor ingredient, string processName) {
		return processes.TryModifyWithAction(ingredient, processName);	
	}

	public ProcessDescriptor GetProcess (string processName) {
		ProcessDescriptor process;
		if (processes.TryGetProcess(processName, out process)) {
			return process;
		} else {
			Debug.LogErrorFormat("Could not find process {0}. Returning null");
			return null;
		}
	}

	protected override void SetReferences () {
		base.SetReferences ();
		parseJSON();
		processData();
	}

	void parseJSON () {
		stations = JsonUtility.FromJson<StationDescriptorList>(stationsJSON.text);
		processes = JsonUtility.FromJson<ProcessDescriptorList>(processesJSON.text);
		ingredients = JsonUtility.FromJson<IngredientDescriptorList>(ingredientsJSON.text);
		dishes = JsonUtility.FromJson<DishDescriptorList>(dishesJSON.text);
		recipes = JsonUtility.FromJson<RecipeDescriptorList>(recipesJSON.text);
		skills = JsonUtility.FromJson<SkillDescriptorList>(skillsJSON.text);
		underlings = JsonUtility.FromJson<UnderlingDescriptorList>(underlingsJSON.text);
	}

	void processData () {
		dishes.RefreshResultLookup();
		processes.RefreshLookup();
		stations.RefreshLookup();
	}
}