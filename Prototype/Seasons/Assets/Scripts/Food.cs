using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public int itemPosition;
    float timeCooked;
    public Sprite raw;
    public Sprite cooked;
    public Sprite burnt;

    private SpriteRenderer spriteRenderer;

    public CookingUI cookingUI;

    // Use this for initialization
    void Start () {      
        cookingUI = GameObject.Find("CookingUI").GetComponent<CookingUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // if the sprite on spriteRenderer is null then set the sprite to raw
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = raw;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        timeCooked = timeCooked + Time.deltaTime;
        if (timeCooked >= 5 && spriteRenderer.sprite == raw) 
        {
            spriteRenderer.sprite = cooked;
            if(itemPosition == 1){
                cookingUI.ChangeItem(itemPosition, "CookedFish");
            } else if(itemPosition == 2){
                cookingUI.ChangeItem(itemPosition, "CookedFish");
            } else if(itemPosition == 3){
                cookingUI.ChangeItem(itemPosition, "CookedFish");
            }
        }
        
        if(timeCooked >= 10 && spriteRenderer.sprite == cooked)
        {
            spriteRenderer.sprite = burnt;
            if(itemPosition == 1){
                cookingUI.ChangeItem(itemPosition, "BurntFish");
            } else if(itemPosition == 2){
                cookingUI.ChangeItem(itemPosition, "BurntFish");
            } else if(itemPosition == 3){
                cookingUI.ChangeItem(itemPosition, "BurntFish");
            }
        }
	}
}
