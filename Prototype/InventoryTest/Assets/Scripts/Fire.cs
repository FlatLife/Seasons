using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Transform itemPrefab;
    public int cookCount;
    float xCo;
    float yCo;

	// Use this for initialization
	void Start () {
        cookCount = 0;
		
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
            food.position = new Vector3(getXCordinate() - 0.7f, getYCordinate() + 1, -1);
        }
        else if (cookCount == 2)
        {
            Transform food = Instantiate(itemPrefab);
            food.position = new Vector3(getXCordinate() + 0.7f, getYCordinate() + 1, -1);
        }
        else if(cookCount == 0)
        {
            Transform food = Instantiate(itemPrefab);
            food.position = new Vector3(getXCordinate(), getYCordinate() + 1, -1);
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
