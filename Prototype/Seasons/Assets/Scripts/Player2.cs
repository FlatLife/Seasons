using UnityEngine;

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
	public Transform torso;
	public Transform legs;
	public float rotationSpeed=0.2f;
	private float oldAngle;
	private float angleDifference = 0;


	void Start()
	{
		animRenderer = GetComponent<Renderer>() as SpriteRenderer;
		walkingSprites = Resources.LoadAll<Sprite>("PlayerWalk");
		walkingFrameIndex = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
	  	float horizontalInput = Input.GetAxis("Horizontal");
	  	float verticalInput = Input.GetAxis("Vertical");
		Player player = transform.GetComponent<Player>();
		bool swimming = this.GetComponent<Player>().isSwimming;
		if(player.playingCastRod || player.playingFireStart){
			animRenderer.flipX = false;
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
						frameSkip = frameSkip == 1 ? 2 : 1;
						walkingFrameIndex = (walkingFrameIndex + frameSkip) % walkingSprites.Length;
					} else{
						timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
					}
				} else {
					animRenderer.sprite = standingSprite;
				}
			}else{
				this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f);
				if (!transform.GetComponent<Fishing>().isFishing && !player.playingFireStart) {
					animRenderer.sprite = standingSprite;
				}
			}
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y - 0.3f);
		}else if(swimming){
			if(!player.openUI){
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
			}else{
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, this.GetComponent<Rigidbody2D>().velocity.y));
			}
			if(atRightWall || atLeftWall || atOceanFloor){
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}

			float angle = (Mathf.Atan2(horizontalInput, verticalInput) * (180 / Mathf.PI)) * -1;
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 2);
			if (angle != oldAngle) {
				angleDifference += oldAngle - angle;
				angleDifference %= 160;
			}
			//angleDifference = angleDifference > 120 ? 120 : angleDifference < -45 ? -45 : angleDifference;
			angleDifference += angleDifference > 1 ? -1 : angleDifference < 1 ? 1 : 0;
			legs.localRotation = Quaternion.Slerp(legs.localRotation, Quaternion.Euler(0,0,angleDifference), Time.deltaTime);
			oldAngle = angle;

			transform.GetComponent<SpriteRenderer>().flipX = horizontalInput < 0;
			torso.GetComponent<SpriteRenderer>().flipX = horizontalInput < 0;
			legs.GetComponent<SpriteRenderer>().flipX = horizontalInput < 0;
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