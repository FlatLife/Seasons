using UnityEngine;

public class Player : MonoBehaviour {
	public float speed;

	// Update is called once per frame
	void Update () {
	  	float horizontalInput = Input.GetAxis("Horizontal");
	  	float verticalInput = Input.GetAxis("Vertical");
		// Move the player object
		this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed,0);
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
	}
}