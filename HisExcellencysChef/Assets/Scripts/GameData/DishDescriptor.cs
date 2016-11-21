/*
 * Author(s): Isaiah Mann
 * Description: 
 */

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
public class DishDescriptorList : HECDataList<DishDescriptor> {}
