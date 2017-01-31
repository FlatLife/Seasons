using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeTop : MonoBehaviour {

	Item item;
	public float spawnProbability;

	public float rateOfFall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Random.Range(0f,1f) < spawnProbability){
			Item item = Instantiate(Resources.Load<Item>("Wood"));
			item.transform.position = transform.position;
			dropItem(item);
			spawnProbability = 5;
		}
		item.GetComponent<Item>();
		//item.dropItem
	}

	void dropItem(Item item){
		Vector3 position = item.transform.position;
		position.y -= rateOfFall;
		item.transform.position = position;
		rateOfFall *= 1.1f;
	}
}
