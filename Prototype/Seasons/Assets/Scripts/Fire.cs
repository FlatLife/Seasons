using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Transform itemPrefab;
    public int cookCount;
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
        cookCount = 0;
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

    public void startCooking()
    {
        //Puts fish in position based on how many are already on the fire
        if (cookCount == 1)
        {
            Transform food = Instantiate(itemPrefab);
            food.position = new Vector3(getXCordinate() - 35, getYCordinate() + 55, 0);
        }
        else if (cookCount == 2)
        {
            Transform food = Instantiate(itemPrefab);
            food.position = new Vector3(getXCordinate() + 45, getYCordinate() + 55, 0);
        }
        else if(cookCount == 0)
        {
            Transform food = Instantiate(itemPrefab);
            food.position = new Vector3(getXCordinate() + 5, getYCordinate() + 55, 0);
        }
        cookCount++;
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
