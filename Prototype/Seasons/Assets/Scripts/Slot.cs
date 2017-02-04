using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {

    private Stack<Item> items;

	public GameObject tooltip;
	public Text stackTxt;
	public Sprite slotEmpty;
	public Sprite slotHighlighted;
	public float cookTime = 2;
	public float growTime = 5;
	

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
		cookTime = 2;
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
		// If an items durability is set to -1, it means it doesnt have durability
		
		if (CurrentItem.durability == 0) {
			if (!isEmpty) {
				items.Pop().Use();
				stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
				if (isEmpty) {
					ChangeSprite(slotEmpty, slotHighlighted);
					Backpack.EmptySlot++;
				}
			}
		} else {
			CurrentItem.Use();
			Debug.Log(CurrentItem.durability);
			CurrentItem.durability--;
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

	public void popup(){
		if(!isEmpty){
			tooltip = Instantiate(Resources.Load<GameObject>("Tooltip"));
			tooltip.name = "tooltip";
			string name = CurrentItem.itemName;
			int length = name.Length;
			length = length * 10;
			RectTransform tooltipRect = tooltip.GetComponent<RectTransform> ();
			tooltipRect.sizeDelta = new Vector2(length, tooltipRect.sizeDelta.y);
			Text tooltipText = tooltip.GetComponentInChildren<Text> ();
			tooltipText.text = CurrentItem.itemName;
			RectTransform tooltipTextRect = tooltipText.gameObject.GetComponent<RectTransform> ();
			tooltipTextRect.sizeDelta = new Vector2 (length, tooltipTextRect.sizeDelta.y );
			tooltip.transform.SetParent(GameObject.Find("Canvas").transform, false);
			tooltip.transform.position = this.transform.position;
			tooltip.transform.position = new Vector3(tooltip.transform.position.x + 60f ,tooltip.transform.position.y - 40f , -50f);
		}
	}


	public void popupDestroy(){
		Destroy(tooltip);
	}

	public void ClearSlot() {
		items.Clear();
		ChangeSprite(slotEmpty, slotHighlighted);
		stackTxt.text = string.Empty;
	}

}
