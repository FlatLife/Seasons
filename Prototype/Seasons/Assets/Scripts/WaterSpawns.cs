using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawns : MonoBehaviour {

	Item item;
	List<Item> items = new List<Item>();
	GameObject player;
	float coolDown = 30;
	float xAxis;
	int itemChoice;
	

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}

	void Update () {
		//check if any items have been picked up if the list is full
		if(items.Count >= 10){
			for(int i = items.Count - 1; i >= 0; i--){
				if(items[i] == null){
					items.RemoveAt(i);
				}
			}
		}
		//if it is time to spawn an item and the player is not in the water
		if(coolDown <= 0 && !player.GetComponent<Player>().isSwimming && items.Count < 10){
			itemChoice = Random.Range(0, 2);
			switch (itemChoice){
				case 0:
					item = Instantiate(Resources.Load<Item>("Rock"));
					break;
				case 1:
					item = Instantiate(Resources.Load<Item>("Seaweed"));
					break;
			}
			items.Add(item);
			xAxis = Random.Range(-42.8f, -21f);
			item.transform.position = new Vector3(xAxis, -22.72f, 0);
			coolDown = 30;
		} else {
			coolDown -= Time.deltaTime;
		}
	}
}
