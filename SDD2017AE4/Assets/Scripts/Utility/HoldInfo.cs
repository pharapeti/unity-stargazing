using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInfo : MonoBehaviour {

	public string UserInfo;
	public int UserLevel;

	public bool Saved;

	public void Start(){

		if((Saved == false) && (UserInfo != null)){
			DontDestroyOnLoad (this);
			Saved = true;
		}
	}
}
