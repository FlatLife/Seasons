using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

public float speed;
	public Backpack backpack;
    public CraftTable craft;
	private bool canTouch = false;
    private Collider2D objectColliderID;
    Fire fire;
    public Slot slot;
    public CookingUI cookingUI;

	public bool isSwimming = false;
	public bool diving = false;
	public bool canFish = false;
	public bool atFire = false;
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

		//If player is pressing the interaction key
		if (Input.GetKeyDown (KeyCode.E)) {
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

		if(Input.GetKey(KeyCode.S) && isSwimming && diving){
			transform.Translate(new Vector3(Time.deltaTime * speed * movementInput,Time.deltaTime * 6.0f * -1,0), Space.World);
			
		}
		if (!performingAction) {
			transform.Translate (new Vector3 (Time.deltaTime * speed * movementInput, Time.deltaTime * speed * movementInput2, 0), Space.World);
		}
	}
    

private void HandleMovement() {
		// float translation = speed * Time.deltaTime;
		// transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0));
        if(Input.GetKeyDown(KeyCode.B)) {
            craft.Slot1.GetComponent<Image>().enabled = !craft.Slot1.GetComponent<Image>().enabled;
            craft.Slot2.GetComponent<Image>().enabled = !craft.Slot2.GetComponent<Image>().enabled;
            
            foreach(GameObject slot in backpack.allSlots) {
                slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;
                slot.GetComponentInChildren<Text>().enabled = !slot.GetComponentInChildren<Text>().enabled;
            }
            backpack.GetComponent<Image>().enabled = !backpack.GetComponent<Image>().enabled;
            craft.GetComponent<Image>().enabled = !craft.GetComponent<Image>().enabled;
            craft.enabled = !craft.enabled;
            slot.GetComponent<Image>().enabled = !slot.GetComponent<Image>().enabled;
            GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled = !GameObject.Find("Canvas/CraftTableTest/CraftButton").GetComponent<Image>().enabled;
            GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").SetActive(!GameObject.Find("Canvas/CraftTableTest/CraftButton/Text").activeInHierarchy); 
       }
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
