using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {

    private Stack<Item> items;
	public Text stackTxt;
	public Sprite slotEmpty;
	public Sprite slotHighlighted;
	public float cookTime = 2;
	

	public Stack<Item> Items {
		get {return items;}
		set {items = value;}
	}

	public bool isEmpty {
		get {return items.Count == 0;}
	}

	public bool IsAvailable {
		get {return CurrentItem.maxSize > items.Count; }
	}

	public Item CurrentItem {
		get {return items.Peek(); }
	}

	// Use this for initialization
	void Start () {

		//myItemType = ItemType.FISHINGROD;

		//stack stuff
		items = new Stack<Item>();
		RectTransform slotRect = GetComponent<RectTransform>();
		RectTransform txtRect = stackTxt.GetComponent<RectTransform>();
		
		int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);
		stackTxt.resizeTextMaxSize = txtScaleFactor;
		stackTxt.resizeTextMinSize = txtScaleFactor;

		txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
		txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddItem(Item item) {
		items.Push(item);

		if (items.Count > 1) {
			stackTxt.text = items.Count.ToString();
		}

		ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
	}

	public void AddItems(Stack<Item> items) {
		this.items = new Stack<Item>(items);
		stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
		ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
	}

	public void StackItems(Stack<Item> items) {
		int maxSize = items.Peek().maxSize;
		while(items.Count > 0 && this.items.Count < maxSize) {
			this.AddItem(items.Pop());
		}
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
			items.Pop().Use();
			stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

			if (isEmpty) {
				ChangeSprite(slotEmpty, slotHighlighted);
				Backpack.EmptySlot++;
			}
		}
	}

	public void DestroyItem() {
		if (!isEmpty) {
			items.Pop();
			stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

			if (isEmpty) {
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

	public void ClearSlot() {
		items.Clear();
		ChangeSprite(slotEmpty, slotHighlighted);
		stackTxt.text = string.Empty;
	}
}
