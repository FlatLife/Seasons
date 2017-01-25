using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public int itemPosition;
    float timeCooked;
    public bool cooking;

    public SpriteRenderer spriteRenderer;

    public CookingUI cookingUI;

    // Use this for initialization
    void Start () {      
        cookingUI = GameObject.Find("CookingUI").GetComponent<CookingUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
