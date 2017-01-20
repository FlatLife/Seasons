using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public Backpack backpack;
	private bool canTouch = false;
	private Collider2D objectColliderID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		OnCollisionUpdate();
	}

	private void HandleMovement() {
		float translation = speed * Time.deltaTime;
		transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0));
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Item") {
			objectColliderID = other;
			canTouch = true;
		}		
	}

	private void OnCollisionUpdate() {
		if (canTouch) {
			if (Input.GetKeyDown(KeyCode.E)) {
				backpack.AddItem(objectColliderID.GetComponent<Item>());
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		canTouch = false;
	}
}
