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

	public static int dayCount = 1;
	public float dayLength;

	// Use this for initialization
	void Start () {
		dayCountText = GameObject.Find("DayCount").GetComponent<Text>();
		GameMaster.dayCount = dayCount;
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
			}
			fadingIn = true;
		}

		if(fadeTime <= 0 && fadingIn){
			//spawn message in a bottle when day starts
			if(bottleHasSpawned != true){
				GameObject bottle = Instantiate(Resources.Load<GameObject>("messageBottle"));
				bottleHasSpawned = true;
			}
			fade.GetComponent<ScreenFade>().FadeToClear();
		}
		if(fade.GetComponent<SpriteRenderer>().color.a < 0.05f && fadingIn){
			dayTime = dayLength;
			fadeTime = 2f;
			fadingIn = false;
		}
	}
}
