using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fishing : MonoBehaviour {

 	private Transform text;
	public GameObject backpack;
	public static float fishChance = 0.003f;
	public Transform fishPrefab;
	public bool isFishing = false;
	public bool minigame = false;
	public bool hasCaught = false;
	public Transform rodAlert;
	public float delay;
	public Transform canvas;
	//Used to spawn items in backpack for fishing
	Item item;
	private Notification notification;




	// Use this for initialization
	void Start () {
		notification = GameObject.Find("Notification").GetComponent<Notification>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isFishing && !minigame) {
			delay -= Time.deltaTime;
			if(Random.Range(0f,1f) < fishChance  && Time.timeScale != 0 && delay < 0f){ 
				minigame = true;
				delay = 2f;
			}
		}

		if (minigame && hasCaught) {
			stop ();
			Backpack inv = backpack.GetComponent<Backpack> ();
			float randNum = Random.Range(0f,1f);
			//Random chance of catching different fish
			if (randNum < 0.6f) {
				item = (Resources.Load("rawGuppy") as GameObject).GetComponent<Item>();
			} else if (randNum > 0.6f && randNum < 0.9f) {
				item = (Resources.Load("rawTrout") as GameObject).GetComponent<Item>();
			} else if (randNum > 0.9f) {
				item = (Resources.Load("rawSalmon") as GameObject).GetComponent<Item>();
			}
			bool added = inv.AddItem(item);
			if(!added){
				notification.InventoryFlag = true;
			}
			Slot fishingRodSlot = backpack.GetComponent<Backpack>().FindItem(ItemType.FISHINGROD);
			fishingRodSlot.CurrentItem.Durability--;
			
			if (fishingRodSlot.CurrentItem.Durability <= 0) {
				fishingRodSlot.DestroyItem();
			}
		}

	}

	public void fish(){
		Backpack inv = backpack.GetComponent<Backpack> ();
		if(inv.CheckItem(ItemType.FISHINGROD)){
			isFishing = true;
		}
		else if(text == null){
			GetComponent<Player>().performingAction = false;
			text = Instantiate(rodAlert);
			text.SetParent(canvas);
			Vector3 position = transform.position;
			position.y += 3;
			rodAlert.position = position;
			Destroy (text.gameObject, 2.0f);
		}
	}		

	public void stop(){
		isFishing = false;
		minigame = false;
		hasCaught = false;
		delay = 2f;
	}
}