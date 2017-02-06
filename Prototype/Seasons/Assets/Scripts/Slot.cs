using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {
	private Item currentItem;
	private int itemCount;

	public GameObject tooltip;
	public Text stackTxt;
	public Sprite slotEmpty;
	public Sprite slotHighlighted;
	public float cookTime = 2;
	public float growTime;
	public bool isGrowing;
	
	public Item CurrentItem {
		get {return currentItem;}
		set {currentItem = value;}
	}

	public int ItemCount {
		get {return itemCount;}
		set {itemCount = value;}
	}

	public bool isEmpty {
		get {return itemCount == 0;}
	}

	public bool IsAvailable {
		get {return CurrentItem.maxSize > itemCount; }
	}

	// Use this for initialization
	void Start () {
		currentItem = null;
		RectTransform slotRect = GetComponent<RectTransform>();
		RectTransform txtRect = stackTxt.GetComponent<RectTransform>();
		
		int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);
		stackTxt.resizeTextMaxSize = txtScaleFactor;
		stackTxt.resizeTextMinSize = txtScaleFactor;

		txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
		txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
	}

	public void AddItem(Item item) {
		currentItem = item;
		item.transform.SetParent(this.transform);
		itemCount = 1;

		stackTxt.text = itemCount > 1 ? itemCount.ToString() : string.Empty;
		ChangeSprite(currentItem.spriteNeutral, currentItem.spriteHighlighted);
	}

	public void deltaCount(int change) {
		itemCount += change;
		stackTxt.text = itemCount > 1 ? itemCount.ToString() : string.Empty;
	}

	// Returned int is the number of items that were able to be added.
	public int AddItems(Item item, int count) {
		int addedCount = 0;
		itemCount = 0;
		currentItem = item;
		if (currentItem != null) {
			while (itemCount < currentItem.maxSize && addedCount < count) {
				itemCount += 1;
				addedCount++;
			}
			ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
			currentItem.transform.SetParent(this.transform);
		} else {
			itemCount = 0;
			ChangeSprite(slotEmpty, slotHighlighted);
		}
		stackTxt.text = itemCount > 1 ? itemCount.ToString() : string.Empty;
		return addedCount;
	}

	private void ChangeSprite(Sprite neutral, Sprite highlighted) {
		GetComponent<Image>().sprite = neutral;
		SpriteState st = new SpriteState();
		st.highlightedSprite = highlighted;
		st.pressedSprite = neutral;

		GetComponent<Button>().spriteState = st;
	}

	public void UseItem() {
		if (!isEmpty) {
			currentItem.slot = this;
			bool toDelete = currentItem.Use();
			if (toDelete) {
				DestroyItem();
			}
			
		}
	}

	public void DestroyItem() {
		if (!isEmpty) {
			itemCount--;
			stackTxt.text = itemCount > 1 ? itemCount.ToString() : string.Empty;
			if (isEmpty) {
				Destroy(currentItem.gameObject);
				ChangeSprite(slotEmpty, slotHighlighted);
				Backpack.EmptySlot++;
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			UseItem();
		}
	}

	public void popup(){
		if(!isEmpty){
			tooltip = Instantiate(Resources.Load<GameObject>("Tooltip"));
			tooltip.name = "tooltip";
			string name = CurrentItem.itemName;
			string use = CurrentItem.itemUse;
			int lengthName = name.Length;
			int lengthUse = use.Length;
			lengthName = lengthName * 10;
			lengthUse = lengthUse * 10;
			RectTransform tooltipRect = tooltip.GetComponent<RectTransform> ();
			tooltipRect.sizeDelta = new Vector2(lengthName, tooltipRect.sizeDelta.y);
			Text tooltipText = tooltip.GetComponentInChildren<Text> ();
			tooltipText.text = CurrentItem.itemName;
			RectTransform tooltipTextRect = tooltipText.gameObject.GetComponent<RectTransform> ();
			tooltipTextRect.sizeDelta = new Vector2 (lengthName, tooltipTextRect.sizeDelta.y );
			tooltip.transform.SetParent(GameObject.Find("Canvas").transform, false);
			tooltip.transform.position = this.transform.position;
			tooltip.transform.position = new Vector3(tooltip.transform.position.x + 60f ,tooltip.transform.position.y - 40f , -50f);
			Component[] images = tooltip.GetComponentsInChildren<Image> ();
			if(lengthUse > 0){
				foreach (Image image in images) {
					if (image.gameObject.name == "Desc") {
						image.enabled = true;
						image.gameObject.GetComponentInChildren<Text> ().text = CurrentItem.itemUse;
						image.gameObject.GetComponentInChildren<Text> ().rectTransform.sizeDelta = new Vector2 (lengthUse, tooltipTextRect.sizeDelta.y );
						image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (lengthUse, tooltipTextRect.sizeDelta.y );
					}
				}
			}else{
				foreach (Image image in images) {
					if (image.gameObject.name == "Desc") {
						image.enabled = false;
					}
				}
			}
		}
	}


	public void popupDestroy(){
		Destroy(tooltip);
	}

	public void ClearSlot() {
		itemCount = 0;
		Destroy(currentItem);
		ChangeSprite(slotEmpty, slotHighlighted);
		stackTxt.text = string.Empty;
	}

}
