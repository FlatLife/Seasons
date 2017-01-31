using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public float time;

	private GameObject health;
	private GameObject thirst;
	private GameObject hunger;
	private GameObject warmth;


	// Use this for initialization
	void Start () {
		health = GameObject.Find("HealthBar");
		thirst = GameObject.Find("ThirstBar");
		hunger = GameObject.Find("HungerBar");
		warmth = GameObject.Find("WarmthBar");
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if(time < 0f){
			hunger.GetComponent<BarScript>().decrement(0.1f);
			thirst.GetComponent<BarScript>().decrement(0.1f);
			time = 5f;
			if(hunger.GetComponent<BarScript>().barEmpty()){
				health.GetComponent<BarScript>().decrement(0.05f);
			}
			if(thirst.GetComponent<BarScript>().barEmpty()){
				health.GetComponent<BarScript>().decrement(0.05f);
			}
	}
	}


}
