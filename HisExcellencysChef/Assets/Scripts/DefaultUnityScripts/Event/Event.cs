using UnityEngine;
using System.Collections;

public static class Event {
	public const string WIN = "Win";

	#region Audio Events

	public const string START_MUSIC = "PlayStartMusic";
	public const string GAME_MUSIC = "PlayGameMusic";

	public const string ADD_TO_DRY = "AddToDry";
	public const string ADD_TO_WET = "AddToWet";
	public const string START_SLOW_BOIL = "StartSlowBoil";
	public const string STOP_SLOW_BOIL = "StopSlowBoil";
	public const string START_FAST_BOIL = "StartFastBoil";
	public const string STOP_FAST_BOIL = "StopFastBoil";
	public const string MINCE = "Mince";
	public const string SIZZLE = "Sizzle";
	public const string SEASONING = "Seasoning";
	public const string FAST_CHOP = "FastChop";
	public const string SLOW_CHOP = "SlowChop";

	#endregion
}
