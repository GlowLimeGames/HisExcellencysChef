/*
 * Author(s): Isaiah Mann
 * Description: Controls the game logic
 */

using UnityEngine;
using System.Collections.Generic;

public class GameController : SingletonController<GameController> {
	[SerializeField]
	bool playMusicOnStart = true;
	CookingController cooking;

	protected override void FetchReferences () {
		base.FetchReferences ();
		if (playMusicOnStart) {
			playMusic();
		}
		cooking = CookingController.Instance;
	}
		
	void playMusic () {
		EventController.Event(Event.GAME_MUSIC);
	}
}