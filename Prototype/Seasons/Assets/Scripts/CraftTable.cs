using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour {
	public GameObject slotPrefab;
	public float slotSize;

	public Backpack BackPack;
	//private RectTransform CraftTableRect;
	public GameObject Slot1;
	public GameObject Slot2;
	public GameObject info;

	private Notification notification;


	// Use this for initialization
	void Awake () {
		CreateLayout();
		notification = GameObject.Find("Notification").GetComponent<Notification>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateLayout() {

			Slot1 = (GameObject)Instantiate(slotPrefab);
			Slot2 = (GameObject)Instantiate(slotPrefab);


			RectTransform slotRect1 = Slot1.GetComponent<RectTransform>();
			RectTransform slotRect2 = Slot2.GetComponent<RectTransform>();

			//CraftTableRect = GetComponent<RectTransform>();

			Slot1.name = "Slot1";
			Slot2.name = "Slot2";
	

			//Slot1.GetComponent<Button>().interactable = false;
			Slot1.transform.SetParent(this.transform);
			Slot2.transform.SetParent(this.transform);


			//Debug.Log(CraftTableRect.localPosition);

			//places the slots in the inventory in each column, then row
			slotRect1.localPosition = new Vector3(60, -50);
			slotRect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
			slotRect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
			

			slotRect2.localPosition = new Vector3(130, -50);			
			slotRect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
			slotRect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

			info = Instantiate(Resources.Load<GameObject>("Info"));
			info.transform.SetParent(this.transform);
			info.GetComponent<InformationPopup>().info = "Use this UI to create new Items. \nPlace an item in each slot to craft a new item if it matches a recipe.\n Note: some recipes only need one slot with an item in it.";
			info.GetComponent<InformationPopup>().width = 300;
			info.GetComponent<InformationPopup>().height = 100;
			info.GetComponent<RectTransform>().localPosition = new Vector3(200, -25);
			info.GetComponent<RectTransform>().sizeDelta = new Vector3(25,25);
			

			
			info.GetComponent<Image>().enabled = false;
			Slot1.GetComponent<Image>().enabled = false;
			Slot2.GetComponent<Image>().enabled = false;
	
		
	}
	
	public void CraftItem() {
		CheckRecipeOneItem(ItemType.WOOD, "Stick", 2);
		CheckRecipe(ItemType.VINE, ItemType.STICK, "FishingRod");
		CheckRecipe(ItemType.STICK, ItemType.WOOD, "Hoe");
		CheckRecipe(ItemType.STICK, ItemType.ROCK, "Hatchet");
		// CheckRecipe needs to check if item should be returned (in this case, the hatchet)
		CheckRecipe(ItemType.HATCHET, ItemType.WOOD, "Bucket");
		CheckRecipe(ItemType.STICK, ItemType.STICK, "FirePrep");
		CheckRecipe(ItemType.SEAWEED, ItemType.ROCK, "WaterPurifierItem");
		CheckRecipe(ItemType.WOOD, ItemType.WOOD, "BarrelItem");
		CheckRecipe(ItemType.RAWSALMON, ItemType.SEAWEED, "Sushi");

		//Other items that are made WITHOUT using CheckRecipe (usually put on something like a fire)
		//Fresh Water = Seawater put on purifier (purifiers instantiated on the ground)
		//Cooked/Burnt fish = Raw fish put on fire (fire instantiated on the ground)
		//Lit Torch = Wood put on fire (fire instantiated on the ground)
		
    }
	public void CheckRecipe(ItemType type1, ItemType type2, string product) {
        Slot tmp1 = Slot1.GetComponent<Slot>();
        Slot tmp2 = Slot2.GetComponent<Slot>();

		if(!tmp1.isEmpty && !tmp2.isEmpty) {
			if((tmp1.CurrentItem.type == type1 && tmp2.CurrentItem.type == type2)
       		|| (tmp1.CurrentItem.type == type2 && tmp2.CurrentItem.type == type1)){
				Item result = Resources.Load<GameObject>(product).GetComponent<Item>();
				if(BackPack.EmptySlots() >= 1){
            		BackPack.AddItem(result);
					if(!tmp1.CurrentItem.keepItem){
           				tmp1.DestroyItem ();
					} else {
						CheckDurability(tmp1);
					}
					if(!tmp2.CurrentItem.keepItem){
						tmp2.DestroyItem ();
					} else {
						CheckDurability(tmp2);
					}
				}else{
					notification.InventoryFlag = true;
				}
			}
		} 
	}

	public void CheckDurability(Slot slot) {
		Item item = slot.CurrentItem;
		if(item.Durability != 0) {
			if(item.type == ItemType.HATCHET) {
				item.Durability--;
			}
			if (item.Durability <= 0) {
				slot.DestroyItem();
				Debug.Log("Your axe broke!");
			}
		}
	}

	public void CheckRecipeOneItem(ItemType type, string product, int quantity) {
		Slot tmp1 = Slot1.GetComponent<Slot>();
        Slot tmp2 = Slot2.GetComponent<Slot>();

		// Checking if only one slot is empty (stops peeking on null error)
		if ((tmp1.isEmpty && !tmp2.isEmpty) || (!tmp1.isEmpty && tmp2.isEmpty)) {
			// Checks what slot is empty, so we know what slot to compare with (avoid null error)
			if (tmp1.isEmpty) {
				if (tmp2.CurrentItem.type == type) {
					Item result = Resources.Load<GameObject>(product).GetComponent<Item>();
					if(BackPack.EmptySlots() >= quantity){
						for(int i = 0; i < quantity; i++){
            				BackPack.AddItem(result);
						}
						if(!tmp2.CurrentItem.keepItem){
							tmp2.DestroyItem();
						}
					}else{
						notification.InventoryFlag = true;
					}
				}
			} else if (tmp2.isEmpty){
				if (tmp1.CurrentItem.type == type) {
					Item result = Resources.Load<GameObject>(product).GetComponent<Item>();
            		if(BackPack.EmptySlots() >= quantity){
						for(int i = 0; i < quantity; i++){
            				BackPack.AddItem(result);
						}
						if(!tmp1.CurrentItem.keepItem){
							tmp1.DestroyItem();
						}
					}else{
						notification.InventoryFlag = true;
					}
				}
			}
		}
	}
}
