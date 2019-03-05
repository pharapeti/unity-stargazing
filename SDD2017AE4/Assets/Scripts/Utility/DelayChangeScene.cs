using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayChangeScene : MonoBehaviour {

	// Use this for initialization
	public IEnumerator Start () {

		//Delays the scene for 3 seconds. Artificial delay
		yield return new WaitForSeconds(0.8f);

		//Application.LoadLevel ("GameScene");
		SceneManager.LoadScene("GameScene");
	}
}