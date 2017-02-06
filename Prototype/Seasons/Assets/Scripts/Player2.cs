using UnityEngine;

public class Player2 : MonoBehaviour {
	public float speed;
	int walkingFrameIndex;
	public float animationSpeed;
	private Sprite[] walkingSprites;
	private int skipFrames;
	public Sprite standingSprite;
    SpriteRenderer animRenderer;
	float timeSinceLastFrame; 
	public float maxSpeed;
	public float waterSpeed;
	public float landSpeed;
	private bool onBeach;
	private bool underwater;

	private bool atLeftWall;

	private bool atRightWall;
	private bool atOceanFloor;

	void Start()
	{
		animRenderer = GetComponent<Renderer>() as SpriteRenderer;
		walkingSprites = Resources.LoadAll<Sprite>("PlayerWalk");
		Debug.Log(walkingSprites.Length);
		walkingFrameIndex = 0;
		skipFrames = 1;
	}

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
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
			this.GetComponent<BoxCollider2D>().enabled = false;
		}

		// Move the player object
		if(!swimming){
			if(!player.openUI && !player.playingFireStart && !player.playingCastRod){
				if (onBeach) {
					this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed + horizontalInput,0);
				} else {
					this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed,0);
				}
				
				// Animate walking
				if (horizontalInput != 0 || verticalInput != 0 ) {
					if(timeSinceLastFrame > animationSpeed){
						animRenderer.flipX = horizontalInput < 0 ? true : horizontalInput > 0 ? false : animRenderer.flipX;
						animRenderer.sprite = walkingSprites[walkingFrameIndex];
						timeSinceLastFrame = 0;
						skipFrames = skipFrames == 1 ? 2 : 1;
						walkingFrameIndex = (walkingFrameIndex+skipFrames)%walkingSprites.Length;
					} else{
						timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
					}
				} else {
					animRenderer.sprite = standingSprite;
				}
			}else{
				this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f);
			}
		}else if(swimming){
			if(!underwater){
				if(Input.GetKey(KeyCode.S)){
					this.GetComponent<Rigidbody2D>().position = new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y - 2f);
				}
			}
			if(!player.openUI && swimming && this.GetComponent<Rigidbody2D>().velocity.x <= maxSpeed 
			&& this.GetComponent<Rigidbody2D>().velocity.y <= maxSpeed){
				if(!underwater){
					this.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalInput * speed,0));
				}else{
					this.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalInput * speed, verticalInput * speed));
				}
			}else{
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, this.GetComponent<Rigidbody2D>().velocity.y));
			}
		}
		if(atRightWall || atLeftWall || atOceanFloor){
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		}
		if(!swimming){
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y - 0.3f);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Beach") {
			onBeach = true;
		}
		if (other.tag == "Swim") {
			underwater = true;
		}

     if(other.tag == "LeftWall") {
         // If collided with the left wall, set
         // the left wall flag to true
         atLeftWall = true;
      } else if(other.tag == "RightWall") {
         // If collided with the right wall, set
         // the right wall flag to true
         atRightWall = true;
      } 
	  if(other.tag == "OceanFloor") {
         // If collided with the right wall, set
         // the right wall flag to true
         atOceanFloor = true;
      } 
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Beach") {
			onBeach = false;
		}
		if (other.tag == "Swim") {
			underwater = false;
		}
		 if(other.tag == "LeftWall") {
         // If collided with the left wall, set
         // the left wall flag to true
         atLeftWall = false;
      } else if(other.tag == "RightWall") {
         // If collided with the right wall, set
         // the right wall flag to true
         atRightWall = false;
      }
	   if(other.tag == "OceanFloor") {
         // If collided with the right wall, set
         // the right wall flag to true
         atOceanFloor = false;
      }  
	}
}