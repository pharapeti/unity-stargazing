using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Scripting;

public class PauseMenu : MonoBehaviour {

	//UI
	[Header("UI")]
	public Canvas PauseMenuCanvas;
	public Canvas SettingsMenuCanvas;
	public Canvas CanvasMenu;
	public Canvas TelescopeCanvas;
	public Canvas CrosshairCanvas;
	[Space(1)]

	public Button BackButton;
	public Text MagnificationLabel;
	public Text EyepieceText;
	public Text ErrorText;
	[Space(1)]

	//Public Gameobjects
	[Header("Gameobjects")]
	public GameObject TelescopeObject;
	public GameObject PlayerModel;
	[Space(1)]

	//Camera
	[Header("Camera Settings")]
	public Camera mainCamera;
	public int defaultFOV;
	public int scopeFOV;
	public int scope2FOV;
	public int scope3FOV;
	public int scopecount;
	[Space(1)]

	//Flags
	[Header("Flags")]
	public bool IsTelescopeViewUp;

	//UserData
	public int UserLevel;


	public void Start(){

		//Initializing the FOV's
		defaultFOV = 60;
		scopeFOV = 30;
		scope2FOV = 20;
		scope3FOV = 10;
		scopecount = 1;

		//Enabling and Disabling Canvas'
		mainCamera.fieldOfView = defaultFOV;
		CrosshairCanvas.enabled = true;
		PauseMenuCanvas.enabled = false;
		SettingsMenuCanvas.enabled = false;
		TelescopeCanvas.enabled = false;

		//Refereencing Canvas;
		PauseMenuCanvas = GameObject.Find ("CanvasPause").GetComponent<Canvas>();
		SettingsMenuCanvas = GameObject.Find ("CanvasPauseSettings").GetComponent<Canvas>();
		BackButton = GameObject.Find ("BackButton").GetComponent<Button> ();
	}

	public void Update(){

		if (TelescopeCanvas.enabled == true) {
			TelescopeObject.SetActive (false);
		} else {
			mainCamera.fieldOfView = defaultFOV;
			TelescopeObject.SetActive (true);
		}
			
		//Changing Telescope Magnification
		if ((Input.GetKeyDown (KeyCode.C)) && (TelescopeCanvas.enabled == true)) {
			TelescopeMagnification ();
		}
			
		//This toggles the Pause Menu
		if (Input.GetKeyDown (KeyCode.P)) {
			TogglePauseMenu ();
		}
			
		//This toggles the Telescope View
		if (Input.GetKeyDown (KeyCode.T)) {
			if (TelescopeCanvas.enabled == true) {
				TelescopeObject.SetActive (true);
				mainCamera.fieldOfView = defaultFOV;
				TelescopeCanvas.enabled = false;
			} else {
				ToggleTelescope ();
			}
		}
	}

	public void TogglePauseMenu(){

		if (PauseMenuCanvas.enabled == true) {
			PauseMenuCanvas.enabled = false;
			mainCamera.fieldOfView = defaultFOV;
			TelescopeObject.SetActive (true);
			Time.timeScale = 1;
		} else {
			SettingsMenuCanvas.enabled = false;
			CanvasMenu.enabled = false;
			TelescopeCanvas.enabled = false;
			PauseMenuCanvas.enabled = true;
			mainCamera.fieldOfView = defaultFOV;
			TelescopeObject.SetActive (false);
			Time.timeScale = 0;
		}
	}

	public void ResumeGame(){
		PauseMenuCanvas.enabled = false;
		Time.timeScale = 1;
	}
		
	public void Settings(){
		//Disabling the Pause Screen and then enabling the SettingsMenu.
		PauseMenuCanvas.enabled = false;
		SettingsMenuCanvas.enabled = true;
	}
		
	public void QuitGame(){
		//if (EditorUtility.DisplayDialog ("Exit Program?", "Are you sure you would like the quit the program?", "Yes", "No")) {
		Debug.Log ("The user quitted the program.");
		//	Application.Quit ();
	//	}
		Application.Quit ();
	}


	public void BackButtonClick(){
		//Enabling the Pause Screen and then Disabling the SettingsMenu.
		SettingsMenuCanvas.enabled = false;
		PauseMenuCanvas.enabled = true;
	}

	public void TelescopeMagnification(){

		UserLevel = GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().UserLevel;
		scopecount = scopecount + 1;

		//Linear Switching of Magnification until scopecount == 4
		if (scopecount == 4) {
			scopecount = 1;
		}

		switch (scopecount) 
		{
		case 1:
			mainCamera.fieldOfView = scopeFOV;
			MagnificationLabel.text = "20x";
			EyepieceText.text = "Celestron EQ130:\n10mm Eyepiece";
			ErrorText.text = "";
			break;
		case 2:
			if (UserLevel < 5) {
				ErrorText.text = "You must be atleast Level 5 to use this magnification.";
				MagnificationLabel.text = "40x";
				EyepieceText.text = "Celestron EQ130:\n8mm Eyepiece";
			} else{
				mainCamera.fieldOfView = scope2FOV;
				ErrorText.text = "";
				MagnificationLabel.text = "40x";
				EyepieceText.text = "Celestron EQ130:\n8mm Eyepiece";
			}
			break;
		case 3:
			if (UserLevel < 10) {
				ErrorText.text = "You must be atleast Level 10 to use this magnification.";
				MagnificationLabel.text = "60x";
				EyepieceText.text = "Celestron EQ130:\n6mm Eyepiece";
			} else{
				mainCamera.fieldOfView = scope3FOV;
				ErrorText.text = "";
				MagnificationLabel.text = "60x";
				EyepieceText.text = "Celestron EQ130:\n6mm Eyepiece";
			}
			break;
		}
	}

	public void ToggleTelescope(){
		//Checks if any Canvas' are enabled at the time of this function
		if ((PauseMenuCanvas.enabled == true) || (CanvasMenu.enabled == true) || (SettingsMenuCanvas.enabled == true)) {
			TelescopeObject.SetActive(true);
			mainCamera.fieldOfView = defaultFOV;
			TelescopeCanvas.enabled = false;
			IsTelescopeViewUp = false;
			//PlayerModel.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
		} else 	{
			TelescopeObject.SetActive(true);
			mainCamera.fieldOfView = scopeFOV;
			TelescopeCanvas.enabled = true;
			IsTelescopeViewUp = true;
			//PlayerModel.transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
		}

	}

}