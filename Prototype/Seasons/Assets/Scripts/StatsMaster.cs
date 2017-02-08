﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsMaster : MonoBehaviour {

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
		 if(Time.timeScale == 0)return;
		if(GameMaster.isWinter){
			warmth.GetComponent<BarScript>().decrement(0.0001f);
		} else if(warmth.GetComponent<BarScript>().returnAmount() > 0.5f){				
			warmth.GetComponent<BarScript>().decrement(0.00005f);
		}
		time -= Time.deltaTime;
		if(time < 0f){
			hunger.GetComponent<BarScript>().decrement(0.03f);
			thirst.GetComponent<BarScript>().decrement(0.05f);
			time = 5f;
			if(hunger.GetComponent<BarScript>().barEmpty()){
				health.GetComponent<BarScript>().decrement(0.005f);
			}
			if(thirst.GetComponent<BarScript>().barEmpty()){
				health.GetComponent<BarScript>().decrement(0.05f);
			}
		}
		if(health.GetComponent<BarScript>().barEmpty()){
			SceneManager.LoadScene("GameOver");
		}
	}
}
