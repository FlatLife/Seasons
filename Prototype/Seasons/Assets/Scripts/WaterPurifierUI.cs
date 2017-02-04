using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPurifierUI : MonoBehaviour {

	public Slot slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform WaterUIRect;
	public Slot FreshWaterSlot;
	public Slot SaltWaterSlot;

	public void Initialize() {
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(slotSize * 2 + slotSize, slotSize + slotSize/2, 0);
		uiRect.position = new Vector3(400,200);

		FreshWaterSlot = Instantiate(slotPrefab);
		FreshWaterSlot.name = "FreshWaterSlot";
		RectTransform FreshwaterRect = FreshWaterSlot.GetComponent<RectTransform>();
		FreshWaterSlot.transform.SetParent(this.transform);
		FreshwaterRect.localPosition = new Vector3(slotSize/(2+1) + (slotSize/(2+1) + slotSize) * 1, -slotSize/4);
		FreshwaterRect.sizeDelta = new Vector3(slotSize, slotSize);

		FreshWaterSlot.GetComponent<Image>().enabled = false;

		SaltWaterSlot = Instantiate(slotPrefab);
		SaltWaterSlot.name = "SaltWaterSlot";
		RectTransform SaltWaterRect = SaltWaterSlot.GetComponent<RectTransform>();
		SaltWaterSlot.transform.SetParent(this.transform);
		SaltWaterRect.localPosition = new Vector3(slotSize/(2+1) + (slotSize/(2+1) + slotSize) * 0, -slotSize/4);
		SaltWaterRect.sizeDelta = new Vector3(slotSize, slotSize);
		SaltWaterSlot.GetComponent<Image>().enabled = false;
	}
}
