/*
 * Author(s): Isaiah Mann
 * Description: 
 */

[System.Serializable]
public class SkillDescriptor : HECData {
	public string Name;
	public string Description;
	public StatModifier[] StatModifiers;
}

[System.Serializable]
public class SkillDescriptorList : HECDataList<SkillDescriptor>{}

[System.Serializable]
public class StatModifier : HECData {
	public string Skill;
	public string Class;
	public string Type;
	public float Mod;
}