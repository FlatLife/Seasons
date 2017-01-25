using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour {

	private RectTransform backpackRect;
	private float backpackWidth, backpackHeight; 
	public int slots;
	public int rows;
	public float slotPaddingLeft, slotPaddingTop;
	public float slotSize;
	public GameObject slotPrefab;
	public List<GameObject> allSlots;
	private static int emptySlot;	
	private Slot from, to;
	public GameObject iconPrefab;
	private static GameObject hoverObject;
	public Canvas canvas;
	private float hoverYOffset;

	public static int EmptySlot {
		get { return emptySlot; }
		set { emptySlot = value;}
	}

	// Use this for initialization
	void Awake () {
		CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {
		if (hoverObject != null) {
			Vector2 position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
			position.Set(position.x, position.y - hoverYOffset);
			hoverObject.transform.position = canvas.transform.TransformPoint(position);
		}
	}

	private void CreateLayout() {
		allSlots = new List<GameObject>();
		emptySlot = slots;
		hoverYOffset = slotSize * 0.01f;
		backpackWidth = (slots / rows) * (slotSize + 5f + slotPaddingLeft) + slotPaddingLeft;
		backpackHeight = rows * (slotSize + 5f + slotPaddingTop) + slotPaddingTop;
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
				newSlot.GetComponent<Image>().enabled = false;
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

	public void MoveItem(GameObject clicked) {
		if(clicked.GetComponent<Image>().enabled){
		if (from == null) {
			if (!clicked.GetComponent<Slot>().isEmpty) {
				from = clicked.GetComponent<Slot>();
				from.GetComponent<Image>().color = Color.gray;

				hoverObject = (GameObject)Instantiate(iconPrefab);
				hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
				hoverObject.name = "Hover";

				RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
				RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

				hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
				hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

				hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
				hoverObject.transform.localScale = from.gameObject.transform.localScale;
			}
		} else if (to == null) {
			to = clicked.GetComponent<Slot>();
			Destroy(GameObject.Find("Hover"));
		}
		if (to != null && from != null) {
			if (to != from && 
				to.GetComponent<Slot>().Items.Count > 0 && 
				to.GetComponent<Slot>().CurrentItem.type == from.GetComponent<Slot>().CurrentItem.type) {
				to.StackItems(from.Items);
				if (from.Items.Count > 0) {
					from.AddItems(from.Items);
				} else {
					from.ClearSlot();
				}
				from.GetComponent<Image>().color = Color.white;
				to = null;
				from = null;
				hoverObject = null;
				EmptySlot++;
			} else {
				Stack<Item> tmpTo = new Stack<Item>(to.Items);
				to.AddItems(from.Items);
			
				if (tmpTo.Count == 0) {
					from.ClearSlot();
				} else {
					from.AddItems(tmpTo);
				}
				from.GetComponent<Image>().color = Color.white;
				to = null;
				from = null;
				hoverObject = null;
			}
		}
	}
	}

	public bool CheckItem(Item item){
		var type = item.type;
		foreach (GameObject slot in allSlots) {
			Slot tmp = slot.GetComponent<Slot>();
			if(!tmp.isEmpty && tmp.CurrentItem.type == type){
				return true;
			}
		}
		return false;
	}
	
}