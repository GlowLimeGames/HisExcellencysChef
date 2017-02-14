/*
 * Author(s): Isaiah Mann
 * Description: Keeps track of various ingredient properties.
 */

using System.Collections.Generic;

[System.Serializable]
public class IngredientDescriptor : HECData {
	public string Ingredient;
	public string PantryType;
	public string TasteStrength;
	public float TasteHeat;
	public float TasteMoisture;
	public string IncomeQuantity;
	public string Spoilage;
	public string[] Processes;
	public bool ResultsInDish;
	public string[] PositiveExecutionModifiers;
	public string[] NegativeExecutionModifiers;

	public void ModifyWithAddOn (IngredientDescriptor addOn) {
		this.TasteHeat += addOn.TasteHeat;
		this.TasteMoisture += addOn.TasteMoisture;
	}
}

[System.Serializable]
public class IngredientDescriptorList : HECDataList<IngredientDescriptor> {
	public Dictionary<string, IngredientDescriptor> IngredientsByName {
		get {
			Dictionary<string, IngredientDescriptor> hash = new Dictionary<string, IngredientDescriptor>();
			foreach (IngredientDescriptor ingredient in Elements) {
				hash.Add(ingredient.Ingredient, ingredient);
			}
			return hash;
		}
	}
}
