using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Transform itemPrefab;

    Transform food1;
    Transform food2;
    Transform food3;

    public Sprite raw;
    public Sprite cooked;
    public Sprite burnt;

    private SpriteRenderer spriteRenderer;

    public CookingUI cookingUI;
    float timeCooked1;
    float timeCooked2;
    float timeCooked3;
    int frameIndex;
    float xCo;
    float yCo;
    // An array with the sprites used for animation
    public Sprite[] animSprites;

    // Reference to the renderer of the sprite
    // game object
    SpriteRenderer animRenderer;

    private float timeSinceLastFrame;    

    // Use this for initialization
    void Start () {
        cookingUI = GameObject.Find("CookingUI").GetComponent<CookingUI>();
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

        //POSITION 1
        if(cookingUI.cooking1){
            if(!cookingUI.tmp1.isEmpty)
            timeCooked1 = timeCooked1 + Time.deltaTime;
            if (timeCooked1 >= 5 && food1.GetComponent<Food>().spriteRenderer.sprite == raw) 
            {
                food1.GetComponent<Food>().spriteRenderer.sprite = cooked;
                if(food1.GetComponent<Food>().itemPosition == 1){
                    cookingUI.ChangeItem(1, "CookedFish");
            }
            
            if(timeCooked2 >= 10 && food1.GetComponent<Food>().spriteRenderer.sprite == cooked)
            {
                food1.GetComponent<Food>().spriteRenderer.sprite = burnt;
                if(food1.GetComponent<Food>().itemPosition == 1){
                    cookingUI.ChangeItem(2, "BurntFish");
                    }
                }
            }
        }

        //POSITION 2
        if (cookingUI.cooking2)
        {
            if (!cookingUI.tmp2.isEmpty)
                timeCooked2 = timeCooked2 + Time.deltaTime;
            if (timeCooked2 >= 5 && food2.GetComponent<Food>().spriteRenderer.sprite == raw)
            {
                food2.GetComponent<Food>().spriteRenderer.sprite = cooked;
                if (food2.GetComponent<Food>().itemPosition == 2)
                {
                    cookingUI.ChangeItem(2, "CookedFish");
                }

                if (timeCooked2 >= 10 && food2.GetComponent<Food>().spriteRenderer.sprite == cooked)
                {
                    food2.GetComponent<Food>().spriteRenderer.sprite = burnt;
                    if (food2.GetComponent<Food>().itemPosition == 2)
                    {
                        cookingUI.ChangeItem(2, "BurntFish");
                    }
                }
            }
        }

        //POSITION 3
        if (cookingUI.cooking3)
        {
            //if there is an item in the cooking slot
            if (!cookingUI.tmp3.isEmpty)
                timeCooked3 = timeCooked3 + Time.deltaTime;
            if (timeCooked3 >= 5 && food3.GetComponent<Food>().spriteRenderer.sprite == raw)
            {
                //set the sprite to the cooked sprite
                food3.GetComponent<Food>().spriteRenderer.sprite = cooked;
                if (food3.GetComponent<Food>().itemPosition == 3)
                {
                    cookingUI.ChangeItem(3, "CookedFish");
                }

                if (timeCooked2 >= 10 && food3.GetComponent<Food>().spriteRenderer.sprite == cooked)
                {
                    food3.GetComponent<Food>().spriteRenderer.sprite = burnt;
                    if (food3.GetComponent<Food>().itemPosition == 3)
                    {
                        cookingUI.ChangeItem(3, "BurntFish");
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		Player control = other.gameObject.GetComponent<Player>();
		control.atFire = true;

	}

	void OnTriggerExit2D(Collider2D other)
	{
		Player control = other.gameObject.GetComponent<Player>();
		control.atFire = false;
	}

    public void startCooking(int position)
    {
        //Puts fish in position based on how many are already on the fire
        if (position == 2)
        {
            food1 = Instantiate(itemPrefab);
            food1.position = new Vector3(getXCordinate() - 0.5f, getYCordinate() + 0.8f, 0);
            food1.GetComponent<Food>().itemPosition = 2;
            timeCooked1 = Time.deltaTime;
        }
        else if (position == 3)
        {
            food2 = Instantiate(itemPrefab);
            food2.position = new Vector3(getXCordinate() + 0.5f, getYCordinate() + 0.8f, 0);
            food2.GetComponent<Food>().itemPosition = 3;
            timeCooked2 = Time.deltaTime;
        }
        else if(position == 1)
        {
            food3 = Instantiate(itemPrefab);
            food3.position = new Vector3(getXCordinate(), getYCordinate() + 0.8f, 0);
            food3.GetComponent<Food>().itemPosition = 1;
            timeCooked3 = Time.deltaTime;
        }
    }

    public void removeItem(int position){
        if(position == 1){
            Destroy(food2.gameObject);
        } else if(position == 2){
            Destroy(food1.gameObject);
        } else if(position == 3){
            Destroy(food3.gameObject);
        }
    }

    //Returns the X cordinate of the object
    public float getXCordinate()
    {
        return transform.position.x;
    }

    //Returns the Y cordinate of the object
    public float getYCordinate()
    {
        return transform.position.y;
    }
}
