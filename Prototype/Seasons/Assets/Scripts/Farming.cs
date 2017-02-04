using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farming : MonoBehaviour {

	public Canvas canvas;
    public int slotNum;
    public FarmingUI farmingUI;
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

            if(currentSlot.Items.Count > 0) {
                // this chunk of code is to ensure the player can only remove the crop once it's finished growing
                if (!currentSlot.CurrentItem.isFinishedCrop) {
                    currentSlot.GetComponent<Button>().interactable = false;
                } else {
                    currentSlot.GetComponent<Button>().interactable = true;
                }
                // checks that our item in the slot is a crop and not a random item before we make it grow
                if (currentSlot.CurrentItem.isCrop) {
					currentSlot.growTime -= !farmingUI.waterSlot.isEmpty && farmingUI.waterSlot.CurrentItem.type == ItemType.FRESHWATER ? (Time.deltaTime)*10 : Time.deltaTime;
                    if (currentSlot.growTime <= 0) {
                        growFood(currentSlot, ItemType.SEED, "Carrot");  
						growFood(currentSlot, ItemType.CARROT, "Wood");
                    }
                }
            } else {
                currentSlot.growTime = 5;
            }
        }
    }


	public void growFood(Slot slot, ItemType itemType, string prefabName) {
        if (slot.CurrentItem.type == itemType && slot.growTime <= 0) {
            slot.ClearSlot();
            slot.AddItem(Resources.Load<Item>(prefabName));
            slot.growTime = 5;
        }
	}
}
