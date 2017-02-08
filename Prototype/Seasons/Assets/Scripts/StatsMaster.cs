using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsMaster : MonoBehaviour {

	public float time;

	private GameObject health;
	private GameObject thirst;
	private GameObject hunger;
	private GameObject warmth;

	private float warmthConstant = 0.02f;
	private float hungerConstant = 0.02f;
	private float thirstConstant = 0.02f;
	private float healthConstant = 0.02f; 

	// Use this for initialization
	void Start () {
		health = GameObject.Find("Health");
		thirst = GameObject.Find("Thirst");
		hunger = GameObject.Find("Hunger");
		warmth = GameObject.Find("Warmth");

	}
	
	// Update is called once per frame
	void Update () {
		StatChange();
	}

	public void StatChange() {
	time -= Time.deltaTime;
		if(time < 0f){
			//Winter Values
			if (GameMaster.isWinter) {
				//Winter Warmth
				if(warmth.GetComponent<BarScript>().returnAmount() > 0.5f) {
					warmth.GetComponent<BarScript>().decrement(warmthConstant);
				} else {
					warmth.GetComponent<BarScript>().decrement(warmthConstant*1.5f);
				}
				//Winter Hunger
				hunger.GetComponent<BarScript>().decrement(hungerConstant*1.5f);
				//Winter Thirst
				thirst.GetComponent<BarScript>().decrement(thirstConstant*0.5f);
				//Winter Health
				if(hunger.GetComponent<BarScript>().barEmpty()){
					health.GetComponent<BarScript>().decrement(healthConstant);
				}
				if(thirst.GetComponent<BarScript>().barEmpty()){
					health.GetComponent<BarScript>().decrement(healthConstant);
				}
				if (hunger.GetComponent<BarScript>().barEmpty() && thirst.GetComponent<BarScript>().barEmpty()) {
					health.GetComponent<BarScript>().decrement(healthConstant*2.5f);
				}
			//Summer Values
			} else {
				//Summer Warmth
				if(warmth.GetComponent<BarScript>().returnAmount() > 0.5f) {
					warmth.GetComponent<BarScript>().decrement(warmthConstant*1.5f);
				} else {
					warmth.GetComponent<BarScript>().decrement(warmthConstant*1.1f);
				}
				//Summer Hunger
				hunger.GetComponent<BarScript>().decrement(hungerConstant*0.75f);
				//Summer Thirst
				thirst.GetComponent<BarScript>().decrement(thirstConstant);
				//Summer Health
				if(hunger.GetComponent<BarScript>().barEmpty()){
					health.GetComponent<BarScript>().decrement(healthConstant);
				}
				if(thirst.GetComponent<BarScript>().barEmpty()){
					health.GetComponent<BarScript>().decrement(healthConstant*0.75f);
				}
				if (hunger.GetComponent<BarScript>().barEmpty() && thirst.GetComponent<BarScript>().barEmpty()) {
					health.GetComponent<BarScript>().decrement(healthConstant*1.25f);
				}
			}
			//Reset tick
			time = 5f;
		}
		//Game Over check
		if(health.GetComponent<BarScript>().barEmpty()){
			SceneManager.LoadScene("GameOver");
		}
	}
}


