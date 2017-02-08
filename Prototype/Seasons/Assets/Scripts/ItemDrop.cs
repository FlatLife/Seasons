using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {

	float speed = 5;
	float yAxisEnd;
	bool falling;

	// Use this for initialization
	void Start () {
		yAxisEnd = Random.Range(-2.5f, 4.5f);
		falling = true;
	}
	
	// Update is called once per frame
	void Update () { 
		if(yAxisEnd < transform.position.y && falling){
			transform.Translate(Vector3.down * speed * Time.deltaTime);
		} else {
			falling = false;
		}		
	}
}
