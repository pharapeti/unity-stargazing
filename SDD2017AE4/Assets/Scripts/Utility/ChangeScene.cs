using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void ChangeSceneTo (string sceneToChangeTo)
	{
		//The scene is changed to a value set in the Unity Inspector.
		//Application.LoadLevel (sceneToChangeTo);
		SceneManager.LoadScene(sceneToChangeTo);
	}
}