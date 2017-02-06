﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Player control = other.gameObject.GetComponent<Player>();
			control.switchSwimMode = true;
			control.atUse = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			Player control = other.gameObject.GetComponent<Player>();
			control.switchSwimMode = false;
			control.atUse = false;
		}
	}
}
