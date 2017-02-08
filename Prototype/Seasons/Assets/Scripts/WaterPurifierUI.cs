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
	public Slot Slot1;
	public Slot Slot2;



	public void Initialize() {
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(228, 123, 0);
		uiRect.position = new Vector3(400,400);

		Slot1 = Instantiate(slotPrefab);
		Slot1.name = "Slot1";
		RectTransform FreshwaterRect = Slot1.GetComponent<RectTransform>();
		Slot1.transform.SetParent(this.transform);
		FreshwaterRect.localPosition = new Vector3(60, -40);
		FreshwaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		FreshwaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		Slot1.GetComponent<Image>().enabled = false;

		Slot2 = Instantiate(slotPrefab);
		Slot2.name = "Slot2";
		RectTransform SaltWaterRect = Slot2.GetComponent<RectTransform>();
		Slot2.transform.SetParent(this.transform);
		SaltWaterRect.localPosition = new Vector3(130, -40);
		SaltWaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		SaltWaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
		Slot2.GetComponent<Image>().enabled = false;

		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "WaterHelp";
		info.GetComponent<RectTransform>().localPosition = new Vector3(200, -25);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
		info.GetComponent<Image>().enabled = false;

	}
}
