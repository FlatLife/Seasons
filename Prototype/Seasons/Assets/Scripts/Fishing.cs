using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour {

	public GameObject backpack;
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
			Backpack inv = backpack.GetComponent<Backpack> ();
			Item item = (Resources.Load("rawFish") as GameObject).GetComponent<Item>();
			inv.AddItem(item);
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