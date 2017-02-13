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


	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player"){
		Player control = other.gameObject.GetComponent<Player>();
		ItemType type = (ItemType.FISHINGROD);
		if(control.backpack.CheckItem(type)){
			control.canFish = true;
		}else{
			control.canFish = false;
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