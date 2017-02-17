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

	void OnTriggerStay2D(Collider2D other){
			colliding = true;
			objectColliderID = other;
	}
		

	void OnTriggerExit2D(Collider2D other){
			if(other == objectColliderID){
				colliding = false;
				objectColliderID = null;
			}
		}
		
	}

