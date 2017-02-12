﻿using UnityEngine;

public class Player2 : MonoBehaviour {
	public float speed;
	int walkingFrameIndex;
	public float animationSpeed;
	private Sprite[] walkingSprites;
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
	public StatsMaster statsRef;
	private int frameSkip = 1;

	void Start()
	{
		animRenderer = GetComponent<Renderer>() as SpriteRenderer;
		walkingSprites = Resources.LoadAll<Sprite>("PlayerWalk");
		walkingFrameIndex = 0;
	}

	// Update is called once per frame
	void Update () {
	  	float horizontalInput = Input.GetAxis("Horizontal");
	  	float verticalInput = Input.GetAxis("Vertical");
		Player player = transform.GetComponent<Player>();
		bool swimming = this.GetComponent<Player>().isSwimming;
		if(player.playingCastRod || player.playingFireStart){
			animRenderer.flipX = false;
		}
		if(swimming){
			speed = waterSpeed;
			this.GetComponent<Rigidbody2D>().gravityScale = 1;
			this.GetComponent<CapsuleCollider2D>().enabled = true;
		}else{
			speed = landSpeed;
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
			this.GetComponent<CapsuleCollider2D>().enabled = false;
		}

		// Move the player object
		if(!swimming){
			if(!player.openUI && !player.playingFireStart && !player.performingAction){
				if (onBeach) {
					this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed + horizontalInput * 2,0);
				} else {
					this.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalInput * speed,verticalInput * speed,0);
				}
				
				// Animate walking
				if (horizontalInput != 0 || verticalInput != 0 ) {
					if(timeSinceLastFrame > animationSpeed){
						animRenderer.flipX = horizontalInput < 0 ? true : horizontalInput > 0 ? false : animRenderer.flipX;
						animRenderer.sprite = walkingSprites[walkingFrameIndex];
						timeSinceLastFrame = 0;
						//frameSkip = frameSkip == 1 ? 2 : 1;
						walkingFrameIndex = (walkingFrameIndex + 1) % walkingSprites.Length;
					} else{
						timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
					}
				} else {
					animRenderer.sprite = standingSprite;
				}
			}else{
				this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f);
				if (!transform.GetComponent<Fishing>().isFishing) {
					animRenderer.sprite = standingSprite;
				}
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

         atLeftWall = true;
      } else if(other.tag == "RightWall") {

         atRightWall = true;
      } 
	  if(other.tag == "OceanFloor") {
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
         atLeftWall = false;
      } else if(other.tag == "RightWall") {

         atRightWall = false;
      }
	   if(other.tag == "OceanFloor") {

         atOceanFloor = false;
      }  
	}
}