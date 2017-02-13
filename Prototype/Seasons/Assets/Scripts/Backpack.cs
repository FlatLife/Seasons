using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour {

	private RectTransform backpackRect;
	public float backpackWidth, backpackHeight; 
	public int slots;
	public int rows;
	public float slotPaddingLeft, slotPaddingTop;
	public float slotSize;
	public GameObject slotPrefab;
	public List<GameObject> allSlots;
	private static int emptySlot;	
	private Slot from, to;
	public GameObject iconPrefab;
	public GameObject info;
	private static GameObject hoverObject;
	public Canvas canvas;

	public GameObject player;


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
			hoverObject.transform.position = new Vector3(Input.mousePosition.x -20f, Input.mousePosition.y+20f);
			if(!player.GetComponent<Player>().openUI){
				from.GetComponent<Image> ().color = Color.white;
				from = null;
				Destroy(hoverObject);
			}
		}
	}

	private void CreateLayout() {
		allSlots = new List<GameObject>();
		emptySlot = slots;
		backpackWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
		backpackHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop + 30;
		backpackRect = GetComponent<RectTransform>();
		backpackRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, backpackWidth);
		backpackRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, backpackHeight);

		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "Use this UI to manage your inventory. \nPicked up items are placed in here.\n From here you can right click to use some items and also left click to pick up and move items to new slots.";
		info.GetComponent<InformationPopup>().width = 300;
		info.GetComponent<InformationPopup>().height = 100;
		info.GetComponent<RectTransform>().localPosition = new Vector3(230, -25);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
		info.GetComponent<Image>().enabled = false;
		int columns = slots / rows;

		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < columns; x++) {
				GameObject newSlot = (GameObject)Instantiate(slotPrefab);
				RectTransform slotRect = newSlot.GetComponent<RectTransform>();
				newSlot.name = "Slot";
				newSlot.GetComponent<Slot>().canShift = true;
				newSlot.transform.SetParent(this.transform);
				//places the slots in the inventory in each column, then row
				if(y == 0){
					slotRect.localPosition = new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), (-slotPaddingTop-30) * (y+1) - (slotSize * y));
				}else{
					slotRect.localPosition = new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), (-slotPaddingTop * (y+1)) - 30 - (slotSize * y));
				}
				slotRect.sizeDelta = new Vector3(slotSize, slotSize);
				allSlots.Add(newSlot);
				newSlot.GetComponent<Image>().enabled = false;
			}
		}
	}

	public bool AddItem(Item item) {
		if (item.maxSize != 1 ) {
			foreach (GameObject slot in allSlots) {
				Slot tmp = slot.GetComponent<Slot>();
				if (!tmp.isEmpty) {
					if (tmp.CurrentItem.type == item.type && tmp.IsAvailable) {
						tmp.deltaCount(1);
						return true;
					}
				}
			}
		}
		foreach (GameObject slot in allSlots) {
			Slot tmp = slot.GetComponent<Slot>();
			if (tmp.isEmpty) {
				Item newItem = Instantiate(item, tmp.transform);
				BoxCollider2D collider = newItem.gameObject.GetComponent<BoxCollider2D>();
				if(collider != null){
					collider.enabled = false;
				}
				newItem.transform.position = new Vector3 (0,20f,0);
				newItem.GetComponent<SpriteRenderer>().enabled = false;
				tmp.AddItem(newItem);
				emptySlot--;
				return true;
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

				hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
				hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
				hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

				hoverObject.transform.localScale = from.gameObject.transform.localScale;
			}
		} else if (to == null) {
			to = clicked.GetComponent<Slot>();
			Destroy(GameObject.Find("Hover"));
		}
		if (to != null && from != null) {
			if (to != from && 
				!to.isEmpty && 
				to.IsAvailable &&
				to.CurrentItem.type == from.CurrentItem.type) {
				int toAdd = from.ItemCount;
				int toCountBefore = to.ItemCount;
				int toCountAfter = to.AddItems(to.CurrentItem, to.ItemCount + from.ItemCount);
				if ((toAdd - (toCountAfter - toCountBefore)) != 0) {
					from.AddItems(from.CurrentItem, (toAdd - (toCountAfter - toCountBefore)));
				} else {
					from.ClearSlot();
				}

				from.GetComponent<Image>().color = Color.white;
				to = null;
				from = null;
				hoverObject = null;
				EmptySlot++;
			} else if (to == from) {
				from.GetComponent<Image>().color = Color.white;
				to = null;
				from = null;
				hoverObject = null;
			} else {
				Item fromItem = from.CurrentItem;
				int fromCount = from.ItemCount;
				from.AddItems(to.CurrentItem, to.ItemCount);
				to.AddItems(fromItem, fromCount);

				from.GetComponent<Image>().color = Color.white;
				to = null;
				from = null;
				hoverObject = null;
			}
		}
	}
	}

public bool CheckItem(ItemType type){
		foreach (GameObject slot in allSlots) {
			Slot tmp = slot.GetComponent<Slot>();
			if(!tmp.isEmpty && tmp.CurrentItem.type == type){
				return true;
			}
		}
		return false;
	}

	public Slot FindItem(ItemType type){
		foreach (GameObject slot in allSlots) {
			Slot tmp = slot.GetComponent<Slot>();
			if(!tmp.isEmpty && tmp.CurrentItem.type == type){
				return tmp;
			}
		}
		return null;
	}

	public int EmptySlots(){
		int slots = 0;
		foreach (GameObject slot in allSlots) {
			Slot tmp = slot.GetComponent<Slot>();
			if (tmp.isEmpty) {
				slots++;
			}
		}
		return slots;
	}
	
}