using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {

	public bool colliding;
	private Collider2D objectColliderID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag != "Build"){
			colliding = true;
			objectColliderID = other;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag != "Build"){
			colliding = true;
			objectColliderID = other;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag != "Build" && other == objectColliderID){
			colliding = false;
		}
	}
}
