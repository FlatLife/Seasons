using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject target;
	private float xOffSet;
	private float yOffSet;
	public float zpos;
	public bool atWall;
	public bool atFloor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target.transform.position.x > xOffSet){
			atWall =false;
		}
		if(target.transform.position.y > yOffSet){
			atFloor =false;
		}
		if(!atWall && !atFloor){
			transform.position = new Vector3(target.transform.position.x, target.transform.position.y, zpos);
		}else if(atWall && !atFloor){
			transform.position = new Vector3(transform.position.x, target.transform.position.y, zpos);
		}else if(!atWall && atFloor){
			transform.position = new Vector3( target.transform.position.x, transform.position.y, zpos);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "LeftWall"){
			atWall = true;
			xOffSet = target.transform.position.x;
		}
		if(other.tag == "OceanFloor"){
			atFloor = true;
			yOffSet = target.transform.position.y;
		}
	}
}
