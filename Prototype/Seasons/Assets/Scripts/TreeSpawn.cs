using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour {
	Item item;
	List<Item> items = new List<Item>();
	public float spawnProbability;
	public float coolDown = 20;
	public int removeTime;
	
	// Update is called once per frame
	void FixedUpdate () {
		//check if any items have been picked up if the list is full
		if(items.Count >= 10){
			for(int i = items.Count - 1; i >= 0; i--){
				if(items[i] == null){
					items.RemoveAt(i);
				}
			}
		}
		//if it is time to spawn an item
		if(coolDown <= 0 && items.Count < 10){
			int itemChoice = Random.Range(0, 3);
			switch (itemChoice){
				case 0:
					item = Instantiate(Resources.Load<Item>("Wood"));
					break;
				case 1:
					item = Instantiate(Resources.Load<Item>("Stick"));
					break;
				case 2:
					int seedChoice = Random.Range(0, 5);
					switch (seedChoice){
						case 0:
							item = Instantiate(Resources.Load<Item>("StrawberrySeeds"));
							break;
						case 1:
							item = Instantiate(Resources.Load<Item>("PineappleSeeds"));
							break;
						case 2:
							item = Instantiate(Resources.Load<Item>("PotatoSeeds"));
							break;
						case 3:
							item = Instantiate(Resources.Load<Item>("CarrotSeeds"));
							break;
						case 4:
							item = Instantiate(Resources.Load<Item>("Vine"));
							break;
					}
					break;
			}
			items.Add(item);
			//getting a random x axis change, has to not be between 2 and 6.5
			float xAxisChange = Random.Range(-5f, 14f);
			while(xAxisChange < 6.5 && xAxisChange > 2){
					xAxisChange = Random.Range(-5f, 14f);
			}
			//add the x axis change
			item.transform.position = new Vector3(xAxisChange, transform.position.y, 0);
			item.InitializeFall();
			//add item drop script
			coolDown = 20;
		} else {
				coolDown -= Time.deltaTime;
		}
	}
}
