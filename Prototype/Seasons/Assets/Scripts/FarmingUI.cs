using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmingUI : MonoBehaviour {

	public Slot slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform FarmingUIRect;
	public Slot[] farmSlots;
	public Text text;
	public GameObject info;
	public Slot waterSlot;
	private RectTransform backpackRef;

	
	public void Initialize(int slotCount) {
		backpackRef = GameObject.Find("Backpack").GetComponent<RectTransform>();
		farmSlots = new Slot[slotCount];
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3((slotSize * slotCount + slotSize) , (slotSize + slotSize/2) * 2, 0);
		uiRect.position = new Vector3(backpackRef.rect.x + backpackRef.rect.width + 20, -backpackRef.rect.y + backpackRef.rect.height + 100);
		for (int i = 0; i < slotCount; i++) {
			Slot farmSlot = Instantiate(slotPrefab);
			RectTransform slotRect = farmSlot.GetComponent<RectTransform>();
			farmSlot.name = "Farm Slot " + i;
			farmSlot.transform.SetParent(this.transform);
			if (i < 4) {
				slotRect.localPosition = new Vector3((slotSize/(slotCount) + (slotSize/(slotCount+1) + slotSize*2) * i) + 30, -slotSize/4.8f);
			} else {
				slotRect.localPosition = new Vector3((slotSize/(slotCount) + (slotSize/(slotCount+1) + slotSize*2) * (i-4)) + 30, -slotSize * 1.6f);
			}
			slotRect.sizeDelta = new Vector3(slotSize, slotSize);
			farmSlot.GetComponent<Image>().enabled = false;
			farmSlots[i] = farmSlot;

		}

		text = Instantiate (Resources.Load<Text> ("Text"), this.transform);
		RectTransform textRect = text.GetComponent<RectTransform> ();
		textRect.anchorMin = new Vector2(0.5f, 1);
		textRect.anchorMax = new Vector2(0.5f, 1);
		textRect.pivot = new Vector2(0.5f, 0.5f);
		textRect.anchoredPosition = new Vector3 (-5, 27);
		text.text = "Farm";

		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "Use this UI to grow your crops. \n Place seeds in any of the 8 slots then wait for them to grow.\n Use the slot on the right to water crops and speed up growth.";
		info.GetComponent<InformationPopup>().width = 300;
		info.GetComponent<InformationPopup>().height = 100;
		info.GetComponent<RectTransform>().localPosition = new Vector3(428	, -40);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
		info.GetComponent<Image>().enabled = false;

		waterSlot = Instantiate(slotPrefab);
		waterSlot.name = "WaterSlot";
		RectTransform waterRect = waterSlot.GetComponent<RectTransform>();
		waterSlot.transform.SetParent(this.transform);
		//waterRect.localPosition = new Vector3(uiRect.localPosition.x + uiRect.rect.width ,uiRect.localPosition.y);
		waterRect.localPosition = new Vector3(uiRect.rect.width+(slotSize/2),-(uiRect.rect.height)/2+(slotSize/2));
		waterRect.sizeDelta = new Vector3(slotSize, slotSize);	
		waterSlot.GetComponent<Image>().enabled = false;
		waterSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		SpriteState state =  new SpriteState();
		state.highlightedSprite = Resources.Load<Sprite>("slotHighlightedBucket");
		waterSlot.GetComponent<Slot>().initialSprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		waterSlot.GetComponent<Slot>().initial = state;
		Button button = waterSlot.GetComponent<Button>();
		button.spriteState = state;
	}
}
