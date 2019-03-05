using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour {

	public float GammaCorrection;
	public Slider Brightness;

	public void Update() {
		RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1.0f);
	}

	public void ChangeBrightness (float BrightnessControl) {

		int Value = (int)(BrightnessControl * 255f);
		//GammaCorrection = new Color(Value, Value, Value);
	}
}