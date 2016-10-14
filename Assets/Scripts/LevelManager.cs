using UnityEngine;
using System.Collections;



public class LevelManager : MonoBehaviour {


	public static LevelManager instance = null;
	// end

	// делаем синглтон для левел менеджера чтобы считать жизни
	void Start () {
		if (instance == null) instance = this;
		else if (instance!=this) Destroy(gameObject);
	}

	public void LoadLevel (string name) {

		Application.LoadLevel(name);
	}

	public void QuitRequest () {
		Debug.Log ("I want to quit!");
		Application.Quit();
	}

	public void LoadNextLevel() {

		Application.LoadLevel(Application.loadedLevel + 1);
	}



}
