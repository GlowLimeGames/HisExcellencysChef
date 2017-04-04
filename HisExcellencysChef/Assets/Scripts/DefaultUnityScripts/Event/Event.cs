using UnityEngine;
using System.Collections;

public static class Event {
	public const string WIN = "Win";

	#region Audio Events

	public const string START_MUSIC = "PlayStartMusic";
	public const string GAME_MUSIC = "PlayGameMusic";
	public const string END_MUSIC = "PlayEndMusic";
	public const string AMBIENCE_MUSIC = "PlayAmbience";

	public const string ADD_TO_DRY = "AddToDry";
	public const string ADD_TO_WET = "AddToWet";
	public const string START_SLOW_BOIL = "StartSlowBoil";
	public const string STOP_SLOW_BOIL = "StopSlowBoil";
	public const string START_FAST_BOIL = "StartFastBoil";
	public const string STOP_FAST_BOIL = "StopFastBoil";
	public const string SIZZLE = "Sizzle";
	public const string SEASONING = "Seasoning";
	public const string FAST_CHOP = "FastChop";
	public const string SLOW_CHOP = "SlowChop";
	public const string FRYLOOP = "FryLoop";
	public const string GRINDDRY = "GrindDry";
	public const string GRINDWET = "GrindWet";
	public const string GUICLICK = "GUIclick";
	public const string CLOCKOUT = "ClockOutOfTime";
	public const string DISHDELIVERED = "DishDelivered";
	public const string ENDOFLEVEL1 = "EndOfLevel1";
	public const string ENDOFLEVEL2 = "EndOfLevel2";

	public const string GET_CHICKEN = "GetChicken";
	public const string GET_EGG = "GetEgg";
	public const string GET_FLOUR = "GetFlour";
	public const string GET_MUSTARD = "GetMustard";
	public const string GET_SPINACH = "GetSpinach";
	public const string GET_WATER = "GetWater";

	public const string AEMILIA = "Aemilia";
	public const string ELOISE = "Eloise";
	public const string GUILBERT = "Guilbert";
	public const string AMAUD = "Amaud";
	public const string LADY = "LadyTalk";

	public const string MALEWALK = "MaleStep";
	public const string FEMALEWALK = "FemaleStep";

	public const string DISHGOOD = "DishGood";
	public const string DISHOK = "DishOk";
	public const string DISHBAD = "DishAwful";

	#endregion

	#region Cooking Actions

	public const string PICKUP = "PickUp";
	public const string PUTDOWN = "PutDown";
	public const string TASTEBAD = "ChefTestBad";
	public const string TASTENEUTRAL = "ChefTestNeutral";
	public const string TASTEGOOD = "ChefTestGood";

	public const string CHOP = "Chop";
	public const string SLIVER = "Sliver";
	public const string MINCE = "Mince";
	public const string POUND = "Pound";
	public const string FRY = "Fry";
	public const string SEETHE = "Seethe";
	public const string BAKE = "Bake";
	public const string ROAST = "Roast";
	public const string BOIL = "Boil";

	#endregion
}
