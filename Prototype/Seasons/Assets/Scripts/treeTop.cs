using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTop : MonoBehaviour {

	Item item;
	int itemChoice;
	public float spawnProbability;
	float xAxisChange;
	public float coolDown = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
					item = Instantiate(Resources.Load<Item>("Seeds"));
					break;
				case 3:
					item = Instantiate(Resources.Load<Item>("Vine"));
					break;		
			}
			//getting a random x axis change, has to not be between 2 and 6.5
			xAxisChange = Random.Range(-5f, 14f);
			while(xAxisChange < 6.5 && xAxisChange > 2){
					xAxisChange = Random.Range(-5f, 14f);
			}
			//add the x axis change
			Vector3 position = transform.position;
			position.x = xAxisChange;
			item.transform.position = position;		
			//add item drop script
			item.gameObject.AddComponent<ItemDrop>();
			coolDown = 5;
			xAxisChange = 0;
		} else {
			coolDown -= Time.deltaTime;
		}
	}

}
