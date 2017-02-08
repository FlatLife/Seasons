﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelUI : MonoBehaviour {

	public Slot slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform barrelUIRect;
	public Slot bucketEmptySlot;
	public Slot bucketFillSlot;
	public GameObject WaterBar;
	public GameObject WaterMask;
	public RectTransform WaterBarRect;
	public RectTransform WaterMaskRect;

	public void Initialize() {
		WaterBar = GameObject.Find("WaterBar");
		WaterMask = GameObject.Find("WaterBar/Mask");
		WaterBarRect = WaterBar.GetComponent<RectTransform>();
		WaterMaskRect = WaterMask.GetComponent<RectTransform>();
		
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(200, 300, 0);
		uiRect.position = new Vector3(600,400);
		WaterBarRect.localPosition = new Vector3(180, -16);
		WaterMaskRect.localPosition = new Vector3(7, -8);
		WaterBar.transform.SetParent(this.transform);
		WaterMask.transform.SetParent(this.WaterBar.transform);

		bucketEmptySlot = Instantiate(slotPrefab);
		bucketEmptySlot.name = "FreshWaterSlot";
		bucketFillSlot = Instantiate(slotPrefab);
		bucketFillSlot.name = "EmptySlot";

		RectTransform bucketEmptyRect = bucketEmptySlot.GetComponent<RectTransform>();
		bucketEmptySlot.transform.SetParent(this.transform);
		bucketEmptyRect.localPosition = new Vector3(40, -20);
		bucketEmptyRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		bucketEmptyRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		RectTransform bucketFillRect = bucketFillSlot.GetComponent<RectTransform>();
		bucketFillSlot.transform.SetParent(this.transform);
		bucketFillRect.localPosition = new Vector3(100, -20);
		bucketFillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		bucketFillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		bucketEmptySlot.GetComponent<Image>().enabled = false;
		bucketFillSlot.GetComponent<Image>().enabled = false;
	}
}
