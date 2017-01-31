using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour {

	public GameObject slotPrefab;
	public float slotSize;
	public GameObject[] cookSlots;

	public void Initialize(int slotCount) {
		cookSlots = new GameObject[slotCount];
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(slotSize * slotCount + slotSize, slotSize + slotSize/2, 0);
		uiRect.position = new Vector3(400,200);
		for (int i = 0; i < slotCount; i++) {
			GameObject cookSlot = (GameObject)Instantiate(slotPrefab);
			RectTransform slotRect = cookSlot.GetComponent<RectTransform>();
			cookSlot.name = "Cook Slot " + i;
			cookSlot.transform.SetParent(this.transform);
			slotRect.localPosition = new Vector3(slotSize/(slotCount+1) + (slotSize/(slotCount+1) + slotSize) * i, -slotSize/4);
			slotRect.sizeDelta = new Vector3(slotSize, slotSize);
			cookSlot.GetComponent<Image>().enabled = false;
			cookSlots[i] = cookSlot;
		}
	}
}
