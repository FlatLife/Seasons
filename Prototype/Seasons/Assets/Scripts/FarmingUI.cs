using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmingUI : MonoBehaviour {

	public GameObject slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform FarmingUIRect;
	public GameObject[] farmSlots;

	
	public void Initialize(int slotCount) {
		farmSlots = new GameObject[slotCount];
		this.transform.localScale = new Vector3(1, 1);
		for (int i = 0; i < slotCount; i++) {
			GameObject farmSlot = (GameObject)Instantiate(slotPrefab);
			RectTransform slotRect = farmSlot.GetComponent<RectTransform>();
			farmSlot.name = "Farm Slot " + i;
			farmSlot.transform.SetParent(this.transform);
			slotRect.localPosition = new Vector3((0.2f/slotCount) + (1.0f/slotCount) * i, -0.2f);
			slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
			slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
			farmSlot.GetComponent<Image>().enabled = false;
			farmSlots[i] = farmSlot;
		}
	}
}
