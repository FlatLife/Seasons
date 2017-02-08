using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour {

	public GameObject slotPrefab;
	public float slotSize;
	public GameObject Slot;
	public GameObject info;



	// Use this for initialization
	void Awake () {
		CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateLayout() {
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(300,400);
		uiRect.position = new Vector3(290, -110, 0);
		
	}

	public void AddEntry(Item item){
		Slot = (GameObject)Instantiate(slotPrefab);


		RectTransform slotRect = Slot.GetComponent<RectTransform>();

		Slot.name = "Slot";

		Slot.transform.SetParent(this.transform);

		//places the slots in the inventory in each column, then row
		slotRect.localPosition = new Vector3(60, -20);
		slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
		
		//Slot.GetComponent<Button>().interactable = false;
		//Slot.GetComponent<Image>().enabled = false;
	}
}
