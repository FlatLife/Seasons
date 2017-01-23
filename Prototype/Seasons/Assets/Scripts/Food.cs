using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    int itemPosition;
    float timeCooked;
    public Sprite raw;
    public Sprite cooked;
    public Sprite burnt;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {      
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
        }
        
        if(timeCooked >= 10 && spriteRenderer.sprite == cooked)
        {
            spriteRenderer.sprite = burnt;
        }
	}
}
