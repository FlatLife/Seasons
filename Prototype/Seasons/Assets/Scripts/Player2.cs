﻿using UnityEngine;

public class Player2 : MonoBehaviour {
	public float speed;
	private bool onBeach;

	// Update is called once per frame
	void Update () {
	  	float horizontalInput = Input.GetAxis("Horizontal");
	  	float verticalInput = Input.GetAxis("Vertical");
		// Move the player object
		if (onBeach) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed + horizontalInput,0);
		} else {
			this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed,0);
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y - 0.3f);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Beach") {
			onBeach = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Beach") {
			onBeach = false;
		}
	}
}