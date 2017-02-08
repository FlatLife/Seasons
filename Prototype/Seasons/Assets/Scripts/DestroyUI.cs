using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyUI : MonoBehaviour {

	public GameObject destroySlot;
	public GameObject slotPrefab;
	public GameObject info;
	//private RectTransform DestroyUIRect;
	public float slotSize;

	// Use this for initialization
	void Start () {
		CreateDestroyUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateDestroyUI() {
		destroySlot = (GameObject)Instantiate(slotPrefab);
		RectTransform destroySlotRect = destroySlot.GetComponent<RectTransform>();
		//DestroyUIRect = GetComponent<RectTransform>();
		destroySlot.name = "DestroySlot";
		destroySlot.transform.SetParent(this.transform);

		destroySlotRect.localPosition = new Vector3(46, -20);
		destroySlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		destroySlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
		
		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "Use this UI to delete unwanted items. \nPlace an item in the slot then click delete";
		info.GetComponent<InformationPopup>().width = 200;
		info.GetComponent<InformationPopup>().height = 70;
		info.GetComponent<RectTransform>().localPosition = new Vector3(105, -25);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);

		info.GetComponent<Image>().enabled = false;
		destroySlot.GetComponent<Image>().enabled = false;
	}

	public void DestroyItem() {
		if (!destroySlot.GetComponent<Slot>().isEmpty) {
			destroySlot.GetComponent<Slot>().DestroyItem();
		}
	}
}
