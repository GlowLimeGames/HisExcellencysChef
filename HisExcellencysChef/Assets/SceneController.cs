using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SceneController : SingletonController<SceneController> {

	protected override void FetchReferences () {
		base.FetchReferences ();
	}

	public void Options(){

	}

	public void LoadScene(int sceneIndex){
		SceneManager.LoadScene (sceneIndex);
	}

	public void ReturnMainMenu(){
		Save ();
		SceneManager.LoadScene (0);
		Destroy (GameController.Instance);
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/Save.sav");

		PlayerData data = new PlayerData ();
		data.Fame = GameController.Instance.Fame;
		data.Infamy = GameController.Instance.Infamy;

//		data.pantry = GameController.Instance.inventory.foods;
		data.poisoned = GameController.Instance.GetComponent<GuestController> ().poisonedList;


		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/Save.sav")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Save.sav", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);

			GameController.Instance.Fame = data.Fame;
			GameController.Instance.Infamy = data.Infamy;

//			GameController.Instance.inventory.foods = data.pantry;
			GameController.Instance.GetComponent<GuestController> ().poisonedList = data.poisoned;


			file.Close();
		}

	}

	public void ClearSave(){
		GameController.Instance.Fame = 0;
		GameController.Instance.Infamy = 0;

		GameController.Instance.GetComponent<GuestController> ().poisonedList.Clear ();

		Save ();
		Application.Quit ();
	}
}

[Serializable]
class PlayerData
{
	public int Fame;
	public int Infamy;

	public Inventory.InventoryNode[] pantry;

	public List<GuestDescriptor> poisoned = new List<GuestDescriptor>();
}


