﻿/*
 * Author(s): Isaiah Mann
 * Description: 
 */

using System.Collections.Generic;

[System.Serializable]
public class ProcessDescriptor : HECData {
	public string Process;
	public string Type;
	public string Station;
	public float TasteHeat;
	public float TasteMoisture;
	public string [] Ingredients;
	public bool Active;
	public string [] Obsoletes;
	public string [] ExecutionBonuses;
	public string [] ExecutionPenalties;
	public int IdealTimeMin;
	public int IdealTimeMax;
	public int RedYellow;
	public int YellowGreen;
	public int GreenDarkGreen;
	public int DarkGreenGreen;
	public int GreenYellow;
	public int YellowRed;
	public string ProgressBarPNG;
	HashSet<string> supportedIngredients;

	public override string ToString () {
		return string.Format ("[ProcessDescriptor] " +
			"{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}",
			Process,
			Type,
			Station,
			TasteHeat,
			TasteMoisture,
			ArrayUtil.ToString(Ingredients),
			Active,
			ArrayUtil.ToString(Obsoletes),
			IdealTimeMin,
			IdealTimeMax,
			RedYellow,
			YellowGreen,
			GreenDarkGreen,
			DarkGreenGreen,
			GreenYellow,
			YellowRed,
			ProgressBarPNG
		);
	}

	public void RefreshLookup () {
		supportedIngredients = new HashSet<string>();
		foreach (string ingredient in Ingredients) {
			supportedIngredients.Add(ingredient);
		}
	}

	public bool SupportsIngredient (IngredientDescriptor ingredient) {
		return supportedIngredients.Contains(ingredient.Ingredient);
	}

	public void PerformOnIngredient (IngredientDescriptor ingredient, float howRaw) {
		ingredient.TasteHeat += this.TasteHeat * howRaw * .25f;
		ingredient.TasteMoisture += this.TasteMoisture * howRaw * .25f;
	}
}

[System.Serializable]
public class ProcessDescriptorList : HECDataList<ProcessDescriptor> {
	Dictionary<string, ProcessDescriptor> processLookup;

	public void RefreshLookup () {
		processLookup = new Dictionary<string, ProcessDescriptor>();
		foreach (ProcessDescriptor process in Elements) {
			processLookup.Add(process.Process, process);
			process.RefreshLookup();
		}
	}

	public bool TryModifyWithAction (IngredientDescriptor ingredient, string processName, float howRaw) {
		ProcessDescriptor process;
		if (processLookup.TryGetValue(processName, out process) && process.SupportsIngredient(ingredient)) {
			process.PerformOnIngredient(ingredient, howRaw);
			return true;
		} else {
			return false;
		}
	}

	public bool TryGetProcess (string processName, out ProcessDescriptor process) {
		return processLookup.TryGetValue(processName, out process);
	}
}
