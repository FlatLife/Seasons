using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {

	public bool colliding;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag != "Build"){
			colliding = true;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag != "Build"){
			colliding = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag != "Build"){
			colliding = false;
		}
	}
}
