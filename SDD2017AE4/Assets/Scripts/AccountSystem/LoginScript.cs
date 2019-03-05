using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class LoginScript : MonoBehaviour {

	[Header("UI")]
	//Public UI
	public InputField usernameinput;
	public InputField passwordinput;
	public Button loginbutton;
	public Button playbutton;
	public Text errortext;

	[Header("Arrays")]
	//Public Arrays
	public string[] AccountArrayInLogin;
	//public string[] tocopy;
	//public GameObject[] Objects;

	[Header("Variables")]
	//Public Variables
	public string accountLoginAttempt;
	public string accountWhichLoggedIn;
	public int accountLevel;

	[Header("Flags")]
	public bool authenticObject;
	public bool ArrayExists;
	public bool loginaccepted;
	public bool savefilexists;

	//Private
	private string username;
	private string password;
	private int i;

	//FileStream
	public FileStream file;


	public void Start()
	{
		//Initialisation
		loginaccepted = false;
		DontDestroyOnLoad (this);
	}
		
	public void OnLoginClick(){
		//Debug.Log ("The login button was clicked.");
		savefilexists = false;
		ArrayExists = false;

		//Gathering data
		username = usernameinput.text;
		password = passwordinput.text;

		//Temp Login Creation
		accountLoginAttempt = username + password;

		//Checks if the save file exists
		if (File.Exists (Application.persistentDataPath + ("/UserData." + accountLoginAttempt))) {
			//Save file exists
			Debug.Log ("A matching savefile exists.");
			savefilexists = true;
			SaveFileExists ();
		} else {
			//No save file exists
			Debug.Log ("A matching savefile does not exists.");
			errortext.text = "No matching save file exists.";
			savefilexists = false;
			NoSaveFileExists ();
		}
	}

	//Loads the SaveFile
	public void SaveFileExists(){

		//Debug.Log ("SaveFileExists was called.");

		//UserData userdata = null;

		//Creating a new BinaryFormatter
		BinaryFormatter bf = new BinaryFormatter ();

		//Debug.Log(Application.persistentDataPath + ("/UserData." + accountLoginAttempt));

		//Making a file which now contains the Un-Serialized File
		FileStream file = File.Open (Application.persistentDataPath + ("/UserData." + accountLoginAttempt), FileMode.Open);

		//Assigning the values of the saved file to the new file
		UserData newData = (UserData)bf.Deserialize (file);

		//Debug.Log ("Line 114");

		//Assigning the values from the save file to the variables in this script.
		accountLoginAttempt = newData.username;
		accountWhichLoggedIn = newData.username;
		accountLevel = newData.userlevel;

		//Debug.Log ("Line 120");

		//Assinging the values from this script to the _PlayerManager script. (This is where the UI gets it's info).
		GameObject.Find ("_AccountInfoSaveHolder").GetComponent<HoldInfo> ().UserInfo = accountLoginAttempt;
		GameObject.Find ("_AccountInfoSaveHolder").GetComponent<HoldInfo> ().UserLevel = accountLevel;

		//Debug.Log ("Line 127");

		errortext.text = "Login Successful.";

		playbutton.enabled = true;
		playbutton.interactable = true;

		Canvas.ForceUpdateCanvases ();
	}

	//Creates a new Account
	public void NoSaveFileExists(){

		//Checks if the Gameobject Exists
		if (GameObject.Find ("_ScriptsCreateAccount") != null) {
			Debug.Log ("_ScriptsCreateAccount exists.");
			ArrayExists = true;
		} else {
			Debug.Log ("_ScriptsCreateAccount does not exist.");
			errortext.text = "Create an account first.";
			ArrayExists = false;
		}


		//If the Array exists
		if ((ArrayExists == true)) {
			string[] storedaccount = GameObject.Find ("_ScriptsCreateAccount").GetComponent<AccountManagement> ().storedaccount;
			AccountArrayInLogin = storedaccount;


			//If there were no values in the input fields
			if (((usernameinput.text == "")) || (passwordinput.text == "")) {
				errortext.text = "This login is invalid.";
				errortext.enabled = true;
				playbutton.interactable = false;
				playbutton.enabled = false;
				loginaccepted = false;
				Canvas.ForceUpdateCanvases ();
			} else {

				//Gathering data
				username = usernameinput.text;
				password = passwordinput.text;

				//Temp Login Creation
				accountLoginAttempt = username + password;
				Debug.Log ("The temporary account is " + accountLoginAttempt);

				//Checking the login credentials
				for (int i = 0; i <= 100; i++) {
					if (accountLoginAttempt == AccountArrayInLogin [i]) {

						//Login Correct
						Debug.Log ("User Login is correct.");
						loginaccepted = true;
						accountWhichLoggedIn = accountLoginAttempt;

						//Updating UI
						errortext.text = "This login is correct.";
						errortext.enabled = true;
						playbutton.interactable = true;
						playbutton.enabled = true;
						Canvas.ForceUpdateCanvases ();

						//Updating UserLevel
						if ((GameObject.Find ("_ScriptsCreateAccount").GetComponent<AccountManagement> ().userLevel == 0) || (accountLevel == 0)) {
							accountLevel = 1;
							GameObject.Find ("_ScriptsCreateAccount").GetComponent<AccountManagement> ().userLevel = 1;
						}

						break;

					} else {
						//Login Incorrect
						Debug.Log ("User login is incorrect.");
						errortext.text = "This login is incorrect.";
						errortext.enabled = true;

						//Nullifying InputFieds and Labels
						accountLoginAttempt = "";
						usernameinput.text = "";
						passwordinput.text = "";

						//Updating UI
						playbutton.enabled = false;
						playbutton.interactable = false;
						loginaccepted = false;
						Canvas.ForceUpdateCanvases ();
					}
				}
			}
		}
	}
}