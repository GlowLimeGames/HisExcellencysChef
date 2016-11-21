/*
 * Author(s): Isaiah Mann
 * Description: 
 */

[System.Serializable]
public class RecipeDescriptor : HECData {
	public string DishName;
	public string[] Steps;
}

[System.Serializable]
public class RecipeDescriptorList : HECDataList<RecipeDescriptor> {}