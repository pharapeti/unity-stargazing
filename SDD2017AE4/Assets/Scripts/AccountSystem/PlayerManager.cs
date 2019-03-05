using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	//Public Variables
	[Header("Public")]
	public string PlayerName;
	public int UserLevel;

	//UI Elements
	[Header("UI")]
	public Text onScreenLevel;


	public void Start()
	{
		DontDestroyOnLoad (this);

		//Error Trapping
		if (PlayerName == "") {
			PlayerName = "undefined";
		}
		if (UserLevel == 0) {
			UserLevel = 1;
		}

		//Finds a  _ScriptsCreateAccount destroys it since there is no need for it to be in the GameScene.
		if (GameObject.Find("_ScriptsCreateAccount") != null)
		{
			//Destroys the gameobject.
			Destroy (GameObject.Find("_ScriptsCreateAccount"));
		}

		//Finds a duplicate _ScriptsLogin object destroys it.
		if (GameObject.Find("_ScriptsLogin") != null)
		{
			//Checks if the accountWhichLoggedIn value is null
			if (GameObject.Find ("_ScriptsLogin").GetComponent<LoginScript> ().accountWhichLoggedIn == "") 
			{
				//Destroys the gameobject.
				Destroy (GameObject.Find ("_ScriptsLogin"));
				Debug.Log ("Null duplicate of [_ScriptsLogin] was destoryed.");
			}
		}

		//Call the GatherInfo function.
		Invoke ("GatherInfo", 0f);
	}

	//Collect the UserName and UserLevel from Registration Scene
	public void GatherInfo(){

		//Collecting the PlayerName and UserLevel from the DontDestroyOnLoad(LoginScene).
		if (GameObject.Find ("_AccountInfoSaveHolder") != null) {
			PlayerName = GameObject.Find ("_AccountInfoSaveHolder").GetComponent<HoldInfo> ().UserInfo;
			UserLevel = GameObject.Find ("_AccountInfoSaveHolder").GetComponent<HoldInfo> ().UserLevel;
		} else {
			Debug.Log ("_AccountInfoSaveHolder does not exist in this scene.");
		}

		//Error Trapping
		if (PlayerName == "") {
			PlayerName = "undefined";
		}
		if (UserLevel == 0) {
			UserLevel = 1;
		}

		//Debugging Output Statements
		Debug.Log ("PlayerName = [" + PlayerName + "] :: UserLevel  = [" + UserLevel + "]"); 

		onScreenLevel.text = "Experience Level: " + UserLevel;
		Canvas.ForceUpdateCanvases();
	}


	public void SaveGame(){
		//Insert a command which disables mouselook in the back.
		Debug.Log ("The save button was clicked.");
	}
		
}