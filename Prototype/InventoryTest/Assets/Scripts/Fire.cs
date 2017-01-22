using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Transform itemPrefab;
    public int cookCount;
    float xCo;
    float yCo;
    // An array with the sprites used for animation
    public Sprite[] animSprites;

    // Controls how fast to change the sprites when
    // animation is running
    public float framesPerSecond;

    // Reference to the renderer of the sprite
    // game object
    SpriteRenderer animRenderer;

    // Time passed since the start of animatin
    private float timeAtAnimStart;

    // Indicates whether animation is running or not
    private bool animRunning = false;

    // Use this for initialization
    void Start () {
        cookCount = 0;
        // Get a reference to game object renderer and
        // cast it to a Sprite Renderer
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;

    }

    void FixedUpdate()
    {
        if (!animRunning)
        {
                // Animation will start playing
                animRunning = true;

                // Record time at animation start
                timeAtAnimStart = Time.timeSinceLevelLoad;
        }
    }

    // Update is called once per frame
    void Update () {
		
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
