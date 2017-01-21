using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour {

	public GameObject plusImage;
	public GameObject equalsImage;
	public GameObject slotPrefab;
	private Backpack BackPack;
	private RectTransform CraftTableRect;
	public Canvas canvas;
	private Backpack backpack;
	private HandleCanvas prefabGrab;

	private GameObject plus;
	private GameObject equal;
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
		
		plus = (GameObject)Instantiate(plusImage);
		equal = (GameObject)Instantiate(equalsImage);
		Slot1 = (GameObject)Instantiate(slotPrefab);
		Slot2 = (GameObject)Instantiate(slotPrefab);
		Slot3 = (GameObject)Instantiate(slotPrefab);

		RectTransform slotRect1 = Slot1.GetComponent<RectTransform>();
		RectTransform slotRect2 = Slot2.GetComponent<RectTransform>();
		RectTransform slotRect3 = Slot3.GetComponent<RectTransform>();
		RectTransform plusRect = plus.GetComponent<RectTransform>();
		RectTransform equalRect = equal.GetComponent<RectTransform>();
		CraftTableRect = GetComponent<RectTransform>();

		Slot1.name = "Slot1";
		Slot2.name = "Slot2";
		Slot3.name = "Slot3";
		plus.name = "PlusImage";
		equal.name = "EqualImage";

		//Slot1.GetComponent<Button>().interactable = false;
		Slot1.transform.SetParent(this.transform.parent);
		Slot2.transform.SetParent(this.transform.parent);
		Slot3.transform.SetParent(this.transform.parent);
		plus.transform.SetParent(this.transform.parent);
		equal.transform.SetParent(this.transform.parent);

		//Debug.Log(CraftTableRect.localPosition);

		//places the slots in the inventory in each column, then row
		slotRect1.localPosition = CraftTableRect.localPosition + new Vector3(10, -20);
		slotRect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
		slotRect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

		slotRect2.localPosition = CraftTableRect.localPosition + new Vector3(90, -20);			
		slotRect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
		slotRect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

		slotRect3.localPosition = CraftTableRect.localPosition + new Vector3(170, -20);		
		slotRect3.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
		slotRect3.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

		plusRect.localPosition = CraftTableRect.localPosition + new Vector3(65, -33);		
		plusRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
		plusRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);

		equalRect.localPosition = CraftTableRect.localPosition + new Vector3(145, -33);		
		equalRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
		equalRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);

	}

	
	public void CraftItem() {
		Slot tmp1 = Slot1.GetComponent<Slot>();
		Slot tmp2 = Slot2.GetComponent<Slot>();
		Slot tmp3 = Slot3.GetComponent<Slot>();

		if(tmp1.CurrentItem.type == ItemType.STICK && tmp2.CurrentItem.type == ItemType.VINE) {
			//backpack.AddItem(tmp3.GetComponent<Item>());
			Debug.Log("Can craft fishing rod, just need to find out how to code the cunt");
			backpack.AddItem(prefabGrab.GetComponent("itemPrefab3").GetComponent<Item>());
		}
	}
}
