using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public int itemPosition;
    float timeCooked;
    string type;
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
            type = "raw";
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        timeCooked = timeCooked + Time.deltaTime;
        if (timeCooked >= 5 && spriteRenderer.sprite == raw) 
        {
            spriteRenderer.sprite = cooked;
            type = "cooked";
            if(itemPosition == 0){
                cookingUI.Slot4.GetComponent<Slot>().CurrentItem.type = ItemType.COOKEDFISH;
            } else if(itemPosition == 1){
                cookingUI.Slot5.GetComponent<Slot>().CurrentItem.type = ItemType.COOKEDFISH;
            } else if(itemPosition == 2){
                cookingUI.Slot6.GetComponent<Slot>().CurrentItem.type = ItemType.COOKEDFISH;
            }
        }
        
        if(timeCooked >= 10 && spriteRenderer.sprite == cooked)
        {
            spriteRenderer.sprite = burnt;
            type = "burnt";
            if(itemPosition == 0){
                cookingUI.Slot4.GetComponent<Slot>().CurrentItem.type = ItemType.BURNTFISH;
            } else if(itemPosition == 1){
                cookingUI.Slot5.GetComponent<Slot>().CurrentItem.type = ItemType.BURNTFISH;
            } else if(itemPosition == 2){
                cookingUI.Slot6.GetComponent<Slot>().CurrentItem.type = ItemType.BURNTFISH;
            }
        }
	}
}
