/*
 * Author(s): Isaiah Mann
 * Description: 
 */

[System.Serializable]
public class UnderlingDescriptor : HECData {
	public string Name;
	public string Gender;
	public string[] Traits;
}

[System.Serializable]
public class UnderlingDescriptorList : HECDataList<UnderlingDescriptor> {}
