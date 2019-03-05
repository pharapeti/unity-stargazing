using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

	public GameObject[] MusicObjects;

	void Awake(){

		GameObject[] MusicObjects = GameObject.FindGameObjectsWithTag ("Music");

		if (MusicObjects.Length > 1) {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);

	}

}
