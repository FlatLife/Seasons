﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public Backpack backpack;
	private bool canTouch = false;
    bool atFire = false;
    private Collider2D objectColliderID;
    Fire fire;

	public bool isSwimming = false;
	public bool diving = false;
	public bool canFish = false;
	public bool performingAction = false;
	public float timeToCatch = 2.0f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		OnCollisionUpdate();
		Fishing fish = GetComponent<Fishing> ();
		float movementInput = Input.GetAxis ("Horizontal");
		float movementInput2 = Input.GetAxis ("Vertical");

		if (Input.GetKeyDown (KeyCode.E) && canFish) {
			if (fish.isFishing) {
				performingAction = false;
				fish.stop ();
			} else {
				performingAction = true;
				timeToCatch = 2.0f;
				fish.fish ();
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

		if(Input.GetKey(KeyCode.S) && isSwimming && diving){
			transform.Translate(new Vector3(Time.deltaTime * speed * movementInput,Time.deltaTime * 6.0f * -1,0), Space.World);
			
		}
		if (!performingAction) {
			transform.Translate (new Vector3 (Time.deltaTime * speed * movementInput, Time.deltaTime * speed * movementInput2, 0), Space.World);
		}
	}
    

	private void HandleMovement() {
		float translation = speed * Time.deltaTime;
		transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0));
	}

	private void OnTriggerEnter2D(Collider2D other){
        //Collision with item on the ground
		if (other.tag == "Item") {
			objectColliderID = other;
			canTouch = true;
		}
        //Collision with Fire
        if (other.tag == "Fire")
        {
            GameObject target = other.gameObject;
            fire = target.GetComponent<Fire>();
            atFire = true;
        }
    }

	private void OnCollisionUpdate() {
        //If near an item on the ground pick it up first
		if (canTouch) {
			if (Input.GetKeyDown(KeyCode.E)) {
				backpack.AddItem(objectColliderID.GetComponent<Item>());
			}
        //If colliding with fire
		} else if (atFire)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (fire.cookCount < 3 && atFire == true)
                {
                    fire.startCooking();
                }
            }
        }
	}

	private void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Item")
        {
            canTouch = false;
        }
        if (other.tag == "Fire")
        {
            atFire = false;
        }
    }
}
