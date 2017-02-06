using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Canvas canvas;
    public int slotNum;
    public CookingUI cookingUI;
    float burnTime = 50;
    GameObject fireSpark;

    //burnState = 1(small), 2(medium), 3(large)
    public int fireState;
    public int lastFrame;
    public float animationTime;


    public int frameIndex;
    // An array with the sprites used for animation
    public Sprite[] animSprites;

    // Reference to the renderer of the sprite
    // game object
    SpriteRenderer animRenderer;

    private float timeSinceLastFrame;    

    // Use this for initialization
    void Awake () {
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        cookingUI = Instantiate(Resources.Load<CookingUI>("CookingUI"));
        cookingUI.name = "CookingUI";
        cookingUI.Initialize(slotNum);
        cookingUI.transform.SetParent(canvas.transform);
        // Get a reference to game object renderer and
        // cast it to a Sprite Renderer
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
        //Sets the animation to the first frame
        timeSinceLastFrame = 0;
        fireState = -1;
    }

    // Update is called once per frame
    void Update () {
        //if the fire is not dead
        if(fireState > 0){
            //changing state of the fire as time goes on
            burnTime -= timeSinceLastFrame;
            if(burnTime < 0){
                //decrease fire state
                changeState(fireState--, 0);          
            }
            //if its time to change the frame
            if(timeSinceLastFrame > animationTime){
                animRenderer.sprite = animSprites[frameIndex];
                timeSinceLastFrame = 0;
                frameIndex++;
            } else{
                timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
            }
            //if we are at the last animation frame, reset it back to the first frame
            if(frameIndex > lastFrame){
                frameIndex = lastFrame - 4;
            }
            
            for (int i = 0; i < slotNum; i++) {
            Slot currentSlot = cookingUI.cookSlots[i].GetComponent<Slot>();
            //if checking the log slot
            if(i == slotNum - 1){
                if(!currentSlot.isEmpty && currentSlot.CurrentItem.type == ItemType.STICK && fireState < 3){
                    currentSlot.UseItem();
                    changeState(fireState++, 1);
                }
            } else {
                //check food slots               
                if(!currentSlot.isEmpty) {
                    if (currentSlot.CurrentItem.isFood) {
                        currentSlot.cookTime -= Time.deltaTime;
                        if (currentSlot.cookTime <= 0) {
                            cookFood(currentSlot, ItemType.RAWFISH, "cookedFish");
                            cookFood(currentSlot, ItemType.COOKEDFISH, "burntFish");
                            
                        }
                    }
                } else {
                    currentSlot.cookTime = 2;
                }
            }
        }
        //if fire is dead, display deadFire sprite    
        } else {
            animRenderer.sprite = animSprites[15];
        }		    

        
    }

    void cookFood(Slot slot, ItemType itemType, string prefabName) {
        if (slot.CurrentItem.type == itemType && slot.cookTime <= 0) {
            slot.ClearSlot();
            slot.AddItem(Resources.Load<Item>(prefabName));
            slot.cookTime = 2;
        }
    }

    void changeState(int state, int increaseOrDecrease){
        burnTime = 50;
        //increase means to add fuel to the fire, causing the lastFrame to be increased
        if(increaseOrDecrease == 1){
            lastFrame += 5;
        } else{
            lastFrame -= 5;
        }
        if(state == 1){
            fireState = -1;
        }
        frameIndex = (state - 1) * 5; 
    }

    public void startFire(){
        fireState = 3;
        burnTime = 50;
        lastFrame = 14;
        frameIndex = 10;
    }

    public void sparkFire(){
        fireSpark = Instantiate(Resources.Load<GameObject>("sparkFire"));
        fireSpark.transform.position = transform.position;
    }

}