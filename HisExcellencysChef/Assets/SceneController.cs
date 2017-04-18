using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonController<SceneController> {

	protected override void FetchReferences () {
		base.FetchReferences ();
	}

	public void Options(){

	}

	public void LoadScene(){

	}

	public void ReturnMainMenu(){
		SceneManager.LoadScene (0);
		Destroy (GameController.Instance);
	}


}
