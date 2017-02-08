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
    public int potatoGrowTime = 30;
	public int strawberryGrowTime = 45;
	public int pineappleGrowTime = 60;
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
         if(Time.timeScale == 0)return;
		for (int i = 0; i < slotNum; i++) {
            Slot currentSlot = farmingUI.farmSlots[i].GetComponent<Slot>();

            if(!currentSlot.isEmpty) {
                if (!currentSlot.CurrentItem.isFinishedCrop) {
                    currentSlot.GetComponent<Button>().interactable = false;
                } else {
                    currentSlot.GetComponent<Button>().interactable = true;
                }
                if (currentSlot.CurrentItem.isCrop) {
                    if (currentSlot.isGrowing == false) {
                        currentSlot.growTime = currentSlot.CurrentItem.growTime;
                        currentSlot.isGrowing = true;
                    }

                    currentSlot.growTime -= Time.deltaTime;

                    if (currentSlot.growTime <= 0) {
                        currentSlot.ClearSlot();
                        Item item = Instantiate(Resources.Load<Item>(currentSlot.CurrentItem.nextItem));
                        currentSlot.AddItem(item);
                        item.transform.position = new Vector3 (0,20f,0);
                        currentSlot.isGrowing = false;
                    }
                }
            } else {
                currentSlot.GetComponent<Button>().interactable = true;
            }
        }
    }
}
