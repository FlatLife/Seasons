using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Canvas canvas;
    public int slotNum;
    public CookingUI cookingUI;

    int frameIndex;
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
        frameIndex = 0;
        timeSinceLastFrame = 0;
    }

    // Update is called once per frame
    void Update () {
        if(timeSinceLastFrame > 0.15){
            animRenderer.sprite = animSprites[frameIndex];
            timeSinceLastFrame = 0;
            frameIndex++;
        } else{
            timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
        }		    
        //If we are at the last animation frame, reset it back to the first frame
        if(frameIndex > 4){
            frameIndex = 0;
        }

        for (int i = 0; i < slotNum; i++) {
            Slot currentSlot = cookingUI.cookSlots[i].GetComponent<Slot>();
            if(currentSlot.Items.Count > 0 && currentSlot.CurrentItem.isFood) {
                currentSlot.cookTime -= Time.deltaTime;
                if (currentSlot.cookTime <= 0) {
                    cookFood(currentSlot, ItemType.RAWFISH, "cookedFish");
                    cookFood(currentSlot, ItemType.COOKEDFISH, "burntFish");
                }
            } else {
                currentSlot.cookTime = 2;
            }
        }
    }

    void cookFood(Slot slot, ItemType itemType, string prefabName) {
        if (slot.CurrentItem.type == itemType && slot.cookTime <= 0) {
            slot.ClearSlot();
            slot.AddItem(Resources.Load<Item>(prefabName));
            slot.cookTime = 2;
        }
    }
}