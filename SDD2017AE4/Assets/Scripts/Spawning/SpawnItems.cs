using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnItems : MonoBehaviour {

	//Spawns
	public Transform[] SpawnPoints;
	public float SpawnTime = 0.01f;

	//Clones
	public GameObject stars;
	public GameObject newClone;
	public StarInfo other;

	//UI
	public Canvas CanvasRaycast;
	public Text CanvasRaycastText;

	//Colliders
	public Collider sphereCollider;

	//Indexing and Flags
	public bool starscloned = false;
	public bool spawninginitiated = false;
	public int spawnIndex = 0;

	// Use this for initialization
	public void Start () 
	{
		Invoke ("SpawnStars", SpawnTime);
	}
		
	public void SpawnStars()
	{
		spawninginitiated = true;
		Vector3 randomPosition;

		starscloned = false;
		spawnIndex = 0;


		while (starscloned == false) 
		{
			//The Vector called randomPosition is randomly assigned X, Y and Z co-ordinates by the "Random.Range" function.
			randomPosition = new Vector3(Random.Range(-400f, 400f), Random.Range(300f, 500f), Random.Range(-400f, 400f));

			// The randomPosition is maximised and scaled appropriate to the scene.
			randomPosition = transform.TransformPoint(randomPosition * 2.5f);

			//Instantiate generates a clone of an object. The function is divided into 3 components;
			//Object, Position, Roation
			newClone = Instantiate (stars, randomPosition, SpawnPoints [spawnIndex].rotation);

			//Giving the Instantiated clone it's name and tag.
			newClone.name = ("StarClone" + spawnIndex);
			newClone.tag = "Star";

		
			//Collider Settings
			sphereCollider = newClone.GetComponent<SphereCollider>();
			sphereCollider.enabled = true;
			Destroy(sphereCollider);
			newClone.AddComponent<SphereCollider> ();
			newClone.GetComponent<SphereCollider> ().enabled = true;
			newClone.GetComponent<SphereCollider> ().center = Vector3.zero;
			newClone.GetComponent<SphereCollider> ().radius = 1f;
		
			spawnIndex = spawnIndex + 1;
			newClone.GetComponent<StarInfo> ().Start ();

			//Checks if 20 stars have been instantiated
			if (spawnIndex == 20) {
				starscloned = true;
				//CanvasRaycastText.text = "";
				//CanvasRaycast.enabled = false;
			} else {
				//other.Classify ();
			}
		}
	}
}