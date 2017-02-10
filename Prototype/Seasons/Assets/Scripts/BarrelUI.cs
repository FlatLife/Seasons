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

	public void Initialize() {
		WaterBar = GameObject.Find("WaterBar");
		WaterMask = GameObject.Find("WaterBar/Mask");
		WaterBarRect = WaterBar.GetComponent<RectTransform>();
		WaterMaskRect = WaterMask.GetComponent<RectTransform>();
		
		RectTransform uiRect = this.GetComponent<RectTransform>();
		uiRect.sizeDelta = new Vector3(200, 300, 0);
		uiRect.position = new Vector3(600,400);
		WaterBar.transform.SetParent(this.transform);
		WaterMask.transform.SetParent(this.WaterBar.transform);
		WaterBarRect.localPosition = new Vector3(uiRect.rect.width/2 - WaterBarRect.rect.width/2, -slotSize*1.8f);
		WaterMaskRect.localPosition = new Vector3(7, -8);
		

		bucketEmptySlot = Instantiate(slotPrefab);
		bucketEmptySlot.name = "FreshWaterSlot";
		bucketFillSlot = Instantiate(slotPrefab);
		bucketFillSlot.name = "EmptySlot";

		RectTransform bucketEmptyRect = bucketEmptySlot.GetComponent<RectTransform>();
		bucketEmptySlot.transform.SetParent(this.transform);
		bucketEmptyRect.localPosition = new Vector3(48, -20);
		bucketEmptyRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		bucketEmptyRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		RectTransform bucketFillRect = bucketFillSlot.GetComponent<RectTransform>();
		bucketFillSlot.transform.SetParent(this.transform);
		bucketFillRect.localPosition = new Vector3(115, -20);
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
		bucketFillSlot.GetComponent<Slot>().initial = state;
		button2.spriteState = state;
		button2.spriteState = state2;

		info = Instantiate(Resources.Load<GameObject>("Info"));
		info.transform.SetParent(this.transform);
		info.GetComponent<InformationPopup>().info = "Use this UI to store your buckets of water.\n Place your filled bucket in the left slot to fill\n up the barrel, or place your empty bucket\n in the right slot to get water back out\n of the barrel.";
		info.GetComponent<InformationPopup>().width = 300;
		info.GetComponent<InformationPopup>().height = 130;
		info.GetComponent<RectTransform>().localPosition = new Vector3(uiRect.rect.width - 25, -22);
		info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
		info.GetComponent<Image>().enabled = false;
	}
}
