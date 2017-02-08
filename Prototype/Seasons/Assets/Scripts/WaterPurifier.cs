using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPurifier : MonoBehaviour {

	public Canvas canvas;
	public WaterPurifierUI WaterUI;
	public float waterPurify = 3f;
	private GameObject water;
	// Use this for initialization

	void Awake () {
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		WaterUI = Instantiate(Resources.Load<WaterPurifierUI>("WaterPurifierUI"));
		WaterUI.name = "WaterPurifierUI";
		WaterUI.Initialize();
		WaterUI.transform.SetParent(canvas.transform);
		water = GameObject.Find("WaterBar");
	}
	
	// Update is called once per frame
	void Update () {
		 if(Time.timeScale == 0)return;
		if (!WaterUI.Slot1.isEmpty && !WaterUI.Slot2.isEmpty) {
			if (WaterUI.Slot1.CurrentItem.type == ItemType.SALTWATER
			   && WaterUI.Slot2.CurrentItem.type == ItemType.BUCKET) {
				waterPurify -= Time.deltaTime;
				if (waterPurify <= 0) {
					WaterUI.Slot2.ClearSlot ();
					Item water = Instantiate(Resources.Load<Item> ("FreshWater"));
					water.transform.position = new Vector3 (0,20f,0);
					WaterUI.Slot2.AddItem (water);
					WaterUI.Slot1.ClearSlot ();
					Item bucket = Instantiate(Resources.Load<Item> ("Bucket"));
					bucket.transform.position = new Vector3 (0,20f,0);
					WaterUI.Slot1.AddItem(bucket);
					waterPurify = 3f;
				}
			}else if (WaterUI.Slot2.CurrentItem.type == ItemType.SALTWATER
			   && WaterUI.Slot1.CurrentItem.type == ItemType.BUCKET) {
				waterPurify -= Time.deltaTime;
				if (waterPurify <= 0) {
					WaterUI.Slot1.ClearSlot ();
					Item water = Instantiate(Resources.Load<Item> ("FreshWater"));
					water.transform.position = new Vector3 (0,20f,0);
					WaterUI.Slot1.AddItem (water);
					WaterUI.Slot2.ClearSlot ();
					Item bucket = Instantiate(Resources.Load<Item> ("Bucket"));
					bucket.transform.position = new Vector3 (0,20f,0);
					WaterUI.Slot2.AddItem(bucket);
					waterPurify = 3f;
				}
			}
		}
	}
}
