﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float speed;

	//animation
	int frameIndex;
	public float animationSpeed;
    public Sprite[] animSprites;
    SpriteRenderer animRenderer;
	float timeSinceLastFrame; 
	public bool playingFireStart = false;
	public bool playingCastRod = false;

	public Canvas canvas;
	public Backpack backpack;
    public CraftTable craft;
	public CookingUI cook;
	public DestroyUI destroy;
	private bool canTouch = false;
	public bool switchSwimMode = false;
    private Collider2D itemColliderID;
	private Collider2D farmColliderID;
	private Collider2D fireColliderID;
	private Collider2D waterColliderID;
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
	public bool fireUIOpen = false;
	public bool farmUIOpen = false;
	public bool waterUIOpen = false;
	public bool atWaterPurifier = false;
	public bool performingAction = false;
	public float timeToCatch = 2.0f;

	public bool openUI = false;

	private Stat health;
	private Stat hunger;
	private Stat thirst;
	private Stat warmth;

	
	private void Awake(){
		health = new Stat();
		hunger = new Stat();
		thirst = new Stat();
		warmth = new Stat();
		health.bar = GameObject.Find("Canvas/HealthBar").GetComponent<BarScript>();
		hunger.bar = GameObject.Find("Canvas/HungerBar").GetComponent<BarScript>();
		thirst.bar = GameObject.Find("Canvas/ThirstBar").GetComponent<BarScript>();
		warmth.bar = GameObject.Find("Canvas/WarmthBar").GetComponent<BarScript>();
		health.currentVal = 100;
		hunger.currentVal = 100;
		thirst.currentVal = 100;
		warmth.currentVal = 100;
		health.Initialize();
		hunger.Initialize();
		thirst.Initialize();
		warmth.Initialize();
		
	}
    // Use this for initialization
    void Start () {
		animRenderer = GetComponent<Renderer>() as SpriteRenderer;
		frameIndex = 0;
		timeSinceLastFrame = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//animation for starting fire
		if(playingFireStart){
			if(frameIndex < 13){				
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
		//animation for casting rod
		if(playingCastRod){
			if(frameIndex < 34){
				if(timeSinceLastFrame > animationSpeed){
				animRenderer.sprite = animSprites[frameIndex];
				timeSinceLastFrame = 0;
				frameIndex++;
				} else{
					timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
				}	
			} else {
				playingCastRod = false;
				animRenderer.sprite = animSprites[0];
			}
		}

		//keys to test bars
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			health.CurrentVal-=10;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			hunger.CurrentVal-=10;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			warmth.CurrentVal-=10;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			thirst.CurrentVal-=10;
		}
		OnCollisionUpdate();
		Fishing fish = GetComponent<Fishing> ();

		//If player is pressing the interaction key
		if (Input.GetKeyDown (KeyCode.E) && !fish.isFishing) {
			//Fishing minigame interaction
			if(canFish){
				if (fish.isFishing) {
				performingAction = false;
				fish.stop ();
			} else {
				//setting up animation values to play
				playingCastRod = true;
				timeSinceLastFrame = 0;
				frameIndex = 14;
				performingAction = true;
				timeToCatch = 2.0f;
				fish.fish ();
				}
			}
			
			//Cooking minigame interaction
			if(atFire){
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
                            pos.y += 0.8f;
                            pos.x -= 0.75f;
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
							if(buttonSmash > 0){
								buttonSmash--;
							}
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
						ToggleCookingUI();
						break;
				}
			}

			if (atFarm) {
				//Debug.Log("Player entered Farm zone and pressed e");
				ToggleUI();
				ToggleFarmUI();
			}

			if (atWaterPurifier) {
				//Debug.Log("Player entered Farm zone and pressed e");
				ToggleUI();
				ToggleWaterUI();
			}

			if(switchSwimMode){
				isSwimming = !isSwimming;
				Vector3 pos = transform.position;
				if(isSwimming){
					transform.position = new Vector3(-21.33625f, -0.53626f, pos.z);
				}else{
					transform.position = new Vector3(-19.33625f, 0.2520248f, pos.z);
				}
			}
		}

		if (fish.minigame) {
			timeToCatch -= Time.deltaTime;
			if (timeToCatch < 0.0f) {
				fish.minigame = false;
				timeToCatch = 2.0f;
			}else if(Input.GetKeyDown (KeyCode.Space)){
				fish.hasCaught = true;
				performingAction = false;
			}
		}

		if(Input.GetKeyDown(KeyCode.Q)) {
			ToggleUI();
       }
	}

	public void ToggleUI() {
		openUI = !openUI;
		craft.Slot1.GetComponent<Image>().enabled = !craft.Slot1.GetComponent<Image>().enabled;
        craft.Slot2.GetComponent<Image>().enabled = !craft.Slot2.GetComponent<Image>().enabled;
		craft.Slot1.GetComponentInChildren<Text>().enabled = !craft.Slot1.GetComponentInChildren<Text>().enabled;
		craft.Slot2.GetComponentInChildren<Text>().enabled = !craft.Slot2.GetComponentInChildren<Text>().enabled;
		destroy.destroySlot.GetComponent<Image>().enabled = !destroy.destroySlot.GetComponent<Image>().enabled;
            
        	foreach(GameObject slot in backpack.allSlots) {
                slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;
                slot.GetComponentInChildren<Text>().enabled = !slot.GetComponentInChildren<Text>().enabled;
            }

        backpack.GetComponent<Image>().enabled = !backpack.GetComponent<Image>().enabled;

        craft.GetComponent<Image>().enabled = !craft.GetComponent<Image>().enabled;
        craft.enabled = !craft.enabled;

		destroy.GetComponent<Image>().enabled = !destroy.GetComponent<Image>().enabled;
		destroy.destroySlot.GetComponentInChildren<Text>().enabled = !destroy.destroySlot.GetComponentInChildren<Text>().enabled;
		destroy.enabled = !destroy.enabled;

		//dont know what this line does was giving me an error. disabled it and everything seems to run fine
//        slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;

        GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled = !GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled;
        GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").SetActive(!GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").activeInHierarchy); 

		GameObject.Find("Canvas/DestroyUI/DestroyButton").GetComponent<Image>().enabled = !GameObject.Find("Canvas/DestroyUI/DestroyButton").GetComponent<Image>().enabled;
        GameObject.Find("Canvas/DestroyUI/DestroyButton/Text").SetActive(!GameObject.Find("Canvas/DestroyUI/DestroyButton/Text").activeInHierarchy); 

		if(atFire){
			ToggleCookingUI();
		}else if(atFarm){
			ToggleFarmUI();
		}else if(atWaterPurifier){
			ToggleWaterUI();
		}
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
    }

	private void OnCollisionUpdate() {
        //If near an item on the ground pick it up first
		if (canTouch) {
			if (Input.GetKeyDown(KeyCode.E)) {
				backpack.AddItem(itemColliderID.gameObject.GetComponent<Item>());
				Destroy (itemColliderID.gameObject);
				itemColliderID = null;
				atUse = false;
				canTouch = false;

			}
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
    }

	private void ToggleWaterUI(){
		waterUIOpen = !waterUIOpen;
		waterUI = waterColliderID.gameObject.GetComponent<WaterPurifier>().WaterUI;
		foreach (Transform waterSlot in waterUI.transform) {
			waterSlot.GetComponent<Image>().enabled = !waterSlot.GetComponent<Image>().enabled;
		}
		waterUI.GetComponent<Image>().enabled = !waterUI.GetComponent<Image>().enabled;
	}

	private void ToggleFarmUI(){
		farmUIOpen = !farmUIOpen;
		farmingUI = farmColliderID.gameObject.GetComponent<Farming>().farmingUI;
		foreach (Transform farmSlot in farmingUI.transform) {
			farmSlot.GetComponent<Image>().enabled = !farmSlot.GetComponent<Image>().enabled;
		}
		farmingUI.GetComponent<Image>().enabled = !farmingUI.GetComponent<Image>().enabled;
	}

	private void ToggleCookingUI(){
		fireUIOpen = !fireUIOpen;
		cook = fireColliderID.gameObject.GetComponent<Fire>().cookingUI;
		foreach (Transform cookSlot in cook.transform) {
			cookSlot.GetComponent<Image>().enabled = !cookSlot.GetComponent<Image>().enabled;
		}
		cook.GetComponent<Image>().enabled = !cook.GetComponent<Image>().enabled;
	}
}
