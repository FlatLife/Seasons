using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour {

	public bool fadingIn = false;
	public float dayTime;
	public float fadeTime;
	public GameObject fade;
	bool bottleHasSpawned;

	private Text dayCountText;
	private Text season;

	public static int dayCount = 1;
	public float dayLength;

	private Transform island;
	private Transform tree;
	private Transform ocean;

	Color[] colorStates = {new Color(0.988f, 0.613f, 0.378f, 1),
						   new Color(0.866f, 0.82f, 0.725f, 1),
						   new Color(0.7f, 0.85f, 0.95f, 1),
						   new Color(0.7f, 0.85f, 0.95f, 1),
						   new Color(0.988f, 0.613f, 0.378f, 1),
						   new Color(0.113f, 0.329f, 0.439f, 1)};

	int currentColorState = 1;

	// Use this for initialization
	void Start () {
		dayCountText = GameObject.Find("DayCount").GetComponent<Text>();
		season = GameObject.Find("Season").GetComponent<Text>();
		GameMaster.dayCount = dayCount;
		GameObject bottle = Instantiate(Resources.Load<GameObject>("messageBottle"));
		bottle.GetComponent<Item>().message = "message" + dayCount;

		island = GameObject.Find("Island").transform;
		tree = GameObject.Find("Tree").transform;
		ocean = GameObject.Find("Ocean Background").transform;
	}
	
	// Update is called once per frame
	void Update () {
		dayTime -= Time.deltaTime;
		if(dayTime <= 0 && !fadingIn){
			fade.GetComponent<ScreenFade>().FadeToBlack();
			bottleHasSpawned = false;
		}

		if(dayTime <= 0 && fade.GetComponent<SpriteRenderer>().color.a > 0.95f){
			fadeTime -= Time.deltaTime;
			if(!fadingIn){
				dayCountText.text = "Day " + ++dayCount;
				GameMaster.dayCount = dayCount;
				if(dayCount % 5 == 0){
					GameMaster.isWinter = !GameMaster.isWinter;
					if(GameMaster.isWinter){
						season.text = "Winter";
					} else {
						season.text = "Summer";
					}
				}
			}
			fadingIn = true;
		}

		if(fadeTime <= 0 && fadingIn){
			//spawn message in a bottle when day starts
			if(bottleHasSpawned != true){
				GameObject bottle = Instantiate(Resources.Load<GameObject>("messageBottle"));
				bottle.GetComponent<Item>().message = "message" + dayCount;
				bottleHasSpawned = true;
			}
			fade.GetComponent<ScreenFade>().FadeToClear();
		}
		if(fade.GetComponent<SpriteRenderer>().color.a < 0.05f && fadingIn){
			currentColorState = 1;
			dayTime = dayLength;
			fadeTime = 2f;
			fadingIn = false;
		}

		float transition = (((dayLength - dayTime) / dayLength) * (colorStates.Length - 1)) % 1;	
		if (((dayLength - dayTime) / dayLength) > ((1f / (colorStates.Length - 1)) * (currentColorState)) && (currentColorState + 1) < colorStates.Length) {
			currentColorState++;
		}
		
		Color firstState = colorStates[currentColorState]; 
		Color secondState = colorStates[currentColorState-1];
		Color effectColour = new Color(((firstState.r - secondState.r) * transition) + secondState.r, 
								 ((firstState.g - secondState.g) * transition) + secondState.g,
								 ((firstState.b - secondState.b) * transition) + secondState.b,
								   1);
		island.GetComponent<SpriteRenderer>().color = effectColour;
		tree.GetComponent<SpriteRenderer>().color = effectColour;
		ocean.GetComponent<SpriteRenderer>().color = effectColour;
		Camera.main.backgroundColor = effectColour;
	}
}
