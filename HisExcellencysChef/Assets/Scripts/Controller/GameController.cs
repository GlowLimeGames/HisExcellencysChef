/*
 * Author(s): Isaiah Mann
 * Description: 
 */

using UnityEngine;

public class GameController : SingletonController<GameController> {
	[SerializeField]
	bool playMusicOnStart = true;

	protected override void FetchReferences () {
		base.FetchReferences ();
		if (playMusicOnStart) {
			playMusic();
		}
	}

	void playMusic () {
		EventController.Event(Event.GAME_MUSIC);
	}
}