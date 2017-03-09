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

	public IngredientDescriptor Copy(){
		IngredientDescriptor ingredient = new IngredientDescriptor ();
		ingredient.Ingredient = this.Ingredient;
		ingredient.PantryType = this.PantryType;
		ingredient.TasteStrength = this.TasteStrength;
		ingredient.TasteHeat = this.TasteHeat;
		ingredient.TasteMoisture = this.TasteMoisture;
		ingredient.IncomeQuantity = this.IncomeQuantity;
		ingredient.Spoilage = this.Spoilage;
		ingredient.Processes = this.Processes;
		ingredient.ResultsInDish = this.ResultsInDish;
		ingredient.PositiveExecutionModifiers = this.PositiveExecutionModifiers;
		ingredient.NegativeExecutionModifiers = this.NegativeExecutionModifiers;

		return ingredient;
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
