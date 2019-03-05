using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

	//Reference to the audio source component
	public AudioSource Audio;
	public Slider volume;


	// The sound source is referenced
	public void Start()
	{
		Audio = null;

		try{
			if (GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> () != null) {
				Audio = GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ();
			} else {
				Debug.Log ("The object: [BackgroundMusic] does not exist in this scene.");
			}
		}
		catch{
		}
		finally{
		}
	}
		
	//VolumeControl is determined by the slider and is passed in as a variable
	public void VolumeControl (float VolumeControl)
	{
		AudioListener.volume = VolumeControl;	
	}
		

	// This function enables the music
	public void musicToggle(bool musicToggle)
	{
		if (musicToggle == true) {
			Audio.mute = false;
		} else {
			Audio.mute = true;
		}
	}
}