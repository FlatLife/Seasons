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

	// Use this for initialization
	void Start () {
		CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateLayout() {
		
		GameObject plus = (GameObject)Instantiate(plusImage);
		GameObject equal = (GameObject)Instantiate(equalsImage);
		GameObject Slot1 = (GameObject)Instantiate(slotPrefab);
		GameObject Slot2 = (GameObject)Instantiate(slotPrefab);
		GameObject Slot3 = (GameObject)Instantiate(slotPrefab);

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
		slotRect1.localPosition = CraftTableRect.localPosition + new Vector3(10, -50);
		slotRect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
		slotRect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

		slotRect2.localPosition = CraftTableRect.localPosition + new Vector3(90, -50);			
		slotRect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
		slotRect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

		slotRect3.localPosition = CraftTableRect.localPosition + new Vector3(170, -50);		
		slotRect3.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
		slotRect3.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

		plusRect.localPosition = CraftTableRect.localPosition + new Vector3(65, -63);		
		plusRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
		plusRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);

		equalRect.localPosition = CraftTableRect.localPosition + new Vector3(145, -63);		
		equalRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
		equalRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);

		//BackPack.allSlots.Add(Slot1);
		//BackPack.allSlots.Add(Slot2);
		//BackPack.allSlots.Add(Slot3);
	}

	public void CraftItem() {
		
	}
}
