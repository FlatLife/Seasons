using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPurifierUI : MonoBehaviour {

	public Slot slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform WaterUIRect;

	public GameObject info;
	public Slot Slot1;
	public Slot Slot2;
	public Text text;



	public void Initialize() {		
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(228, 123, 0);
		uiRect.position = new Vector3(525,494);

		text = Instantiate (Resources.Load<Text> ("Text"), this.transform);
		RectTransform textRect = text.GetComponent<RectTransform> ();
		textRect.anchorMin = new Vector2(0.5f, 1);
		textRect.anchorMax = new Vector2(0.5f, 1);
		textRect.pivot = new Vector2(0.5f, 0.5f);
		textRect.anchoredPosition = new Vector3 (-5, -27);
		text.text = "Water Purifier";

		Slot1 = Instantiate(slotPrefab);
		Slot1.name = "Slot1";
		RectTransform FreshwaterRect = Slot1.GetComponent<RectTransform>();
		Slot1.GetComponent<Image>().sprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		SpriteState state =  new SpriteState();
		state.highlightedSprite = Resources.Load<Sprite>("slotHighlightedBucket");
		Slot1.GetComponent<Slot>().initialSprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		Slot1.GetComponent<Slot>().initial = state;
		Button button = Slot1.GetComponent<Button>();
		button.spriteState = state;
		Slot1.transform.SetParent(this.transform);
		FreshwaterRect.localPosition = new Vector3(60, -40);
		FreshwaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		FreshwaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		Slot1.GetComponent<Image>().enabled = false;

		Slot2 = Instantiate(slotPrefab);
		Slot2.name = "Slot2";
		RectTransform SaltWaterRect = Slot2.GetComponent<RectTransform>();
		Slot2.GetComponent<Image>().sprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		state.highlightedSprite = Resources.Load<Sprite>("slotHighlightedBucket");
		Slot2.GetComponent<Slot>().initialSprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		Slot2.GetComponent<Slot>().initial = state;
		Slot2.GetComponent<Button>().spriteState = state;
		Slot2.transform.SetParent(this.transform);
		SaltWaterRect.localPosition = new Vector3(130, -40);
		SaltWaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		SaltWaterRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
		Slot2.GetComponent<Image>().enabled = false;

		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "Use this UI to purify salt water into fresh water. \nPlace an empty bucket in one slot and a bucket of salt water in the other then wait for the process to complete";
		info.GetComponent<InformationPopup>().width = 300;
		info.GetComponent<InformationPopup>().height = 100;
		info.GetComponent<RectTransform>().localPosition = new Vector3(200, -25);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
		info.GetComponent<Image>().enabled = false;

	}
}
