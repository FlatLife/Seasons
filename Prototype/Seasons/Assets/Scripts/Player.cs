using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float speed;

	//animation
	int frameIndex;
	public Sprite standingSprite;
	public Transform torso;
	public Transform legs;
	public float animationSpeed;
	public float fishIdleSpeed;
    public Sprite[] animSprites;
	private Sprite[] fishingSprites;
	private bool fishIdleDirection = false;
    SpriteRenderer animRenderer;
	float timeSinceLastFrame; 

	public GameObject underwater;
	public GameObject full;
	public bool playingFireStart = false;
	public bool playingCastRod = false;
	private bool catchingFish = false;

	public Canvas canvas;
	public Backpack backpack;
    public CraftTable craft;
	public CookingUI cook;
	public BarrelUI barrel;
	public BarrelUI barrelBarRef;
	public DestroyUI destroy;
	public bool canTouch = false;
	public bool switchSwimMode = false;
    public Collider2D itemColliderID; 
	private Collider2D farmColliderID;
	public Collider2D fireColliderID;
	private Collider2D waterColliderID;
	private Collider2D barrelColliderID;
    Fire fire;
	public int buttonSmash = 0;
	float buttonPressed;
    public Slot slot;

    public CookingUI cookingUI;
	public FarmingUI farmingUI;
	public WaterPurifierUI waterUI;
	public bool isSwimming = false;
	public bool isUnderwater = false;
	public bool diving = false;
	public bool canFish = false;
	public bool atFire = false;
	public bool atFarm = false;
	public bool atUse = false;
	public bool atFish = false;

	public bool atBarrel = false;
	public bool fireUIOpen = false;
	public bool farmUIOpen = false;
	public bool waterUIOpen = false;
	public bool barrelUIOpen = false;
	public bool atWaterPurifier = false;
	public bool performingAction = false;
	public float timeToCatch = 2.0f;

	public bool openUI = false;
	public bool openUIFlag = false;
	public bool atUIFlag = false;

	private Stat health;
	private Stat hunger;
	private Stat thirst;
	private Stat warmth;
	private Notification notification;

	private int frameSkip = 1;
	private bool reelingSprite = false;
	public float reelingAnimationSpeed;
	
	private void Awake(){
		health = new Stat();
		hunger = new Stat();
		thirst = new Stat();
		warmth = new Stat();
		health.bar = GameObject.Find("Canvas/Stats/Health").GetComponent<BarScript>();
		hunger.bar = GameObject.Find("Canvas/Stats/Hunger").GetComponent<BarScript>();
		thirst.bar = GameObject.Find("Canvas/Stats/Thirst").GetComponent<BarScript>();
		warmth.bar = GameObject.Find("Canvas/Stats/Warmth").GetComponent<BarScript>();
		health.currentVal = 100;
		hunger.currentVal = 100;
		thirst.currentVal = 100;
		warmth.currentVal = 100;
		health.Initialize();
		hunger.Initialize();
		thirst.Initialize();
		warmth.Initialize();
		notification = GameObject.Find("Notification").GetComponent<Notification>();
	}
    // Use this for initialization
    void Start () {
		animRenderer = GetComponent<Renderer>() as SpriteRenderer;
		frameIndex = 0;
		timeSinceLastFrame = 0;
		fishingSprites = Resources.LoadAll<Sprite>("fishing");
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0)return;
		if(openUI && Input.GetKeyDown(KeyCode.Escape)){
			ToggleUI();
		}
		//animation for starting fire
		if(playingFireStart){
			if(frameIndex < 12){				
				if(timeSinceLastFrame > animationSpeed){
				animRenderer.sprite = animSprites[frameIndex];
				timeSinceLastFrame = 0;
				frameIndex++;
				} else{
					timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
				}	
			} else {
				fire = fireColliderID.gameObject.GetComponent<Fire>();
				fire.fireState = 0;
			}
		}


		Fishing fish = GetComponent<Fishing> ();
		//animation for casting rod
		if(playingCastRod){
			if(frameIndex < 35){
				if(timeSinceLastFrame > animationSpeed){
					animRenderer.sprite = fishingSprites[frameIndex];
					timeSinceLastFrame = 0;
					frameSkip = frameSkip == 1 ? 2 : 1;
					frameIndex += frameSkip;
				} else{
					timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
				}	
			} else {
				playingCastRod = false;
				frameIndex = 35;
				timeSinceLastFrame = 0;
			}
		} else if (fish.isFishing && !fish.minigame && !catchingFish) {
			if (timeSinceLastFrame > fishIdleSpeed) {
				animRenderer.sprite = fishingSprites[frameIndex];
				fishIdleDirection = frameIndex >= 40 || frameIndex <= 34 ? !fishIdleDirection : fishIdleDirection; 
				frameIndex = fishIdleDirection ? frameIndex-1 : frameIndex+1;
				timeSinceLastFrame = 0;
			} else {
				timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
			}
		}

		if (catchingFish) {
			if(frameIndex > 0){
				if(timeSinceLastFrame > animationSpeed){
					animRenderer.sprite = fishingSprites[frameIndex];
					timeSinceLastFrame = 0;
					frameSkip = frameSkip == 1 ? 2 : 1;
					frameIndex -= frameSkip;
				} else{
					timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
				}	
			} else {
				fish.hasCaught = true;
				catchingFish = false;
				performingAction = false;
			}
		}

		if (fish.minigame && !catchingFish) {
			if(timeSinceLastFrame > reelingAnimationSpeed){
				timeSinceLastFrame = 0;
				frameIndex = frameIndex >= 30 ? frameIndex-1 : frameIndex = frameIndex <= 29 ? frameIndex+1 : frameIndex;
				reelingSprite = !reelingSprite;
				animRenderer.sprite = fishingSprites[frameIndex];
			} else {
				timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
			}
			
			timeToCatch -= Time.deltaTime;
			if (timeToCatch < 0.0f) {
				frameIndex = 39;
				fishIdleDirection = true;
				fish.minigame = false;
				timeToCatch = 2.0f;
			}else if(Input.GetKeyDown (KeyCode.E)){
				timeSinceLastFrame = 0;
				frameIndex = 35;
				catchingFish = true;
			}
		}
		
		OnCollisionUpdate();
		//If player is pressing the interaction key
		if (Input.GetKeyDown (KeyCode.E)) {
			//Fishing minigame interaction
			if(canFish){
				if (fish.isFishing && !fish.minigame) {
					playingCastRod = false;
					performingAction = false;
					fish.stop ();
				} else if(!fish.isFishing){
					//setting up animation values to play
					timeToCatch = 2.0f;
					if(openUI){
						ToggleUI();
					}
					fish.fish ();
					if(fish.isFishing){
						performingAction = true;
						playingCastRod = true;
						timeSinceLastFrame = 0;
						frameIndex = 0;
					}
				}
			}
		
			
			//Cooking minigame interaction
			if(atFire && !atUIFlag){
				atUIFlag = true;
				fire = fireColliderID.gameObject.GetComponent<Fire>();
				switch (fire.fireState){
					//if player hasnt begun to start the fire
					case -1:
						if(!playingFireStart){
							frameIndex = 1;
							timeSinceLastFrame = 0;
							playingFireStart = true;
                            //getting the position that the player should be at
                            Vector3 pos = fire.transform.position;
                            pos.y += 0.30f;
                            pos.x -= 1f;
                            transform.position = pos;
						}
						break;
					//if player is starting the fire
					case 0:
						//choosing the right animation frame
						if(frameIndex == 13){
							animRenderer.sprite = animSprites[frameIndex];
							frameIndex--;
						} else {
							animRenderer.sprite = animSprites[frameIndex];
							frameIndex++;
						}
						buttonPressed = Time.deltaTime;
						if(buttonPressed < 0.4){
							buttonSmash++;
						} else {
							buttonSmash--;
						}	
						//if they have pressed the button fast enough, enough times
						if(buttonSmash == 10){
							fire.startFire();
							playingFireStart = false;
							animRenderer.sprite = animSprites[0];
							buttonSmash = 0;
						}
						break;
					//if the fire is started
					default:
						ToggleUI();
						//ToggleCookingUI();
						break;
				}
			}

			if (atFarm && !atUIFlag) {
				atUIFlag = true;
				if(openUI){
					if(!farmUIOpen){
						ToggleFarmUI();
					}
				}
				ToggleUI();
					//ToggleFarmUI();
				
			}

			if (atWaterPurifier && !atUIFlag) {
				atUIFlag = true;
				//Debug.Log("Player entered Farm zone and pressed e");
				if(openUI){
					if(!waterUIOpen){
						ToggleWaterUI();
					}
				}
				ToggleUI();
				//ToggleWaterUI();
				
			}

			if (atBarrel && !atUIFlag) {
				atUIFlag = true;
				//Debug.Log("Player entered Farm zone and pressed e");
				if(openUI){
					if(!barrelUIOpen){
						ToggleBarrelUI();
					}
				}
				ToggleUI();
				
			}

			atUIFlag = false;

			if(switchSwimMode){
				isSwimming = !isSwimming;
				Vector3 pos = transform.position;
				if(isSwimming){
					Camera.main.GetComponent<BoxCollider2D>().enabled = true;
					underwater.GetComponent<BoxCollider2D> ().enabled = true;
					animRenderer.sortingLayerName = "Background";
					animRenderer.sortingOrder = -2;
					animRenderer.flipX = false;
					animRenderer.sprite = Resources.Load<Sprite>("Head");
					torso.GetComponent<SpriteRenderer>().enabled = true;
					torso.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
					torso.GetComponent<SpriteRenderer>().sortingOrder = -3;
					legs.GetComponent<SpriteRenderer>().enabled = true;
					legs.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
					legs.GetComponent<SpriteRenderer>().sortingOrder = -4;
					transform.GetComponent<PolygonCollider2D>().offset = new Vector2(0, 0.8f);
					transform.position = new Vector3(-23.15025f,-2.443089f, pos.z);
				}else{
					Camera.main.GetComponent<BoxCollider2D>().enabled = false;
					underwater.GetComponent<BoxCollider2D> ().enabled = false;
					animRenderer.sprite = standingSprite;
					animRenderer.sortingLayerName = "Default";
					animRenderer.sortingOrder = 0;
					transform.rotation = new Quaternion();
					torso.GetComponent<SpriteRenderer>().enabled = false;
					legs.GetComponent<SpriteRenderer>().enabled = false;
					transform.GetComponent<PolygonCollider2D>().offset = new Vector2(0, -0.3f);
					transform.position = new Vector3(-19.81603f, -0.6278321f, pos.z);
				}
			}
		}


		if(Input.GetKeyDown(KeyCode.Q)) {
			if(openUI){
				if(!waterUIOpen && atWaterPurifier){
					ToggleWaterUI();
				}
				if(!fireUIOpen && atFire){
					ToggleCookingUI();
				}
				if(!farmUIOpen && atFarm){
					ToggleFarmUI();
				}
				if(!barrelUIOpen && atBarrel) {
					ToggleBarrelUI();
				}
			}
			ToggleUI();
       }
	}

	public void ToggleUI() {
		if(!performingAction && Time.timeScale != 0){
			openUI = !openUI;
			craft.Slot1.GetComponent<Image>().enabled = !craft.Slot1.GetComponent<Image>().enabled;
			craft.Slot2.GetComponent<Image>().enabled = !craft.Slot2.GetComponent<Image>().enabled;
			craft.info.GetComponent<Image>().enabled = !craft.info.GetComponent<Image>().enabled;
			craft.Slot1.GetComponentInChildren<Text>().enabled = !craft.Slot1.GetComponentInChildren<Text>().enabled;
			craft.Slot2.GetComponentInChildren<Text>().enabled = !craft.Slot2.GetComponentInChildren<Text>().enabled;
			destroy.destroySlot.GetComponent<Image>().enabled = !destroy.destroySlot.GetComponent<Image>().enabled;
				
				foreach(GameObject slot in backpack.allSlots) {
					slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;
					slot.GetComponentInChildren<Text>().enabled = !slot.GetComponentInChildren<Text>().enabled;
				}

			backpack.GetComponent<Image>().enabled = !backpack.GetComponent<Image>().enabled;
			backpack.GetComponentInChildren<Text>().enabled = !backpack.GetComponentInChildren<Text>().enabled;
			backpack.info.GetComponent<Image>().enabled = !backpack.info.GetComponent<Image>().enabled;

			craft.GetComponent<Image>().enabled = !craft.GetComponent<Image>().enabled;
			//Text[] texts = craft.GetComponentsInChildren<Text> ();
			//foreach(Text text in texts) {
				//text.enabled = !text.enabled;
			//}
			GameObject.Find("Canvas/CraftTableTest/Text").GetComponent<Text>().enabled = !GameObject.Find("Canvas/CraftTableTest/Text").GetComponent<Text>().enabled;
			craft.enabled = !craft.enabled;

			destroy.info.GetComponent<Image>().enabled = !destroy.info.GetComponent<Image>().enabled;
			destroy.GetComponent<Image>().enabled = !destroy.GetComponent<Image>().enabled;
			destroy.destroySlot.GetComponentInChildren<Text>().enabled = !destroy.destroySlot.GetComponentInChildren<Text>().enabled;
			destroy.enabled = !destroy.enabled;

			//dont know what this line does was giving me an error. disabled it and everything seems to run fine
				//slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;

			GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled = !GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled;
			GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").SetActive(!GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").activeInHierarchy); 

			GameObject.Find("Canvas/DestroyUI/DestroyButton").GetComponent<Image>().enabled = !GameObject.Find("Canvas/DestroyUI/DestroyButton").GetComponent<Image>().enabled;
			GameObject.Find("Canvas/DestroyUI/DestroyButton/Text").SetActive(!GameObject.Find("Canvas/DestroyUI/DestroyButton/Text").activeInHierarchy); 

			if(atFire && !openUIFlag){
				ToggleCookingUI();
				openUIFlag = true;

			}else if(atFarm && !openUIFlag){
				ToggleFarmUI();
				openUIFlag = true;
			}else if(atWaterPurifier && !openUIFlag){
				ToggleWaterUI();
				openUIFlag = true;
			}else if (atBarrel && !openUIFlag) {
				ToggleBarrelUI();
				openUIFlag = true;
			}
			openUIFlag = false;
		}
	}

	private void OnCollisionUpdate() {
		bool pickedUp = false;
		//If near an item on the ground pick it up first
		if (canTouch) {
			if (Input.GetKeyDown(KeyCode.E)) {
				if (itemColliderID.gameObject.GetComponent<Item> ().type == ItemType.BOTTLE) {
					GameObject scroll = Instantiate (Resources.Load<GameObject> (itemColliderID.gameObject.GetComponent<Item> ().message), canvas.transform);
					scroll.transform.localPosition = new Vector3 (0, 0);
					string note = itemColliderID.gameObject.GetComponent<Item> ().message;
					itemColliderID.gameObject.GetComponent<Item> ().itemName = note;
				}
				pickedUp = backpack.AddItem (itemColliderID.gameObject.GetComponent<Item> ());
				if (pickedUp) {
					Destroy (itemColliderID.gameObject);
					itemColliderID = null;
					atUse = false;
					canTouch = false;
				} else {
					notification.InventoryFlag = true;
				}
			}
		}
	}

	public void ToggleJournalUI() {
		openUI = !openUI;
		
	}

	private void OnTriggerStay2D(Collider2D other){
        //Collision with item on the ground
		if (other.tag == "Item") {
			itemColliderID = other;
			atUse = true;
			canTouch = true;
		}
		if (other.tag == "Fire") {
			fireColliderID = other;
			//canTouch = false;
			atUse = true;
			atFire = true;
		}
		if (other.tag == "Farm") {
			farmColliderID = other;
			atUse = true;
			atFarm = true;
		}
		if (other.tag == "WaterPurifier") {
			waterColliderID = other;
			atUse = true;
			atWaterPurifier = true;
		}
		if (other.tag == "Barrel") {
			barrelColliderID = other;
			atUse = true;
			atBarrel = true;
		}
    }

	private void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Item" && other == itemColliderID)
        {
			itemColliderID = null;
			atUse = false;
            canTouch = false;
        }
        if (other.tag == "Fire" && other == fireColliderID)
        {
			atUse = false;
            atFire = false;
			if (fireUIOpen) {
				ToggleCookingUI ();
			}
        }
		if (other.tag == "Farm" && other == farmColliderID) {
			atUse = false;
			atFarm = false;
			if (farmUIOpen) {
				ToggleFarmUI ();
			}
		}
		if (other.tag == "WaterPurifier" && other == waterColliderID) {
			atUse = false;
			atWaterPurifier = false;
			if (waterUIOpen) {
				ToggleWaterUI ();
			}
		}

		if (other.tag == "Barrel" && other == barrelColliderID) {
			atUse = false;
			atBarrel = false;
			if (barrelUIOpen) {
				ToggleBarrelUI ();
			}
		}
    }

	private void ToggleWaterUI(){
		waterUIOpen = !waterUIOpen;
		waterUI = waterColliderID.gameObject.GetComponent<WaterPurifier>().WaterUI;
		foreach (Transform waterSlot in waterUI.transform) {
			if (waterSlot.GetComponent<Image> () != null) {
				waterSlot.GetComponent<Image> ().enabled = !waterSlot.GetComponent<Image> ().enabled;
			} else {
				waterSlot.GetComponent<Text> ().enabled = !waterSlot.GetComponent<Text> ().enabled;
			}
		}
		waterUI.GetComponent<Image>().enabled = !waterUI.GetComponent<Image>().enabled;
	}

	private void ToggleFarmUI(){
		farmUIOpen = !farmUIOpen;
		farmingUI = farmColliderID.gameObject.GetComponent<Farming>().farmingUI;
		foreach (Transform farmSlot in farmingUI.transform) {
			if (farmSlot.GetComponent<Image> () != null) {
				farmSlot.GetComponent<Image> ().enabled = !farmSlot.GetComponent<Image> ().enabled;
			} else {
				farmSlot.GetComponent<Text> ().enabled = !farmSlot.GetComponent<Text> ().enabled;
			}
		}
		farmingUI.GetComponent<Image>().enabled = !farmingUI.GetComponent<Image>().enabled;
	}

	private void ToggleCookingUI(){
		fireUIOpen = !fireUIOpen;
		cook = fireColliderID.gameObject.GetComponent<Fire>().cookingUI;
		foreach (Transform cookSlot in cook.transform) {
			if (cookSlot.GetComponent<Image> () != null) {
				cookSlot.GetComponent<Image> ().enabled = !cookSlot.GetComponent<Image> ().enabled;
			} else {
				cookSlot.GetComponent<Text> ().enabled = !cookSlot.GetComponent<Text> ().enabled;
			}
		}
		cook.GetComponent<Image>().enabled = !cook.GetComponent<Image>().enabled;
	}

	private void ToggleBarrelUI(){
		barrelUIOpen = !barrelUIOpen;
		barrel = barrelColliderID.gameObject.GetComponent<Barrel>().BarrelUI;
		foreach (Transform barrelSlot in barrel.transform) {
			if (barrelSlot.GetComponent<Image> () != null) {
				barrelSlot.GetComponent<Image> ().enabled = !barrelSlot.GetComponent<Image> ().enabled;
			} else {
				barrelSlot.GetComponent<Text> ().enabled = !barrelSlot.GetComponent<Text> ().enabled;
			}
		}
		barrel.GetComponent<Image>().enabled = !barrel.GetComponent<Image>().enabled;
		barrel.WaterMask.GetComponent<Image>().enabled = !barrel.WaterMask.GetComponent<Image>().enabled;
		barrel.WaterMask.SetActive(!barrel.WaterMask.activeSelf);
	}
}
