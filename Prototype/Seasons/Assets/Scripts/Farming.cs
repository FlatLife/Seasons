using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farming : MonoBehaviour {

    public ItemType type;
	public Canvas canvas;
    public int slotNum;
    int emptySlots;
    public FarmingUI farmingUI;

    public Sprite[] animSprites;
	SpriteRenderer animRenderer;
    int state = 0;

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
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < slotNum; i++) {
            Slot currentSlot = farmingUI.farmSlots[i].GetComponent<Slot>();

            if(!currentSlot.isEmpty) {
                if (!currentSlot.CurrentItem.isFinishedCrop) {
                    currentSlot.GetComponent<Button>().interactable = false;
                } else {
                    if(state < 3){
                        state = 3;
                    }
                    currentSlot.GetComponent<Button>().interactable = true;
                }
                if (currentSlot.CurrentItem.isCrop) {
                    //seedling sprite
                    if(state < 1){
                        state = 1;
                    }      
                    if (currentSlot.isGrowing == false) {
                        currentSlot.growTime = currentSlot.CurrentItem.growTime;
                        currentSlot.isGrowing = true;
                   
                    }
                    //if the crop is half grown change it to the half grown sprite
                    if((currentSlot.CurrentItem.type == ItemType.CARROTSEED) && (currentSlot.CurrentItem.growTime < carrotGrowTime/2f)){
                        if(state < 2){
                            state = 2; 
                        }          
                        
                    }
                    if(currentSlot.CurrentItem.type == ItemType.POTATOSEED && currentSlot.CurrentItem.growTime < potatoGrowTime/2f){
                        if(state < 2){
                            state = 2;
                        } 
                    }
                    if(currentSlot.CurrentItem.type == ItemType.STRAWBERRYSEED && currentSlot.CurrentItem.growTime < strawberryGrowTime/2f){
                        if(state < 2){
                            state = 2;
                        } 
                    }
                    if(currentSlot.CurrentItem.type == ItemType.PINEAPPLESEED && currentSlot.CurrentItem.growTime < pineappleGrowTime/2f){
                        if(state < 2){
                            state = 2;
                        } 
                    }

                    //reduce the grow time
                    if(GameMaster.isWinter){
                        currentSlot.growTime -= Time.deltaTime/2;
                    } else {
                        currentSlot.growTime -= Time.deltaTime;
                    }
                    
                    //if the item is fully grown
                    if (currentSlot.growTime <= 0) {
                        currentSlot.ClearSlot();
                        Item item = Instantiate(Resources.Load<Item>(currentSlot.CurrentItem.nextItem));
                        currentSlot.AddItem(item);
                        if(state < 3){
                            state = 3;
                        }
                        item.transform.position = new Vector3 (0,20f,0);
                        currentSlot.isGrowing = false;
                    }
                }
            } else {
                currentSlot.GetComponent<Button>().interactable = true;
                emptySlots++;
            }
        }
        //if all the slots were empty set it back to the original sprite
        if(emptySlots >= slotNum){       
            state = 0;
        }
        //set the sprite to the correct state
        if(animRenderer.sprite != animSprites[state]){  
            animRenderer.sprite = animSprites[state];
        }
        emptySlots = 0;
        state = 0;
    }
}
