using UnityEngine;

public class Player2 : MonoBehaviour {
	public float speed;

	public float maxSpeed;
	public float waterSpeed;
	public float landSpeed;
	private bool onBeach;


	// Update is called once per frame
	void Update () {
	  	float horizontalInput = Input.GetAxis("Horizontal");
	  	float verticalInput = Input.GetAxis("Vertical");
		Player player = transform.GetComponent<Player>();
		bool swimming = this.GetComponent<Player>().isSwimming;
		if(swimming){
			speed = waterSpeed;
			this.GetComponent<Rigidbody2D>().gravityScale = 1;
			this.GetComponent<BoxCollider2D>().enabled = true;
		}else{
			speed = landSpeed;
			this.GetComponent<BoxCollider2D>().enabled = false;
		}
		// Move the player object
		if(!swimming){
			if(!player.openUI && !player.playingFireStart){
				if (onBeach) {
					this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed + horizontalInput,0);
				} else {
					this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed,0);
				}
			}else{
				this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f);
			}
		}else if(swimming){
			if(!player.openUI && swimming && this.GetComponent<Rigidbody2D>().velocity.x <= maxSpeed 
			&& this.GetComponent<Rigidbody2D>().velocity.y <= maxSpeed){
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalInput * speed,verticalInput * speed));
			}else{
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, this.GetComponent<Rigidbody2D>().velocity.y));
			}
		}
		if(!swimming){
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y - 0.3f);
		}
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