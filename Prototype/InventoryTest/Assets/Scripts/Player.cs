﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float speed;
	public Backpack backpack;
    public CraftTable craft;
	private bool canTouch = false;
    bool atFire = false;
    private Collider2D objectColliderID;
    Fire fire;
    public Slot slot;
    public CookingUI cookingUI;
    
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		OnCollisionUpdate();
	}

	private void HandleMovement() {
		float translation = speed * Time.deltaTime;
		transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0));
        if(Input.GetKeyDown(KeyCode.B)) {
            craft.Slot1.GetComponent<Image>().enabled = !craft.Slot1.GetComponent<Image>().enabled;
            craft.Slot2.GetComponent<Image>().enabled = !craft.Slot2.GetComponent<Image>().enabled;
            craft.Slot3.GetComponent<Image>().enabled = !craft.Slot3.GetComponent<Image>().enabled;
            foreach(GameObject slot in backpack.allSlots) {
                slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;
                slot.GetComponentInChildren<Text>().enabled = !slot.GetComponentInChildren<Text>().enabled;
            }
            backpack.GetComponent<Image>().enabled = !backpack.GetComponent<Image>().enabled;
            craft.GetComponent<Image>().enabled = !craft.GetComponent<Image>().enabled;
            craft.enabled = !craft.enabled;
            slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;
            GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled = !GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled;
            GameObject.Find("Canvas/CraftTableTest/PlusSymbol").SetActive(!GameObject.Find("Canvas/CraftTableTest/PlusSymbol").activeInHierarchy); 
            GameObject.Find("Canvas/CraftTableTest/EqualsSymbol").SetActive(!GameObject.Find("Canvas/CraftTableTest/EqualsSymbol").activeInHierarchy); 
            GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").SetActive(!GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").activeInHierarchy); 
       }
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
                GameObject.Find("Canvas/CookingUI").GetComponent<Image>().enabled = !GameObject.Find("Canvas/CookingUI").GetComponent<Image>().enabled;
                cookingUI.Slot4.GetComponent<Image>().enabled = !cookingUI.Slot4.GetComponent<Image>().enabled;
                cookingUI.Slot5.GetComponent<Image>().enabled = !cookingUI.Slot5.GetComponent<Image>().enabled;
                cookingUI.Slot6.GetComponent<Image>().enabled = !cookingUI.Slot6.GetComponent<Image>().enabled;
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
