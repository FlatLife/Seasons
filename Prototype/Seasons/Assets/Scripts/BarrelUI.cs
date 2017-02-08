using System.Collections;
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


	public void Initialize() {
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(200, 300, 0);
		uiRect.position = new Vector3(600,400);

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
