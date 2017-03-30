using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LadyFeedback
{
	private List<string> heatMoisture, timeFunctions;
	private double dishHeat, dishMoisture, idealValueMin, idealValueMax, timerValue;
	private string processColor;
	//Heat of dish, moisture of dish, color of process, ideal cooking time, current time
	public LadyFeedback(double dishHeat, double dishMoisture) {
		this.dishHeat = dishHeat;
		this.dishMoisture = dishMoisture;
		heatMoisture = new List<string>();
		updateAll (this.dishHeat, this.dishMoisture);
	}

	public void updateAll(double dishHeat, double dishMoisture)
	{
		updateHeatAndMoisture(dishHeat, dishMoisture);
	}

	public void updateHeatAndMoisture(double dishHeat, double dishMoisture)
	{
		heatMoisture = new List<string>();
		this.dishHeat = dishHeat;
		this.dishMoisture = dishMoisture;
		if (Math.Abs(dishHeat) < .25f && (Math.Abs(dishMoisture) < .25f)){
			heatMoisture.Add("The <course name>'s flavors were delectably and wholesomely balanced. Well done.");
		}
		if (Math.Abs(dishHeat) < .25f && (dishMoisture > .26f && dishMoisture <= 2f)) {
			heatMoisture.Add("The <course name> was almost balanced, but could have been less dry.");
		}
		if (Math.Abs(dishHeat) < .25f && (dishMoisture >= 2.01f && dishMoisture < 3.49f)) {
			heatMoisture.Add("The <course name>'s heat flavors were nicely balanced, but it was unhealthily dry.");
		}
		if (Math.Abs(dishHeat) < .25f && (dishMoisture < -.26f && dishMoisture >= -2f)) {
			heatMoisture.Add("The <course name> was a little too moist, but otherwise balanced. ");
		}
		if (Math.Abs(dishHeat) < .25f && (dishMoisture <= -2.01f && dishMoisture > -3.49f)) {
			heatMoisture.Add("Too much moist food is unhealthy for the body, and the <course name> was very moist.");
		}
		if ((dishHeat < 2.0f && dishHeat >= .26f) && Math.Abs(dishMoisture) < .25f)
		{
			heatMoisture.Add("The <course name> was touch overly warmly flavored.");
		}
		if ((dishHeat >= 2.0f && dishHeat < 3.49f) && Math.Abs(dishMoisture) < .25f)
		{
			heatMoisture.Add("I could barely stomach the <course name>. Its humours were too hot for health or comfort.");
		}
		if ((dishHeat < -.26f && dishHeat >= -2f) && Math.Abs(dishMoisture) < .25f)
		{
			heatMoisture.Add("The moisture flavors of the <course name> were well balanced, but it tasted a bit too cold.");
		}
		if ((dishHeat <= -2.0f && dishHeat > -3.49f) && Math.Abs(dishMoisture) < .25f)
		{
			heatMoisture.Add("I do enjoy cold humours, but the <course name> was far too cold for comfort.");
		}
		if (((dishHeat >= .25f && dishHeat <= 2f) && dishMoisture >= .25f) ||(dishHeat >= 3.5f && dishMoisture <= 3.5f))
		{
			heatMoisture.Add("The <course name> had far too much fire food. It needed more Cold and Moist dishes.");
		}
		if (((dishMoisture >= .25f && dishMoisture <= 2f) && dishHeat >= .25f)|| (dishHeat >= 3.5f && dishMoisture >= -3.5f))
		{
			heatMoisture.Add("The <course name> was too much of the Air. Include more Cold and Dry dishes.");
		}
		if (((dishHeat <= -.25f && dishHeat >= -2f) && dishMoisture >= .25f)||dishHeat <= 3.5f && dishMoisture >= 3.5f)
		{
			heatMoisture.Add("The <course name> was overly earthy. It could have used more Hot and Moist flavors.");
		}
		if (((dishHeat <= -.25f && dishHeat >= -2f) && dishMoisture <= -.25f)||dishHeat <= -3.5f && dishMoisture <= -3.5f)
		{
			heatMoisture.Add("The <course name> had too much water food. Include more Hot and Dry flavors next time.");
		}
		if (Math.Abs(dishMoisture) >= 4 && Math.Abs(dishHeat) >= 4)
		{
			heatMoisture.Add("I don't even know what to say. You unbalanced all the flavors in the <course name> completely. It was entirely unhealthy.");
		}

		if (heatMoisture.Count == 0) {
			heatMoisture.Add ("Ayyy that's pretty guuud!");
		}
	}

	public string tasteTest() {
		if (heatMoisture.Count == 0) {
			updateHeatAndMoisture (dishHeat, dishMoisture);
		}
		string returnValue;
		Random random = new Random ();
		int randomIndex = random.Next (0, heatMoisture.Count);
		returnValue = heatMoisture [randomIndex];
		return returnValue;
	}
}