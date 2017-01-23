using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour {

	public float fishChance = 0.002f;
	public Transform fishPrefab;
	public bool isFishing = false;
	public bool minigame = false;
	public bool hasCaught = false;
	public Transform alertP;
	Transform alert;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFishing && !minigame) {
			if(Random.Range(0f,1f) < fishChance  && Time.timeScale != 0){ 
				alert = Instantiate(alertP);
				minigame = true;
				Vector3 position = transform.position;
				position.y += 2;
				alert.position = position;
				Destroy (alert.gameObject, 2.0f);
			}
		}

		if (minigame && hasCaught) {
			Transform fish = Instantiate(fishPrefab);
			Vector3 position = transform.position;
			position.x += 3;
			fish.position =	position;
			Destroy (alert.gameObject);
			stop ();
		}
	}

	public void fish(){
		isFishing = true;
	}	

	public void stop(){
		isFishing = false;
		minigame = false;
		hasCaught = false;
	}
}