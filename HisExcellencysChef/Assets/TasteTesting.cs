using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TasteTesting
{
	private List<string> heatMoisture, timeFunctions;
	private double dishHeat, dishMoisture, idealValueMin, idealValueMax, timerValue;
	private string processColor;
	//Heat of dish, moisture of dish, color of process, ideal cooking time, current time
	public TasteTesting(double dishHeat, double dishMoisture, string processColor, double idealValueMin, double idealValueMax, double timerValue) {
		this.dishHeat = dishHeat;
		this.dishMoisture = dishMoisture;
		this.idealValueMin = idealValueMin;
		this.idealValueMax = idealValueMax;
		this.timerValue = timerValue;
		this.processColor = processColor;
		heatMoisture = new List<string>();
		timeFunctions = new List<string>();
		updateAll (this.dishHeat, this.dishMoisture, this.processColor, this.idealValueMin, this.idealValueMax, this.timerValue);
	}

	public void updateAll(double dishHeat, double dishMoisture, string processColor, double idealValueMin, double idealValueMax, double timerValue)
	{
		updateHeatAndMoisture(dishHeat, dishMoisture);
		updateProcess(processColor, idealValueMin, idealValueMax, timerValue);
	}

	public void updateHeatAndMoisture(double dishHeat, double dishMoisture)
	{
		heatMoisture = new List<string>();
		this.dishHeat = dishHeat;
		this.dishMoisture = dishMoisture;
		if (Math.Abs(dishMoisture) < .25 && Math.Abs(dishHeat) < .25) {
			heatMoisture.Add("Nothing stands out about this mixture's flavor");
		}
		if (Math.Abs(dishMoisture) < .25 && Math.Abs(dishHeat) > .25) {
			heatMoisture.Add("This mixture's moisture flavor is balanced.");
		}
		if (Math.Abs(dishMoisture) > .25 && Math.Abs(dishHeat) < .25) {
			heatMoisture.Add("This mixture's heat flavor is balanced.");
		}
		if (dishHeat >= .25 && dishHeat <= 3) {
			heatMoisture.Add("This has a warm flavor.");
		}
		if (dishHeat > 3) {
			heatMoisture.Add("This tasted very hot!");
		}
		if (dishHeat >= -.25 && dishHeat <= -3)
		{
			heatMoisture.Add("Tastes cool.");
		}
		if (dishHeat < -3)
		{
			heatMoisture.Add("A very cold mixture.");
		}
		if (dishMoisture >= .25 && dishMoisture <= 3)
		{
			heatMoisture.Add("Seems rather dry-tasting.");
		}
		if (dishMoisture > 3)
		{
			heatMoisture.Add("This tastes very dry!");
		}
		if (dishMoisture >= -.25 && dishMoisture <= -3)
		{
			heatMoisture.Add("This tastes moist.");
		}
		if (dishMoisture < -3)
		{
			heatMoisture.Add("This is quite the moist mixture!");
		}
	}

	public void updateProcess(string processColor, double idealValueMin, double idealValueMax, double timerValue)
	{
		timeFunctions = new List<string>();
		this.processColor = processColor;
		this.idealValueMin = idealValueMin;
		this.idealValueMax = idealValueMax;
		this.timerValue = timerValue;
		bool greater;
		if (timerValue > idealValueMax) {
			greater = true;
		} else {
			greater = false;
		}
		if (processColor.Equals ("DarkGreen")) {
			timeFunctions.Add ("Perfectly done!");
		}
		if (processColor.Equals("Green")){
			timeFunctions.Add("That last was well done.");
		}
		else if (processColor.Equals("OYellow") && greater)
		{
			timeFunctions.Add("This was a little overdone.");
		}
		else if (processColor.Equals("UYellow") && !greater)
		{
			timeFunctions.Add("Hmm. Could use a little more time.");
		}
		else if (processColor.Equals("URed") && !greater)
		{
			timeFunctions.Add("Did they even try to finish what they were doing?!");
		}
		else if (processColor.Equals("ORed") && greater)
		{
			timeFunctions.Add("That was a disaster! Can it be salvaged?");
		}
	}

	public string tasteTest() {
		if (heatMoisture.Count == 0) {
			updateHeatAndMoisture (dishHeat, dishMoisture);
		}
		if (timeFunctions.Count == 0) {
			updateProcess (processColor, idealValueMin, idealValueMax, timerValue);
		}
		string returnValue;
		Random random = new Random ();
		int randomIndex = random.Next (0, heatMoisture.Count + timeFunctions.Count);
		if (randomIndex >= heatMoisture.Count) {
			returnValue = timeFunctions [0];
		} else {
			returnValue = heatMoisture [randomIndex];
		}
		return returnValue;
	}

	public string tasteHeat(){
		if (heatMoisture.Count == 0) {
			updateHeatAndMoisture (dishHeat, dishMoisture);
		}
		string returnValue;
		Random random = new Random ();
		int randomIndex = random.Next (0, heatMoisture.Count - 1);
		returnValue = heatMoisture [randomIndex];
		heatMoisture.RemoveAt (randomIndex);
		return returnValue;
	}
}