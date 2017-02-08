using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPurifierUI : MonoBehaviour {

	public Slot slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform WaterUIRect;

	public GameObject info;
	public Slot FreshWaterSlot;
	public Slot SaltWaterSlot;



	public void Initialize() {		
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(228, 123, 0);
		uiRect.position = new Vector3(400,400);

		FreshWaterSlot = Instantiate(slotPrefab);
		FreshWaterSlot.name = "FreshWaterSlot";
		RectTransform FreshwaterRect = FreshWaterSlot.GetComponent<RectTransform>();
		FreshWaterSlot.transform.SetParent(this.transform);
		FreshwaterRect.localPosition = new Vector3(60, -40);
		FreshwaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		FreshwaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		FreshWaterSlot.GetComponent<Image>().enabled = false;

		SaltWaterSlot = Instantiate(slotPrefab);
		SaltWaterSlot.name = "SaltWaterSlot";
		RectTransform SaltWaterRect = SaltWaterSlot.GetComponent<RectTransform>();
		SaltWaterSlot.transform.SetParent(this.transform);
		SaltWaterRect.localPosition = new Vector3(130, -40);
		SaltWaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		SaltWaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
		SaltWaterSlot.GetComponent<Image>().enabled = false;

		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "WaterHelp";
		info.GetComponent<RectTransform>().localPosition = new Vector3(200, -25);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
		info.GetComponent<Image>().enabled = false;

	}
}
