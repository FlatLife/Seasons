using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour {
	public GameObject slotPrefab;
	public float slotSize;

	private Backpack BackPack;
	private RectTransform CraftTableRect;
	public GameObject Slot1;
	public GameObject Slot2;
	public GameObject Slot3;



	// Use this for initialization
	void Awake () {
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
			
			Slot1.GetComponent<Image>().enabled = false;
			Slot2.GetComponent<Image>().enabled = false;
			Slot3.GetComponent<Image>().enabled = false;
		
	}
	
	public void CraftItem() {
		CheckRecipe(ItemType.VINE, ItemType.STICK, "FishingRod");
    }
	public void CheckRecipe(ItemType type1, ItemType type2, string product) {
        Slot tmp1 = Slot1.GetComponent<Slot>();
        Slot tmp2 = Slot2.GetComponent<Slot>();
        Slot tmp3 = Slot3.GetComponent<Slot>();

		if((tmp1.CurrentItem.type == type1 && tmp2.CurrentItem.type == type2)
        || (tmp1.CurrentItem.type == type2 && tmp2.CurrentItem.type == type1)){
            Item result = Resources.Load<GameObject>(product).GetComponent<Item>();
            tmp3.AddItem(result);
            tmp1.UseItem ();
            tmp2.UseItem ();
        }
	}
}
