using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmingUI : MonoBehaviour {

	public Slot slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform FarmingUIRect;
	public Slot[] farmSlots;
	public Slot waterSlot;

	
	public void Initialize(int slotCount) {
		farmSlots = new Slot[slotCount];
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3((slotSize * slotCount + slotSize) , (slotSize + slotSize/2) * 2, 0);
		uiRect.position = new Vector3(350,550);
		for (int i = 0; i < slotCount; i++) {
			Slot farmSlot = Instantiate(slotPrefab);
			RectTransform slotRect = farmSlot.GetComponent<RectTransform>();
			farmSlot.name = "Farm Slot " + i;
			farmSlot.transform.SetParent(this.transform);
			if (i < 4) {
				slotRect.localPosition = new Vector3((slotSize/(slotCount) + (slotSize/(slotCount+1) + slotSize*2) * i) + 30, -slotSize/4.8f);
			} else {
				slotRect.localPosition = new Vector3((slotSize/(slotCount) + (slotSize/(slotCount+1) + slotSize*2) * (i-4)) + 30, -slotSize * 1.6f);
			}
			slotRect.sizeDelta = new Vector3(slotSize, slotSize);
			farmSlot.GetComponent<Image>().enabled = false;
			farmSlots[i] = farmSlot;
		}

		waterSlot = Instantiate(slotPrefab);
		waterSlot.name = "WaterSlot";
		RectTransform waterRect = waterSlot.GetComponent<RectTransform>();
		waterRect.localPosition = new Vector3(820,500);
		waterRect.sizeDelta = new Vector3(slotSize, slotSize);
		waterSlot.transform.SetParent(this.transform);
		waterSlot.GetComponent<Image>().enabled = false;
	}
}
