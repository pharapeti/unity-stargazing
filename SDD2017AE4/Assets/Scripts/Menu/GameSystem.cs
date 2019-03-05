using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour {

	//UI
	[Header("UI")]
	public Canvas MenuCanvas;
	public Canvas ErrorCanvas;
	public Canvas BannerCanvas;
	public Canvas TelescopeCanvas;
	public InputField starclassinput;
	public Button enterbutton;
	public Text ErrorText;
	public Text UserExperience;
	public Text UserLevelBanner;
	public Text UnlockText;

	//Public Variables
	[Header("Public")]
	public int UserLevel;
	public string UserGuessClass;
	public string StarClass;
	public string CloneName;

	//Public Objects
	[Header("Objects")]
	public GameObject PlayerModel;

	//Audio
	[Header("Audio")]
	public AudioSource audio;
	public AudioClip correct;
	public AudioClip incorrect;



	public void Awake(){
		MenuCanvas.enabled = false;
		BannerCanvas.enabled = false;
		Time.timeScale = 1;
	}

	public void Start(){
		audio = GetComponent<AudioSource>();
	}

	public void OnClick(){
		//Retreiving the  StarClass from the MouseClick.cs
		StarClass = gameObject.GetComponent<MouseClick> ().StarClass;

		//Retreiving the CloneName from MouseClick.cs
		CloneName = gameObject.GetComponent<MouseClick> ().StarHitName;

		string[] classifiedstars = gameObject.GetComponent<MouseClick> ().classifiedStars;

		//If the star has already been classified. Pop up ErrorCanvas
		for (int i = 0; i < 20; i++) {

			if (CloneName == classifiedstars [i]) {

				Debug.Log ("Already exists in the array.");
				ErrorCanvas.enabled = true;
				MenuCanvas.enabled = false;

				ErrorText.text = "<Waiting for Input>";
				starclassinput.text = "";
				Canvas.ForceUpdateCanvases ();
				break;
			}
		}
			
		//Collects the UserLevel from the _Scripts's PlayerManager script.
		Debug.Log ("The class input button was clicked.");
		UserLevel = GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().UserLevel;

		//Collects the user guess from the UI inputfield.
		UserGuessClass = starclassinput.text;
		Debug.Log ("The user entered " + UserGuessClass);

		//Checks if the user's guess matches the actual starclass.
		if (UserGuessClass == StarClass) {
			Debug.Log ("User guess was correct.");
			ErrorText.text = "<Correct>";

			//Adds the classified star to the array
			for (int i = 0; i < 20; i++) {
				//There was space
				if (classifiedstars [i] == "") {
					classifiedstars [i] = CloneName;

					Debug.Log ("Classified star added into array");
					ErrorText.text = "<Waiting for Input>";
					starclassinput.text = "";
					Canvas.ForceUpdateCanvases ();
					i = 30;
					break;
				}
			}


			//If the guess is correct, the userlevel is incremented.
			UserLevel = UserLevel + 1;
			UserExperience.text = "Experience Level: " + UserLevel.ToString ();
			GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().UserLevel = GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().UserLevel + 1;
			GameObject.Find ("_AccountInfoSaveHolder").GetComponent<HoldInfo> ().UserLevel = GameObject.Find ("_AccountInfoSaveHolder").GetComponent<HoldInfo> ().UserLevel + 1;
			Canvas.ForceUpdateCanvases();

			//Incorrect Sound
			if (GameObject.Find ("_Scripts").GetComponent<VolumeSlider> ().Audio.mute == false) {
				audio.PlayOneShot(correct, 0.7F);
			} else {
				audio.PlayOneShot(correct, 0F);
			}

			Invoke ("CorrectGuess", 0f);

		} else {
			Debug.Log ("User guess was incorrect.");

			//Incorrect Sound
			if (GameObject.Find ("_Scripts").GetComponent<VolumeSlider> ().Audio.mute == false) {
				audio.PlayOneShot(incorrect, 0.7F);
			} else {
				audio.PlayOneShot(incorrect, 0F);
			}
				
			if (ErrorText.text == "<Incorrect>") {
				ErrorText.text = "<Invalid>";
			} else{
				ErrorText.text = "<Incorrect>";
			}
		}
	}

	public void CorrectGuess(){
		//One less star
		PlayerModel.GetComponent<MouseClick> ().remainingstars -= - 1;
			
		MenuCanvas.enabled = false;
		TelescopeCanvas.enabled = false;	
	
		//Enables the achievement window
		BannerCanvas.enabled = true;
		UnlockText.enabled = true;
		UserLevelBanner.text = UserLevel.ToString ();
		UserLevel = GameObject.Find ("_Scripts").GetComponent<PlayerManager> ().UserLevel;
		UnlockText.text = "Level Up!";


		if (UserLevel == 5) {
			UnlockText.text = "8mm Eyepiece Unlocked!";
		}

		if (UserLevel == 10) {
			UnlockText.text = "6mm Eyepiece Unlocked!";
		}

		Invoke ("CorrectGuessBanner", 3f);
	}

	public void CorrectGuessBanner(){
		BannerCanvas.enabled = false;
		UnlockText.enabled = false;
		ClosePanel();
	}

	public void ClosePanel(){
		TelescopeCanvas.enabled = true;
		MenuCanvas.enabled = false;
		Time.timeScale = 1;
		ErrorText.text = "<Waiting for Input>";
		starclassinput.text = "";
		Canvas.ForceUpdateCanvases ();
	}
}