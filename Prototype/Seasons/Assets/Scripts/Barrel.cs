using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

	public Canvas canvas;
	public BarrelUI BarrelUI;
	// set our barrel fill bar to 0, contains nothing 
	public float barrelFill = 0f;
	public Backpack backpack;
	private GameObject water;

	void Awake () {
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		BarrelUI = Instantiate(Resources.Load<BarrelUI>("BarrelUI"));
		BarrelUI.name = "BarrelUI";
		BarrelUI.Initialize();
		BarrelUI.transform.SetParent(canvas.transform);
		water = BarrelUI.WaterBar;
	}
	
	// Update is called once per frame
	void Update () {

		//empty slot
	if(!BarrelUI.bucketEmptySlot.isEmpty){
		if((BarrelUI.bucketEmptySlot.CurrentItem.type == ItemType.FRESHWATER) && !(barrelFill >= 1f)) {
			barrelFill+=0.1f;
			water.GetComponent<BarScript>().increment(0.1f);
			BarrelUI.bucketEmptySlot.ClearSlot();
			Item item = Instantiate(Resources.Load<Item> ("Bucket"));
			item.transform.position = new Vector3 (0,20f,0);
			BarrelUI.bucketEmptySlot.AddItem(item);
		}
	}
		//fill slot
	if(!BarrelUI.bucketFillSlot.isEmpty) {
		if(BarrelUI.bucketFillSlot.CurrentItem.type == ItemType.BUCKET) {
				if(barrelFill > 0) {
					barrelFill-=0.1f;
					water.GetComponent<BarScript>().decrement(0.1f);
					BarrelUI.bucketFillSlot.ClearSlot();
					Item item = Instantiate(Resources.Load<Item> ("FreshWater"));
					item.transform.position = new Vector3 (0,20f,0);
					BarrelUI.bucketFillSlot.AddItem(item);
				}
			}
		}
	}
}

