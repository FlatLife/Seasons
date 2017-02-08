using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour {

	Item item;
	List<Item> groundItems = new List<Item>();
	int itemChoice;
	int seedChoice;
	public float spawnProbability;
	float xAxisChange;
	public float coolDown = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if the amount of items on the ground is full
		if(groundItems.Count >= 5){
			foreach(Item thing in groundItems){
				if(thing == null){
					groundItems.Remove(thing);
				}
			}
		} else {
			if(Random.Range(0f,1f) > spawnProbability && (coolDown <= 0)){
			itemChoice = Random.Range(0, 4);
			switch (itemChoice){
				case 0:
					item = Instantiate(Resources.Load<Item>("Wood"));
					break;
				case 1:
					item = Instantiate(Resources.Load<Item>("Stick"));
					break;
				case 2:
					item = Instantiate(Resources.Load<Item>("Vine"));
					break;
				case 3:
					seedChoice = Random.Range(0, 4);
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
					}
					break;
			}
			groundItems.Add(item);
			foreach(Item thing in groundItems){
				if(thing == null){
					groundItems.Remove(item);
				}
			}
			//getting a random x axis change, has to not be between 2 and 6.5
			xAxisChange = Random.Range(-5f, 14f);
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
}
