using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Transform itemPrefab;
    public Canvas canvas;
    public int slotNum;
    private CookingUI cookingUI;

    int frameIndex;
    // An array with the sprites used for animation
    public Sprite[] animSprites;

    // Reference to the renderer of the sprite
    // game object
    SpriteRenderer animRenderer;

    private float timeSinceLastFrame;    

    // Use this for initialization
    void Start () {
        cookingUI = Instantiate(Resources.Load<CookingUI>("CookingUI"));
        cookingUI.name = "CookingUI";
        cookingUI.Initialize(slotNum);
        cookingUI.transform.SetParent(canvas.transform);
        GameObject.Find("Player").GetComponent<Player>().cookingUI = cookingUI;

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

        for (int i = 0; i < cookingUI.cookSlots.Length; i++) {
            Slot currentSlot = cookingUI.cookSlots[i].GetComponent<Slot>();
            if(currentSlot.Items.Count > 0 && currentSlot.CurrentItem.isFood) {
                currentSlot.cookTime -= Time.deltaTime;
                if (currentSlot.cookTime <= 0 && currentSlot.CurrentItem.type == ItemType.RAWFISH) {
                    currentSlot.ClearSlot();
                    currentSlot.AddItem(Resources.Load<Item>("cookedFish"));
                    currentSlot.cookTime = 2;
                }
            } else {
                currentSlot.cookTime = 2;
            }
        }
    }
}