using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Transform itemPrefab;

    Transform food1;
    Transform food2;
    Transform food3;
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

    public void startCooking(int position)
    {
        //Puts fish in position based on how many are already on the fire
        if (position == 2)
        {
            food1 = Instantiate(itemPrefab);
            food1.position = new Vector3(getXCordinate() - 0.5f, getYCordinate() + 0.8f, 0);
            food1.GetComponent<Food>().itemPosition = 2;
            
        }
        else if (position == 3)
        {
            food2 = Instantiate(itemPrefab);
            food2.position = new Vector3(getXCordinate() + 0.5f, getYCordinate() + 0.8f, 0);
            food2.GetComponent<Food>().itemPosition = 3;
        }
        else if(position == 1)
        {
            food3 = Instantiate(itemPrefab);
            food3.position = new Vector3(getXCordinate(), getYCordinate() + 0.8f, 0);
            food3.GetComponent<Food>().itemPosition = 1;
        }
    }

    public void removeItem(int position){
        if(position == 1){
            Destroy(food1.gameObject);
        } else if(position == 2){
            Destroy(food2.gameObject);
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
