using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorCanvasScript : MonoBehaviour {

	public Canvas ErrorCanvas;
	public Button ResumeButton;


	public void Start(){
		ErrorCanvas.enabled = false;
	}

	public void ResumeClick(){
		ErrorCanvas.enabled = false;
		Time.timeScale = 1;
	}

}
