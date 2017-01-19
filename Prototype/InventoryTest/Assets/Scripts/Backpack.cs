using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour {

	private RectTransform backpackRect;
	private float backpackWidth, backpackHeight; 
	public int slots;
	public int rows;
	public float slotPaddingLeft, slotPaddingTop;
	public float slotSize;
	public GameObject slotPrefab;
	private List<GameObject> allSlots;
	private int emptySlot;	

	// Use this for initialization
	void Start () {
		CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateLayout() {
		allSlots = new List<GameObject>();
		emptySlot = slots;
		backpackWidth = (slots / rows) * (slotSize + 5 + slotPaddingLeft) + slotPaddingLeft;
		backpackHeight = rows * (slotSize + 5 + slotPaddingTop) + slotPaddingTop;
		backpackRect = GetComponent<RectTransform>();
		backpackRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, backpackWidth);
		backpackRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, backpackHeight);
		int columns = slots / rows;

		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < columns; x++) {
				GameObject newSlot = (GameObject)Instantiate(slotPrefab);
				RectTransform slotRect = newSlot.GetComponent<RectTransform>();
				
				newSlot.name = "Slot";
				newSlot.transform.SetParent(this.transform.parent);
				//places the slots in the inventory in each column, then row
				slotRect.localPosition = backpackRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y+1) - (slotSize * y));
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
				allSlots.Add(newSlot);
			}
		}
	}

	public bool AddItem(Item item) {
		if (item.maxSize == 1) {
			PlaceEmpty(item);
			return true;
		} else {
			foreach (GameObject slot in allSlots) {
				Slot tmp = slot.GetComponent<Slot>();
				if (!tmp.isEmpty) {
					if (tmp.CurrentItem.type == item.type && tmp.IsAvailable) {
						tmp.AddItem(item);
						return true;
					}
				}
			}
			if (emptySlot > 0) {
				PlaceEmpty(item);
			}	
		}

		return false;
	}

	private bool PlaceEmpty(Item item) {
		if (emptySlot > 0) {
			foreach (GameObject slot in allSlots) {
				Slot tmp = slot.GetComponent<Slot>();
				if (tmp.isEmpty) {
					tmp.AddItem(item);
					emptySlot--;
					return true;
				}
			}
		}
		return false;
	}
}
