using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {

	public bool colliding;
	private List<Collider2D> objectColliderID;
	// Use this for initialization
	void Start () {
		objectColliderID = new List<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		colliding = objectColliderID.Count != 0;
	}

	void OnTriggerEnter2D(Collider2D other){
			objectColliderID.Add(other);
	}
		

	void OnTriggerExit2D(Collider2D other){
		for(int i = objectColliderID.Count - 1; i >= 0; i--){
			if(other == objectColliderID[i]){
				objectColliderID.RemoveAt(i);
			}
		}
		
	}
}
