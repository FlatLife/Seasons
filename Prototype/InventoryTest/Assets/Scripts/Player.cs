using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public Backpack backpack;
	private bool canTouch = false;
    bool atFire = false;
    private Collider2D objectColliderID;
    Fire fire;
    


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
