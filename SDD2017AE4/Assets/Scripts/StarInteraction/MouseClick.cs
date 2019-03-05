using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class MouseClick : MonoBehaviour {

	//UI Canvas
	[Header("UI Canvas")]
	public Canvas panel;
	public Canvas SettingsMenu;
	public Canvas TelescopeView;
	public Canvas PauseScreen;
	public Canvas ErrorCanvas;
	public Canvas CanvasError;

	//UI Text
	[Header("UI Text")]
	public Text StarID;
	public Text StarTemp;
	public Text CanvasErrorText;
	public InputField StarClassInput;
	public Text ErrorText;

	//Raycasting
	[Header("Raycasting")]
	public RaycastHit StarHit;

	//Public Objects
	[Header("Public Objects")]
	public GameObject StarSelected;
	public Camera MainCamera;
	public GameObject starController;

	//Public Variables
	[Header("Flags")]
	public bool RaycastHitBool;
	public bool starsleft;
	public int remainingstars;
	public int tempstars;


	[Header("Public Variables")]
	public string StarHitName;
	public string StarClass;
	public string nameOfObjectHit;
	public string tagOfObjectHit;
	public string[] classifiedStars;


	public void Start(){
		panel.enabled = false;
		CanvasError.enabled = false;
		remainingstars = 20;
	}


	public void Update () {

		//Freezing Camera Movement if the MenuCanvas is up
		if ((panel.enabled == true) || (PauseScreen.enabled == true) || (SettingsMenu.enabled == true) || (ErrorCanvas.enabled == true) ) {
			Time.timeScale = 0;
			CanvasError.enabled = false;

		} else{
			Time.timeScale = 1;
		}
			
		//If the user presses the left mouse button
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("The user clicked.");

			if ((PauseScreen.enabled == true) || (SettingsMenu.enabled == true)) {
			} else {
				//If the canvas is minimised
				if ((panel.enabled == false) && (TelescopeView.enabled == true)) {
					CanvasError.enabled = false;
					Invoke ("ClassifiedStarsUpdate", 0f);
					Invoke ("StarCheck", 0f);
					Raycast ();
				} else {
					//Time.timeScale = 0;
					CanvasErrorText.text = "Enter Telescope View first";
					CanvasError.enabled = true;
					Invoke ("DisableCanvasRaycast", 2f);
				}
			}
		}

		Invoke ("ClassifiedStarsUpdate", 1f);
	}


	public void DisableCanvasRaycast(){
		//CanvasErrorText.text = "";
		CanvasError.enabled = false;
		//Time.timeScale = 1;
	}

	public void ClassifiedStarsUpdate(){
		//Checking the for the amount of remaining stars.
		tempstars = 20;
		for (int i = 0; i < 20; i++) {
			if (classifiedStars [i] != "") {

				tempstars = tempstars - 1;
				remainingstars = tempstars;
			}
		}
	}

	public void StarCheck(){
		//Debug.Log ("There are " + remainingstars + " elements empty");

		if (remainingstars <= 0) {
			//Debug.Log ("There are 0 remaining stars.");
			DestroyStars ();
		}
	}


	public void DestroyStars(){
		//Debug.Log ("DestoryStars was called.");

		//CanvasRaycastUpdate
		CanvasError.enabled = true;
		CanvasErrorText.text = "Regenerating stars...";


		//Destroy the classified stars
		for (int i = 0; i < 20; i++) {
			Destroy(GameObject.Find(classifiedStars[i]));
		}

		NullifyArray ();
	}

	public void NullifyArray(){	
		//Debug.Log ("NullifyArray was called");

		//Nullify the array
		for (int i = 0; i < 20; i++) {
			classifiedStars [i] = null;	
		}
		starController.GetComponent<SpawnItems> ().SpawnStars ();
	}


	//This function sents out a Raycast
	public void Raycast(){

		//This line assigns the result of the raycast to a flag (later used in an if-else decision)
		RaycastHitBool = Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out StarHit);
		Debug.DrawRay (MainCamera.transform.position, MainCamera.transform.forward * 10000f, Color.cyan, 5f);

		//If there is an object attached with a collider in front of the object sending out the raycast. This condition is true.
		if (RaycastHitBool == true) {

			//Assigning the name and tag of the object hit to a variable to make code simpler.
			tagOfObjectHit = StarHit.collider.tag.ToString ();
			nameOfObjectHit = StarHit.collider.name.ToString();

			if ((tagOfObjectHit == "Star") || (tagOfObjectHit == "StarPrefab")) {
				StarHitName = nameOfObjectHit;
				Invoke ("RaycastHit", 0f);
			} else {
				RaycastHitBool = false;
			}
		}
	}


	//This function will occur when the raycast hits the star. Here, the menu information is changed to select the star.
	public void RaycastHit()
	{
		//Finding the clone
		StarSelected = GameObject.Find (StarHitName);

		//Retreiving the values of the variable from the selected clone
		StarID.text = (StarSelected.GetComponent<StarInfo> ().starID).ToString ();
		StarTemp.text = (StarSelected.GetComponent<StarInfo> ().startemp).ToString() + " Kelvin";
		StarClass = (StarSelected.GetComponent<StarInfo> ().starclass).ToString ();


		//Updating the GUI
		TelescopeView.enabled = false;
		panel.enabled = true;
		//Time.timeScale = 0;
		Canvas.ForceUpdateCanvases ();



		//If the star has already been classified. Pop up ErrorCanvas
		for (int i = 0; i < 20; i++) {

			if (StarHitName == classifiedStars [i]) {

				ErrorCanvas.enabled = true;
				panel.enabled = false;

				ErrorText.text = "<Waiting for Input>";
				StarClassInput.text = "";
				Canvas.ForceUpdateCanvases ();

				//Time.timeScale = 0;
				i = 50;
			}
		}


	}

}