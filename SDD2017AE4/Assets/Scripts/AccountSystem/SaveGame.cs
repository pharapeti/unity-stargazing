using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour {

	//UI 
	[Header("UI")]
	public Canvas UpdateCanvas;
	public Text UpdateText;

	//Saved Variables
	[Header("Saved Variables")]
	public string savedname;
	public string attemptedlogin;
	public int savedlevel;

	//Loaded Variables
	[Header("Loaded Variables")]
	public string loadedname;
	public int loadedlevel;


//Redundant Code
//	public void Awake(){
//		if (SceneManager.GetActiveScene ().name == "GameScene") {
//			Load ();
//		}
//	}

	public void Start(){
		try{
			UpdateCanvas.enabled = false;
		}
		catch{
		}
		finally{
		}
	}

	//This function will assign the values of the username and level to the variables used in this script.
	public void InitializeSave(){
		Debug.Log ("The user clicked the save button.");

		//if (EditorUtility.DisplayDialog ("Save Progress?", "Are you sure you would like to save your progess?", "Yes", "No")) {
		Debug.Log ("The user saved.");
		//Assigns the values from _PlayerManager to the variables of this script for saving purposes.
		savedname = GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().PlayerName;
		savedlevel = GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().UserLevel;

		Save ();
	}
		
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();

		Debug.Log(Application.persistentDataPath + ("/UserData." + savedname));

		//Creates a new file at the directory of the save path.
		FileStream file = File.Open(Application.persistentDataPath + ("/UserData." + savedname), FileMode.Create);

		//Clearing temp files and starting from  a new file
		UserData newData = new UserData();

		//Assigning Variables
		newData.username = savedname;
		newData.userlevel = savedlevel;

		//Encodes newData to file.
		bf.Serialize(file, newData);
		file.Close();

		UpdateCanvas.enabled = true;
		Invoke ("DisablePopup", 0.5f);
	}

	public void DisablePopup(){
		UpdateCanvas.enabled = false;
	}


//This function is redundant. A new version of this function is used in the Login script
//	public void Load(){
//		attemptedlogin = GameObject.Find ("_ScriptsLogin").GetComponent<LoginScript> ().accountLoginAttempt;
//
//
//		if (File.Exists (Application.persistentDataPath + ("/UserData." + attemptedlogin))) {
//			BinaryFormatter bf = new BinaryFormatter ();
//
//			//Making a file which now contains the Un-Serialized File
//			FileStream file = File.Open (Application.persistentDataPath + ("/UserData." + attemptedlogin), FileMode.Open);
//
//			//Assigning the values of the saved file to the new file
//			UserData newData = (UserData)bf.Deserialize (file);
//
//			//Assigning the values from the save file to the variables in this script.
//			loadedname = newData.username;
//			loadedlevel = newData.userlevel;
//
//			//Close the file
//			file.Close ();
//
//			//Assinging the values from this script to the _PlayerManager script. (This is where the UI gets it's info).
//			GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().PlayerName = loadedname;
//			GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().UserLevel = loadedlevel;
//			Canvas.ForceUpdateCanvases ();
//
//		} else {
//			Debug.Log("No save files exist to load from.");
//		}
//	}

}