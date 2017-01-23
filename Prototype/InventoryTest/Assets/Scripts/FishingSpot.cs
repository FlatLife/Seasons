﻿using System.Collections;
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
		Player control = other.gameObject.GetComponent<Player>();
		control.canFish = true;

	}

	void OnTriggerExit2D(Collider2D other)
	{
		Player control = other.gameObject.GetComponent<Player>();
		control.canFish = false;
	}
}