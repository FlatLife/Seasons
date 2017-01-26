﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

public float speed;

	public Canvas canvas;
	public Backpack backpack;
    public CraftTable craft;

	public DestroyUI destroy;
	private bool canTouch = false;
    private Collider2D objectColliderID;
    Fire fire;
    public Slot slot;
    public CookingUI cookingUI;

	public bool isSwimming = false;
	public bool isUnderwater = false;
	public bool diving = false;
	public bool canFish = false;
	public bool atFire = false;
	public bool performingAction = false;
	public float timeToCatch = 2.0f;

	private bool openUI = false;
	private Rigidbody2D rb; 

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
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
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
		HandleMovement();
		OnCollisionUpdate();
		Fishing fish = GetComponent<Fishing> ();
		float movementInputH = Input.GetAxis ("Horizontal");
		float movementInputV = Input.GetAxis ("Vertical");

		//If player is pressing the interaction key
		if (Input.GetKeyDown (KeyCode.E) && !fish.isFishing) {
			//Fishing minigame interaction
			if(canFish){
				if (fish.isFishing) {
				performingAction = false;
				fish.stop ();
			} else {
				performingAction = true;
				timeToCatch = 2.0f;
				fish.fish ();
				}
			}
			//Cooking minigame interaction
			if(atFire){
				GameObject.Find("Canvas/CookingUI").GetComponent<Image>().enabled = !GameObject.Find("Canvas/CookingUI").GetComponent<Image>().enabled;
                cookingUI.Slot4.GetComponent<Image>().enabled = !cookingUI.Slot4.GetComponent<Image>().enabled;
                cookingUI.Slot5.GetComponent<Image>().enabled = !cookingUI.Slot5.GetComponent<Image>().enabled;
                cookingUI.Slot6.GetComponent<Image>().enabled = !cookingUI.Slot6.GetComponent<Image>().enabled;
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

		if(isSwimming && Input.GetKeyDown(KeyCode.S)){
			diving = true;
		}

		if(Input.GetKeyDown(KeyCode.B)) {
			ToggleUI();
       }
	}
    

	private void HandleMovement() {
		if(!openUI && !performingAction) {
			float movementInputH = Input.GetAxis ("Horizontal");
			float movementInputV = Input.GetAxis ("Vertical");
			if(!isSwimming){
				rb.velocity = new Vector3(movementInputH * speed, rb.velocity.y);
			}else if (isSwimming){
				rb.velocity = new Vector3(movementInputH * speed, rb.velocity.y);
				if(diving){
					Vector3 dive = new Vector3(0.0f, movementInputV);
					rb.velocity = dive * speed;
				}
			}
			if(isUnderwater){
				rb.velocity = new Vector3(movementInputH * 2.0f, movementInputV*2.0f + 1.0f);
			}
		}
	}

	private void ToggleUI() {
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

        slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;

        GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled = !GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled;
        GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").SetActive(!GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").activeInHierarchy); 

		GameObject.Find("Canvas/DestroyUI/DestroyButton").GetComponent<Image>().enabled = !GameObject.Find("Canvas/DestroyUI/DestroyButton").GetComponent<Image>().enabled;
        GameObject.Find("Canvas/DestroyUI/DestroyButton/Text").SetActive(!GameObject.Find("Canvas/DestroyUI/DestroyButton/Text").activeInHierarchy); 
	}

	private void OnTriggerEnter2D(Collider2D other){
        //Collision with item on the ground
		if (other.tag == "Item") {
			objectColliderID = other;
			canTouch = true;
		}
    }

	private void OnCollisionUpdate() {
        //If near an item on the ground pick it up first
		if (canTouch) {
			if (Input.GetKeyDown(KeyCode.E)) {
				backpack.AddItem(objectColliderID.GetComponent<Item>());
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Item" && other == objectColliderID)
        {
            canTouch = false;
        }
        if (other.tag == "Fire")
        {
            atFire = false;
        }
    }
}
