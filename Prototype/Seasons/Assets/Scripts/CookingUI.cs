using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour {

	public GameObject slotPrefab;
	public float slotSize;

	private Backpack BackPack;
	private RectTransform CookingUIRect;
	public GameObject[] cookSlots;

	public void Initialize(int slotCount) {
		cookSlots = new GameObject[slotCount];
		this.transform.localScale = new Vector3(slotSize/30 * slotCount, slotSize/30, 0);
		for (int i = 0; i < slotCount; i++) {
			GameObject cookSlot = (GameObject)Instantiate(slotPrefab);
			RectTransform slotRect = cookSlot.GetComponent<RectTransform>();
			cookSlot.name = "Cook Slot " + i;
			cookSlot.transform.SetParent(this.transform);
			slotRect.localPosition = new Vector3((0.2f/slotCount) + (1.0f/slotCount) * i, -0.2f);
			slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
			slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
			cookSlot.GetComponent<Image>().enabled = false;
			cookSlots[i] = cookSlot;
		}
	}
}
