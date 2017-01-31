﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour {

 private Transform text;
	public GameObject backpack;
	public float fishChance = 0.002f;
	public Transform fishPrefab;
	public bool isFishing = false;
	public bool minigame = false;
	public bool hasCaught = false;
	public Transform rodAlert;
	public Transform canvas;
	GameObject alert;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isFishing && !minigame) {
			if(Random.Range(0f,1f) < fishChance  && Time.timeScale != 0){ 
				alert = Instantiate(Resources.Load<GameObject>("alert"));
				minigame = true;
				Vector3 position = transform.position;
				position.y += 3;
				alert.transform.position = position;
				Destroy (alert.gameObject, 2.0f);
			}
		}

		if (minigame && hasCaught) {
			Backpack inv = backpack.GetComponent<Backpack> ();
			Item item = (Resources.Load("rawFish") as GameObject).GetComponent<Item>();
			inv.AddItem(item);
			Destroy (alert);
			stop ();

		}
	}

	public void fish(){
		Backpack inv = backpack.GetComponent<Backpack> ();
		Item item = (Resources.Load("FishingRod") as GameObject).GetComponent<Item>();
		if(inv.CheckItem(item)){
			isFishing = true;
		}
		else if(text == null){
			GetComponent<Player>().performingAction = false;
			text = Instantiate(rodAlert);
			text.SetParent(canvas);
			Vector3 position = transform.position;
			position.y += 3;
			rodAlert.position = position;
			Destroy (text.gameObject, 2.0f);
		}
	}		

	public void stop(){
		isFishing = false;
		minigame = false;
		hasCaught = false;
	}
}