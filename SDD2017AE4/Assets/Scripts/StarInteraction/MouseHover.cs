using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MouseHover : MonoBehaviour {

	//UI
	public Canvas HoverCanvas;
	public Canvas TelescopeCanvas;
	public Text HoverCanvasText;

	//Objects
	public GameObject PlayerModel;
	public Camera MainCamera;

	//Raycasting
	public bool RaycastBool;
	public RaycastHit StarHit;


	public void Update () {
		Invoke("CheckIfClassified", 0.2f);
	}

	public void CheckIfClassified(){

		if (TelescopeCanvas.enabled == true) {

			//Send out a Raycast
			RaycastBool = Physics.Raycast (MainCamera.transform.position, MainCamera.transform.forward, out StarHit);

			//Check if the Raycast is true
			if (RaycastBool == true) {

				Debug.Log ("The user is hovering over an object.");

				//Gather Variables
				string[] classifiedstars = PlayerModel.GetComponent<MouseClick> ().classifiedStars;
				string nameOfObjectHit = StarHit.collider.name.ToString ();

				//Check if the star is classified
				for (int i = 0; i < 20; i++) {
					if (nameOfObjectHit == classifiedstars [i]) {
						//Star is classified
						Debug.Log ("Classified Star.");
						HoverCanvasText.text = "This star has already been classified!";
						HoverCanvas.enabled = true;
						break;
					} else {
						//Star isn't classified
						if ((StarHit.collider.tag == "Star") || (StarHit.collider.tag == "StarPrefab")) {
							Debug.Log ("Unclassified Star.");
							HoverCanvas.enabled = true;
							HoverCanvasText.text = "Unknown Star";
						} else {
							HoverCanvas.enabled = true;
							HoverCanvasText.text = "";
						}
					}
				}
			} else {
				HoverCanvas.enabled = false;
			}
		} else {
			HoverCanvas.enabled = false;
		}
	}
}
