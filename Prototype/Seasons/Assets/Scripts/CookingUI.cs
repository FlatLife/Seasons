﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour {

	public GameObject slotPrefab;
	public float slotSize;

	private Backpack BackPack;
	private RectTransform CookingUIRect;
	public GameObject Slot4;
	public GameObject Slot5;
	public GameObject Slot6;

	Slot tmp1;
	Slot tmp2;
	Slot tmp3;

	bool cooking1 = false;
	bool cooking2 = false;
	bool cooking3 = false;

	// Use this for initialization
	void Awake () {
		CreateCookingUI();
	}
	
	// Update is called once per frame
	void Update () {
		//Update reference to what item is in the slot
		tmp1 = Slot4.GetComponent<Slot>();
        tmp2 = Slot5.GetComponent<Slot>();
        tmp3 = Slot6.GetComponent<Slot>();
		Fire fire = GameObject.Find("Fire").GetComponent<Fire>();

		if(!tmp1.isEmpty){
			if(tmp1.CurrentItem.type == ItemType.RAWFISH && !cooking1){		
				fire.startCooking(1);
				cooking1 = true;
			}
		}
		if(!tmp2.isEmpty){
			if(tmp2.CurrentItem.type == ItemType.RAWFISH && !cooking2){
				fire.startCooking(2);
				cooking2 = true;
			}
		}

		if(!tmp3.isEmpty){
			if(tmp3.CurrentItem.type == ItemType.RAWFISH && !cooking3){
				fire.startCooking(3);
				cooking3 = true;
			}
		}
	}
	

	public void CreateCookingUI() {
			Slot4 = (GameObject)Instantiate(slotPrefab);
			Slot5 = (GameObject)Instantiate(slotPrefab);
			Slot6 = (GameObject)Instantiate(slotPrefab);

			RectTransform slotRect4 = Slot4.GetComponent<RectTransform>();
			RectTransform slotRect5 = Slot5.GetComponent<RectTransform>();
			RectTransform slotRect6 = Slot6.GetComponent<RectTransform>();
			CookingUIRect = GetComponent<RectTransform>();

			Slot4.name = "Slot4";
			Slot5.name = "Slot5";
			Slot6.name = "Slot6";

			//Slot1.GetComponent<Button>().interactable = false;
			Slot4.transform.SetParent(this.transform.parent);
			Slot5.transform.SetParent(this.transform.parent);
			Slot6.transform.SetParent(this.transform.parent);



			//places the slots in the inventory in each column, then row
			slotRect4.localPosition = CookingUIRect.localPosition + new Vector3(10, -20);
			slotRect4.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
			slotRect4.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

			slotRect5.localPosition = CookingUIRect.localPosition + new Vector3(90, -20);			
			slotRect5.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
			slotRect5.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

			slotRect6.localPosition = CookingUIRect.localPosition + new Vector3(170, -20);		
			slotRect6.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
			slotRect6.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
			
			Slot4.GetComponent<Image>().enabled = false;
			Slot5.GetComponent<Image>().enabled = false;
			Slot6.GetComponent<Image>().enabled = false;
	}
}
