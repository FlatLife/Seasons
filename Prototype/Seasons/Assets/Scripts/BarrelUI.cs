using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelUI : MonoBehaviour {

	public GameObject info;
	public Slot slotPrefab;
	public float slotSize;
	private Backpack BackPack;
	private RectTransform barrelUIRect;
	public Slot bucketEmptySlot;
	public Slot bucketFillSlot;
	public GameObject WaterBar;
	public GameObject WaterMask;
	public RectTransform WaterBarRect;
	public RectTransform WaterMaskRect;
	public Stat water;
	public Text text;

	public void Initialize() {

		
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(200, 350);
		uiRect.anchorMin = new Vector2(0, 0.5f);
		uiRect.anchorMax = new Vector2(0, 0.5f);
		uiRect.pivot = new Vector2(0, 1);
		uiRect.anchoredPosition = new Vector3(520, 495);

		WaterBar = Instantiate(Resources.Load<GameObject>("WaterBar"));
		WaterMask = WaterBar.transform.Find("Mask").gameObject;
		WaterBar.transform.SetParent(this.transform);
		WaterMask.transform.SetParent(this.WaterBar.transform);
		WaterBarRect = WaterBar.GetComponent<RectTransform>();
		WaterMaskRect = WaterMask.GetComponent<RectTransform>();
		WaterBarRect.anchoredPosition = new Vector3(45, -113);
		WaterMaskRect.localPosition = new Vector3(7, -8);

		water = new Stat();
		water.bar = WaterBar.GetComponent<BarScript>();
		water.currentVal = 0;
		water.Initialize();


		text = Instantiate (Resources.Load<Text> ("Text"), this.transform);
		RectTransform textRect = text.GetComponent<RectTransform> ();
		textRect.anchorMin = new Vector2(0.5f, 1);
		textRect.anchorMax = new Vector2(0.5f, 1);
		textRect.pivot = new Vector2(0.5f, 0.5f);
		textRect.anchoredPosition = new Vector3 (-5, -27);
		text.text = "Water Storage";

		bucketEmptySlot = Instantiate(slotPrefab);
		bucketEmptySlot.name = "FreshWaterSlot";
		bucketFillSlot = Instantiate(slotPrefab);
		bucketFillSlot.name = "EmptySlot";

		RectTransform bucketEmptyRect = bucketEmptySlot.GetComponent<RectTransform>();
		bucketEmptySlot.transform.SetParent(this.transform);
		bucketEmptyRect.localPosition = new Vector3(48, -50);
		bucketEmptyRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		bucketEmptyRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		RectTransform bucketFillRect = bucketFillSlot.GetComponent<RectTransform>();
		bucketFillSlot.transform.SetParent(this.transform);
		bucketFillRect.localPosition = new Vector3(115, -50);
		bucketFillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		bucketFillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		bucketEmptySlot.GetComponent<Image>().enabled = false;
		bucketFillSlot.GetComponent<Image>().enabled = false;
		WaterMask.SetActive(false);

		bucketEmptySlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		SpriteState state =  new SpriteState();
		state.highlightedSprite = Resources.Load<Sprite>("slotHighlightedBucket");
		Button button = bucketEmptySlot.GetComponent<Button>();
		bucketEmptySlot.GetComponent<Slot>().initialSprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		bucketEmptySlot.GetComponent<Slot>().initial = state;
		button.spriteState = state;
		button.spriteState = state;

		bucketFillSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		SpriteState state2 =  new SpriteState();
		state2.highlightedSprite = Resources.Load<Sprite>("slotHighlightedBucket");
		Button button2 = bucketFillSlot.GetComponent<Button>();
		bucketFillSlot.GetComponent<Slot>().initialSprite = Resources.Load<Sprite>("slotUnhighlightedBucket");
		bucketFillSlot.GetComponent<Slot>().initial = state2;
		button2.spriteState = state2;

		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "Use this UI to store your buckets of water.\n Place your filled bucket in the left slot to fill up the barrel,\n or \nPlace your empty bucket in the right slot to get water back out of the barrel.";
		info.GetComponent<InformationPopup>().width = 300;
		info.GetComponent<InformationPopup>().height = 130;
		info.GetComponent<RectTransform>().localPosition = new Vector3(uiRect.rect.width - 25, -50);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
		info.GetComponent<Image>().enabled = false;
	}
}
