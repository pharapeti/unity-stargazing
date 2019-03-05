using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StarInfo : MonoBehaviour {

	//Star Properties
	[Header("Star Variables")]
	public float starID;
	public string starclass;
	public int startemp;

	//Script Variables
	[Header("Script Variables")]
	static public SpawnItems other;
	public GameObject StarController;
	private float randomintfloat;

	//Mesh Renderer
	[Header("Mesh Renderer")]
	public MeshRenderer meshrend;



	public void Start()
	{
		//Mesh Renderer Referencing
		if (gameObject.GetComponent<MeshRenderer> () == null) {
			MeshRenderer meshrend = gameObject.AddComponent<MeshRenderer> () as MeshRenderer;
		} else {
			meshrend = gameObject.GetComponent<MeshRenderer> ();
		}


		//SphereCollider Referencing
		if (gameObject.GetComponent<SphereCollider> () == null) {
			Debug.Log ("There is no SphereCollider on this Object.");
			gameObject.AddComponent<SphereCollider> ();
		}

		// Calling the Classify function
		Invoke("Classify", 0f);
	}

	public void Classify()
	{
		// Randomly generates a float between 1001 and 9999.
		starID = Random.Range (1001, 9999);

		// Initialising the star class.
		starclass = "";

		// Generating a random integer between 0 and 6 which represent the 7 possible star types.
		randomintfloat = Random.Range (0, 6);
		int randomint = (int)Mathf.Round (randomintfloat);

		// Randomly Generates a number in a range determined by common star temperatures from:
		// https://en.wikipedia.org/wiki/Stellar_classification
		switch (randomint) 
		{
		case 0:
			starclass = "O";
			// https://en.wikipedia.org/wiki/Stellar_classification
			startemp = Random.Range (30000, 50000);
			break;
		case 1:
			starclass = "B";
			startemp = Random.Range (10000, 29999);
			break;
		case 2:
			starclass = "A";
			startemp = Random.Range (7500, 9999);
			break;
		case 3:
			starclass = "F";
			startemp = Random.Range (6000, 7499);
			break;
		case 4:
			starclass = "G";
			startemp = Random.Range (5200, 5999);
			break;
		case 5:
			starclass = "K";
			startemp = Random.Range (3700, 5199);
			break;
		case 6:
			starclass = "M";
			startemp = Random.Range (2400, 3699);
			break;
		}
			


		//This line alters the material of the renderer attached to the GameObject
		switch (starclass) 
		{
		case "O":
			Material newMatO = Resources.Load("O", typeof(Material)) as Material;
			meshrend.material = newMatO;
			meshrend.enabled = true;
			break;
		case "B":
			Material newMatB = Resources.Load("B", typeof(Material)) as Material;
			meshrend.material = newMatB;
			meshrend.enabled = true;
			break;
		case "A":
			Material newMatA = Resources.Load("A", typeof(Material)) as Material;
			meshrend.material = newMatA;
			meshrend.enabled = true;
			break;
		case "F":
			Material newMatF = Resources.Load("F", typeof(Material)) as Material;
			meshrend.material = newMatF;
			meshrend.enabled = true;
			break;
		case "G":
			Material newMatG = Resources.Load("G", typeof(Material)) as Material;
			meshrend.material = newMatG;
			meshrend.enabled = true;
			break;
		case "K":
			Material newMatK = Resources.Load("K", typeof(Material)) as Material;
			meshrend.material = newMatK;
			meshrend.enabled = true;
			break;
		case "M":
			Material newMatM = Resources.Load("M", typeof(Material)) as Material;
			meshrend.material = newMatM;
			meshrend.enabled = true;
			break;
		}

		//Debug.Log ("This clone has been classified and colored.");
	}
}