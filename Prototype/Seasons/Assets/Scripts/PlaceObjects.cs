﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour {

	public GameObject item;
	public bool canBuild;
	public bool buildMode = true;

	private GameObject hoverObject;

	private string buildItem;
	private string placeItem;

	private Color buildColor = new Color (1,1,1, 0.5f);
	private Color cantBuild = new Color (1,0,0, 0.5f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(buildMode){
			if(hoverObject == null){
				hoverObject = Instantiate(Resources.Load<GameObject>(placeItem));
				hoverObject.name = "Hover";
				hoverObject.layer = 2;
			}
			// Makes sure object isn't on top of something
			canBuild = !hoverObject.GetComponent<Placeable>().colliding;

			// Makes sure object is being built close enough to the player
			Vector3 centerPosition = Camera.main.transform.position + new Vector3(Screen.width/2, Screen.height/2 - 65);
			Vector2 difference = Input.mousePosition - centerPosition;
			float distance = difference.magnitude;
			canBuild = canBuild ? distance < 250 : false;

			// Makes sure object is not outside island
			int layerMask = 1 << 8 | 1 << 9;
			int hit = Physics2D.LinecastNonAlloc(Camera.main.ScreenToWorldPoint(centerPosition), Camera.main.ScreenToWorldPoint(Input.mousePosition), new RaycastHit2D[1], layerMask);
			canBuild = canBuild ? hit == 0 : false;
			

			// Set hoverObject's position and colour
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			hoverObject.transform.position = new Vector3(pos.x, pos.y, pos.y - 0.3f);
			hoverObject.GetComponent<SpriteRenderer>().color = canBuild ? buildColor : cantBuild;

			if (Input.GetMouseButtonDown(0) && canBuild) {
				item = Instantiate(Resources.Load<GameObject>(buildItem));
				item.name = buildItem;
				item.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				item.transform.position = new Vector3 (item.transform.position.x, item.transform.position.y, item.transform.position.y + 0.2f);
				buildMode = false;
				Destroy(hoverObject);
			}
		}
	}

	public void build(string item, string placeItem){
		buildItem = item;
		this.placeItem = placeItem;
		buildMode = true;
	}

}
