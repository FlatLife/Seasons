﻿using System.Collections;
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
		if (!WaterUI.SaltWaterSlot.isEmpty && !WaterUI.FreshWaterSlot.isEmpty) {
			if (WaterUI.SaltWaterSlot.CurrentItem.type == ItemType.SALTWATER
			   && WaterUI.FreshWaterSlot.CurrentItem.type == ItemType.BUCKET) {
				waterPurify -= Time.deltaTime;
				if (waterPurify <= 0) {
					WaterUI.FreshWaterSlot.ClearSlot ();
					Item water = Instantiate(Resources.Load<Item> ("FreshWater"));
					water.transform.position = new Vector3 (0,20f,0);
					WaterUI.FreshWaterSlot.AddItem (water);
					WaterUI.SaltWaterSlot.ClearSlot ();
					Item bucket = Instantiate(Resources.Load<Item> ("Bucket"));
					bucket.transform.position = new Vector3 (0,20f,0);
					WaterUI.SaltWaterSlot.AddItem(bucket);
					waterPurify = 3f;
				}
			}
		}
	}
}