using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyUI : MonoBehaviour {

	public GameObject destroySlot;
	public GameObject slotPrefab;
	private RectTransform DestroyUIRect;
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
		DestroyUIRect = GetComponent<RectTransform>();
		destroySlot.name = "DestroySlot";
		destroySlot.transform.SetParent(this.transform.parent);

		destroySlotRect.localPosition = DestroyUIRect.localPosition + new Vector3(100, -30);
		destroySlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		destroySlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		destroySlot.GetComponent<Image>().enabled = false;
	}

	public void DestroyItem() {
		if (!destroySlot.GetComponent<Slot>().isEmpty) {
			destroySlot.GetComponent<Slot>().DestroyItem();
		}
	}
}
