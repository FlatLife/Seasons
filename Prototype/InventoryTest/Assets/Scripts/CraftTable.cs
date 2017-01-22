using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour {
	public GameObject slotPrefab;
	public float slotSize;

	private Backpack BackPack;
	private RectTransform CraftTableRect;
	private GameObject Slot1;
	private GameObject Slot2;
	private GameObject Slot3;



	// Use this for initialization
	void Start () {
		CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateLayout() {
		Slot1 = (GameObject)Instantiate(slotPrefab);
		Slot2 = (GameObject)Instantiate(slotPrefab);
		Slot3 = (GameObject)Instantiate(slotPrefab);

		RectTransform slotRect1 = Slot1.GetComponent<RectTransform>();
		RectTransform slotRect2 = Slot2.GetComponent<RectTransform>();
		RectTransform slotRect3 = Slot3.GetComponent<RectTransform>();
		CraftTableRect = GetComponent<RectTransform>();

		Slot1.name = "Slot1";
		Slot2.name = "Slot2";
		Slot3.name = "Slot3";

		//Slot1.GetComponent<Button>().interactable = false;
		Slot1.transform.SetParent(this.transform.parent);
		Slot2.transform.SetParent(this.transform.parent);
		Slot3.transform.SetParent(this.transform.parent);

		//Debug.Log(CraftTableRect.localPosition);

		//places the slots in the inventory in each column, then row
		slotRect1.localPosition = CraftTableRect.localPosition + new Vector3(10, -20);
		slotRect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		slotRect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		slotRect2.localPosition = CraftTableRect.localPosition + new Vector3(90, -20);			
		slotRect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		slotRect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

		slotRect3.localPosition = CraftTableRect.localPosition + new Vector3(170, -20);		
		slotRect3.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
		slotRect3.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

	}

	
	public void CraftItem() {
		// tmp1/2/3 are just the crafting slots
		Slot tmp1 = Slot1.GetComponent<Slot>();
		Slot tmp2 = Slot2.GetComponent<Slot>();
		Slot tmp3 = Slot3.GetComponent<Slot>();

		//compares items in slots (has to be specific), atm just called find everytime, so probably shud do that in instanatiate
		// on start up or whatever to not slow it down but yolo
		if(tmp1.CurrentItem.type == ItemType.STICK && tmp2.CurrentItem.type == ItemType.VINE) {
			tmp3.AddItem(GameObject.Find("Items/FishingRod").GetComponent<Item>());
			tmp1.UseItem ();
			tmp2.UseItem ();
		}
	}
}
