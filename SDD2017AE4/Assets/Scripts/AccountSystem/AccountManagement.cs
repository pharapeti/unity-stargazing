using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class AccountManagement : MonoBehaviour {

	[Header("UI")]
	public InputField usernameinput;
	public InputField passwordinput;
	public Button createaccount;
	public Text errortext;
	[Space(1)]

	[Header("Public")]
	public string[] storedaccount;
	public string accountname;
	public GameObject[] Objects;
	public int userLevel;
	public bool authenticObject;
	[Space(1)]

	//Private
	private string username;
	private string password;
	private int i;
	//private string delimiter = "*-*";

	//Awake Function is started once the script is ran and does not stop until the whole program is killed.
	public void Awake()
	{
		//Forces Unity not to clear cache from this script once at another scene.
		DontDestroyOnLoad(gameObject);
	}

	public void Start()
	{
		userLevel = 0;
	}
			
	public void OnClick()
	{
		//Debug.Log ("CreateAccount button was clicked.");
	
		//Error Trapping
		if((usernameinput.text == "")){
			errortext.text = "The Username field is empty.";
		}
		else {
			//Gathering data from input fields
			username = usernameinput.text;
			password = passwordinput.text;

			//Error Trapping
			if (passwordinput.text == "") {
				errortext.text = "The Password field is empty.";} 
			else {
				errortext.text = "Thank you for registering.";
				//Adding data to empty account
				for (int i = 0; i < 100;) {
					if (storedaccount [i] == "") 
					{
						storedaccount [i] = username + password;
						accountname = username + password;
						Debug.Log ("The account [" + storedaccount [i] + "] has been registered.");
						userLevel = 1;
						authenticObject = true;



						//SAVE ACCOUNT TO FILE
						BinaryFormatter bf = new BinaryFormatter();
						//Debug.Log(Application.persistentDataPath + ("/UserData." + accountname));

						//Creates a new file at the directory of the save path.
						FileStream file = File.Open(Application.persistentDataPath + ("/UserData." + accountname), FileMode.Create);

						//Clearing temp files and starting from  a new file
						UserData newData = new UserData();

						//Asigning Variables
						newData.username = accountname;
						newData.userlevel = userLevel;

						//Encodes newData to file.
						bf.Serialize(file, newData);
						file.Close();
						//SAVE ACCOUNT TO FILE

						i = 102;
						break;
					} else {
						i = i + 1;
					}
				}
			}
		}
	}
}