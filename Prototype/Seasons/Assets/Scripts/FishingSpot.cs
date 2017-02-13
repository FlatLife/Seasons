using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player"){
		Player control = other.gameObject.GetComponent<Player>();
		Item item = (Resources.Load("FishingRod") as GameObject).GetComponent<Item>();
		if(control.backpack.CheckItem(item)){
			control.canFish = true;
		}
		control.atFish = true;
		control.atUse = true;
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player"){
			Player control = other.gameObject.GetComponent<Player>();
			control.atFish = false;
			control.canFish = false;
			control.atUse = false;
		}
	}
}