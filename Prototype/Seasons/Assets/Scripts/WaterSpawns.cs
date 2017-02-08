using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawns : MonoBehaviour {

	Item item;
	List<Item> groundItems = new List<Item>();
	GameObject player;
	float coolDown = 30;
	float xAxis;
	int itemChoice;
	

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//check if any items have been picked up if the list is full
		if(groundItems.Count >= 10){
			foreach(Item waterItem in groundItems){
				if(waterItem == null){
					groundItems.Remove(waterItem);
				}
			}
		} else {
			//if we can spawn an item
			if(coolDown <= 0){
				itemChoice = Random.Range(0, 2);
				switch (itemChoice){
					case 0:
						item = Instantiate(Resources.Load<Item>("Rock"));
						break;
					case 1:
						item = Instantiate(Resources.Load<Item>("Seaweed"));
						break;
				}
				groundItems.Add(item);
				//check if any items have been picked up after adding a new item
				foreach(Item waterItem in groundItems){
					if(waterItem == null){
						groundItems.Remove(waterItem);
					}
				}
				xAxis = Random.Range(-42.8f, -21f);
				item.transform.position = new Vector3(xAxis, -16, 0);
				coolDown = 30;
			} else {
				coolDown -= Time.deltaTime;
			}
		}
	}
}
