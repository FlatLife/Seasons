using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTop : MonoBehaviour {

	Item item;
	public float spawnProbability;
	float xPos;
	public float coolDown = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Random.Range(0f,1f) > spawnProbability && (coolDown <= 0)){
			Item item = Instantiate(Resources.Load<Item>("Wood"));
			Vector3 position = transform.position;
			while(xPos < 6.5 && xPos > 2){
				xPos = Random.Range(-5f, 14f);
			}		
			position.x += xPos;
			item.transform.position = position;
			item.gameObject.AddComponent<ItemDrop>();
			coolDown = 5;
		} else {
			coolDown -= Time.deltaTime;
		}
	}

}
