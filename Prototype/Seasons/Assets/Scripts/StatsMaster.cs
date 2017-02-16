using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsMaster : MonoBehaviour {

	public float time;

	private bool seasonChange;
	private GameObject health;
	private GameObject thirst;
	private GameObject hunger;
	private GameObject warmth;

	public float warmthConstant = 0.0002f;
	public float hungerConstant = 0.0002f;
	public float thirstConstant = 0.0002f;
	public float healthConstant = 0.0002f; 

	public float Health {
		get { return health.GetComponent<BarScript>().returnAmount(); }
	}
	public float Thirst {
		get { return thirst.GetComponent<BarScript>().returnAmount(); }
	}
	public float Hunger {
		get { return hunger.GetComponent<BarScript>().returnAmount(); }
	}
	public float Warmth {
		get { return warmth.GetComponent<BarScript>().returnAmount(); }
	}

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
		if(GameMaster.dayCount % 5 == 0 && seasonChange){
			SeasonStatChange();
			seasonChange = false;
		}
		if(GameMaster.dayCount % 5 == 1){
			seasonChange = true;
		}
	}

	public void SeasonStatChange() {
		hungerConstant += 0.0002f;
		warmthConstant += 0.0002f;
		thirstConstant += 0.0002f;
		if(!GameMaster.isWinter){
			Fishing.fishChance = 0.003f;
		}else if(GameMaster.isWinter){
			Fishing.fishChance = 0.0015f;
		}
	}

	public void StatChange() {
	time -= Time.deltaTime;
		if(time < 0f){
			//Winter Values
			if (GameMaster.isWinter) {
				//Winter Warmth
				if(warmth.GetComponent<BarScript>().returnAmount() > 0.5f) {
					warmth.GetComponent<BarScript>().decrement(warmthConstant * 3);
				} else {
					warmth.GetComponent<BarScript>().decrement(warmthConstant*2);
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
				if (warmth.GetComponent<BarScript>().barEmpty()) {
					health.GetComponent<BarScript>().decrement(healthConstant*2.5f);
					hunger.GetComponent<BarScript>().decrement(hungerConstant*0.75f);
				}
			//Summer Values
			} else {
				//Summer Warmth
				if(warmth.GetComponent<BarScript>().returnAmount() > 0.5f) {
					warmth.GetComponent<BarScript>().decrement(warmthConstant);
				} else {
					warmth.GetComponent<BarScript>().decrement(warmthConstant*0.75f);
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
			time = 0.1f;
		}
		//Game Over check
		if(health.GetComponent<BarScript>().barEmpty()){
			SceneManager.LoadScene("GameOver");
		}
	}
}


