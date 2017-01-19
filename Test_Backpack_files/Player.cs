using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public Backpack backpack;
	private bool canTouch = false;

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
			canTouch = true;
		}		
	}

	private void OnCollisionUpdate() {
		if (canTouch) {
			Collider2D objectCollider = GameObject.FindGameObjectWithTag("Item").GetComponent<Collider2D>();
			if (Input.GetKeyDown(KeyCode.E)) {
				backpack.AddItem(objectCollider.GetComponent<Item>());
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		canTouch = false;
	}
}
