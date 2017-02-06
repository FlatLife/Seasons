using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farming : MonoBehaviour {

    public ItemType type;
	public Canvas canvas;
    public int slotNum;
    public FarmingUI farmingUI;

    public int carrotGrowTime = 15;
    public int potatoGrowTime = 45;
	public int strawberryGrowTime = 30;
	public int pineappleGrowTime = 6;
	// Use this for initialization

	void Awake () {
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		farmingUI = Instantiate(Resources.Load<FarmingUI>("FarmingUI"));
        farmingUI.name = "FarmingUI";
        farmingUI.Initialize(slotNum);
        farmingUI.transform.SetParent(canvas.transform);
	}
	
	// Update is called once per frame
	void Update () {
        // Cycles through each slot in our array of farming slots
		for (int i = 0; i < slotNum; i++) {
            Slot currentSlot = farmingUI.farmSlots[i].GetComponent<Slot>();

            if(!currentSlot.isEmpty) {
                // checks that our item in the slot is a crop and not a random item before we make it grow
                if (currentSlot.CurrentItem.isCrop) {
                     // this chunk of code is to ensure the player can only remove the crop once it's finished growing
                    if (!currentSlot.CurrentItem.isFinishedCrop) {
                        currentSlot.GetComponent<Button>().interactable = false;
                    } else {
                        currentSlot.GetComponent<Button>().interactable = true;
                    }
                    CheckFood(currentSlot);
					currentSlot.growTime -= !farmingUI.waterSlot.isEmpty && farmingUI.waterSlot.CurrentItem.type == ItemType.FRESHWATER ? (Time.deltaTime)*10 : Time.deltaTime;
                    if ((currentSlot.CurrentItem.type == ItemType.CARROTSEED ||
                     currentSlot.CurrentItem.type == ItemType.CARROTGROW) && currentSlot.growTime <= 0) {
                        growFood(currentSlot, ItemType.CARROTSEED, "CarrotGrow");  
						growFood(currentSlot, ItemType.CARROTGROW, "Carrot");
                    } else if ((currentSlot.CurrentItem.type == ItemType.POTATOSEED ||
                    currentSlot.CurrentItem.type == ItemType.POTATOGROW) && currentSlot.growTime <= 0) {
                        growFood(currentSlot, ItemType.POTATOSEED, "PotatoGrow");  
						growFood(currentSlot, ItemType.POTATOGROW, "Potato");
                    } else if ((currentSlot.CurrentItem.type == ItemType.PINEAPPLESEED ||
                    currentSlot.CurrentItem.type == ItemType.PINEAPPLEGROW) && currentSlot.growTime <= 0) {
                        growFood(currentSlot, ItemType.PINEAPPLESEED, "PineappleGrow");  
						growFood(currentSlot, ItemType.PINEAPPLEGROW, "Pineapple");
                    } else if ((currentSlot.CurrentItem.type == ItemType.STRAWBERRYSEED ||
                    currentSlot.CurrentItem.type == ItemType.STRAWBERRYGROW) && currentSlot.growTime <= 0) {
                        growFood(currentSlot, ItemType.STRAWBERRYSEED, "StrawberryGrow");  
						growFood(currentSlot, ItemType.STRAWBERRYGROW, "Strawberries");
                    }
                }
            } else {
                currentSlot.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void CheckFood(Slot slot) {
        if (slot.CurrentItem.type == ItemType.CARROTSEED && slot.isGrowing == false) {
            slot.growTime = carrotGrowTime;
            slot.isGrowing = true;
        } else if (slot.CurrentItem.type == ItemType.POTATOSEED && slot.isGrowing == false) {
            slot.growTime = potatoGrowTime;
            slot.isGrowing = true;
        } else if (slot.CurrentItem.type == ItemType.PINEAPPLESEED && slot.isGrowing == false) {
            slot.growTime = pineappleGrowTime;
            slot.isGrowing = true;
        } else if (slot.CurrentItem.type == ItemType.STRAWBERRYSEED && slot.isGrowing == false) {
            slot.growTime = strawberryGrowTime;
            slot.isGrowing = true;
        }
    }

	public void growFood(Slot slot, ItemType itemType, string prefabName) {
        if (slot.CurrentItem.type == itemType && slot.growTime <= 0) {
            slot.ClearSlot();
            slot.AddItem(Resources.Load<Item>(prefabName));
            slot.growTime = carrotGrowTime;
            slot.isGrowing = false;
            slot.GetComponent<Button>().interactable = true;
        }
	}
}
