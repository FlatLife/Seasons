using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelUI : MonoBehaviour {

	public Slot slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform barrelUIRect;
	public Slot bucketSlot;


	public void Initialize() {
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(200, 300, 0);
		uiRect.position = new Vector3(600,400);

		bucketSlot = Instantiate(slotPrefab);
		bucketSlot.name = "FreshWaterSlot";
		RectTransform barrelUIRect = bucketSlot.GetComponent<RectTransform>();
		bucketSlot.transform.SetParent(this.transform);
		barrelUIRect.localPosition = new Vector3(80, -20);
		barrelUIRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		barrelUIRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		bucketSlot.GetComponent<Image>().enabled = false;

	}
}
