/*
 * Author(s): Isaiah Mann
 * Description: 
 */

using System.Collections.Generic;

[System.Serializable]
public class DishDescriptor : HECData {
	public string Ingredient;
	public string Chop;
	public string Sliver;
	public string Mince;
	public string Pound;
	public string Fry;
	public string Seethe;
	public string Bake;
	public string Roast;
	public string Boil;
}

[System.Serializable]
public class DishDescriptorList : HECDataList<DishDescriptor> {
	Dictionary<string, Dictionary<string, string>> results;

	public void RefreshResultLookup () {
		results = new Dictionary<string, Dictionary<string, string>>();
		foreach (DishDescriptor dish in Elements) {
			results.Add(dish.Ingredient, new Dictionary<string, string> {
				{Event.CHOP, dish.Chop},
				{Event.SLIVER, dish.Sliver},
				{Event.MINCE, dish.Mince},
				{Event.POUND, dish.Pound},
				{Event.FRY, dish.Fry},
				{Event.SEETHE, dish.Seethe},
				{Event.BAKE, dish.Bake},
				{Event.ROAST, dish.Roast},
				{Event.BOIL, dish.Boil}
			});
		}
	}

	public bool SupportsAction (string ingredient, string actionName) {
		return results.ContainsKey(actionName) && results[actionName] != null;
	}

	public string Result (string ingredient, string actionName) {
		return results[ingredient][actionName];
	}
}
