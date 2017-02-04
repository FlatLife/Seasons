using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPurifier : MonoBehaviour {

	public Canvas canvas;
	public WaterPurifierUI WaterUI;
	public float waterPurify = 3f;
	// Use this for initialization

	void Awake () {
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		WaterUI = Instantiate(Resources.Load<WaterPurifierUI>("WaterPurifierUI"));
		WaterUI.name = "WaterPurifierUI";
		WaterUI.Initialize();
		WaterUI.transform.SetParent(canvas.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (WaterUI.SaltWaterSlot.Items.Count > 0 && WaterUI.FreshWaterSlot.Items.Count > 0) {
			if (WaterUI.SaltWaterSlot.CurrentItem.type == ItemType.SALTWATER
			   && WaterUI.FreshWaterSlot.CurrentItem.type == ItemType.BUCKET) {
				waterPurify -= Time.deltaTime;
				if (waterPurify <= 0) {
					WaterUI.FreshWaterSlot.ClearSlot ();
					WaterUI.FreshWaterSlot.AddItem (Resources.Load<Item> ("FreshWater"));
					WaterUI.SaltWaterSlot.ClearSlot ();
					WaterUI.SaltWaterSlot.AddItem(Resources.Load<Item>("Bucket"));
					waterPurify = 3f;
				}
			}
		}
	}
}
