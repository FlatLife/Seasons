using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour {

	public GameObject slotPrefab;
	public GameObject info;
	public Text text;
	public float slotSize;
	public GameObject[] cookSlots;

	public void Initialize(int slotCount) {
		cookSlots = new GameObject[slotCount];
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(280, 150);
		uiRect.position = new Vector3(510,330);

		text = Instantiate (Resources.Load<Text> ("Text"), this.transform);
		RectTransform textRect = text.GetComponent<RectTransform> ();
		textRect.anchorMin = new Vector2(0.5f, 1);
		textRect.anchorMax = new Vector2(0.5f, 1);
		textRect.pivot = new Vector2(0.5f, 0.5f);
		textRect.anchoredPosition = new Vector3 (-5, -27);
		text.text = "Cooking";

		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "Use this UI to cook your food. \n Place items you wish to cook in the three slots at the top.\n Use the bottom slot to put wood on the fire and keep the fire going.";
		info.GetComponent<InformationPopup>().width = 300;
		info.GetComponent<InformationPopup>().height = 100;
		info.GetComponent<RectTransform>().localPosition = new Vector3(250	, -25);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
		info.GetComponent<Image>().enabled = false;
		for (int i = 0; i < slotCount; i++) {
			GameObject cookSlot = (GameObject)Instantiate(slotPrefab);
			RectTransform slotRect = cookSlot.GetComponent<RectTransform>();
			cookSlot.name = "Cook Slot " + i;
			cookSlot.transform.SetParent(this.transform);
			if(i < 3){
				slotRect.localPosition = new Vector3(60 * (i+1), -40);
			}else{
				slotRect.localPosition = new Vector3(120, -90);
				cookSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("slotUnhighlightedWood");
				SpriteState state =  new SpriteState();
				state.highlightedSprite = Resources.Load<Sprite>("slotHighlightedWood");
				cookSlot.GetComponent<Slot>().initialSprite = Resources.Load<Sprite>("slotUnhighlightedWood");
				cookSlot.GetComponent<Slot>().initial = state;
				cookSlot.GetComponent<Button>().spriteState = state;
			}
			slotRect.sizeDelta = new Vector3(slotSize, slotSize);
			cookSlot.GetComponent<Image>().enabled = false;
			cookSlots[i] = cookSlot;

			
		}
	}
}
