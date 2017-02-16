using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour {

	public bool fadingIn = false;
	public float dayTime;
	public float fadeTime;
	public GameObject fade;
	public ParticleSystem snowParticles;
	bool bottleHasSpawned;

	private Text dayCountText;
	private Text season;

	public static int dayCount = 1;
	public float dayLength;

	List<Transform> transformList = new List<Transform>();
	private Transform sunIcon;
	private Transform land;
	Color[] colorStates = {new Color(0.988f, 0.613f, 0.378f, 1),
						   new Color(0.95f, 0.95f, 0.95f, 1),
						   new Color(1f, 1f, 1f, 1),
						   new Color(1f, 1f, 1f, 1),
						   new Color(0.988f, 0.613f, 0.378f, 1),
						   new Color(0.113f, 0.329f, 0.439f, 1)};

	Color[] skyStates = {new Color(0.988f, 0.413f, 0.278f, 1),
						 new Color(0.5f, 0.8f, 0.95f, 1),
						 new Color(0.5f, 0.8f, 0.95f, 1),
						 new Color(0.5f, 0.8f, 0.95f, 1),
						 new Color(0.988f, 0.613f, 0.378f, 1),
						 new Color(0.053f, 0.129f, 0.239f, 1)};

	int currentColorState = 1;
	public Transform treeSnow;

	// Use this for initialization
	void Start () {
		dayCount = 1;
		dayCountText = GameObject.Find("DayCount").GetComponent<Text>();
		season = GameObject.Find("Season").GetComponent<Text>();
		GameMaster.dayCount = dayCount;
		GameObject bottle = Instantiate(Resources.Load<GameObject>("messageBottle"));
		bottle.GetComponent<Item>().message = "Message " + dayCount;

		transformList.Add(GameObject.Find("Island").transform);
		transformList.Add(GameObject.Find("Tree").transform);
		transformList.Add(GameObject.Find("Ocean Background").transform);
		transformList.Add(GameObject.Find("Ocean Background Bottom").transform);
		transformList.Add(GameObject.Find("Beach").transform);
		transformList.Add(GameObject.Find("Pier").transform);
		transformList.Add(GameObject.Find("oceanColor").transform);
		transformList.Add(GameObject.Find("Waves 1").transform);
		transformList.Add(GameObject.Find("Waves 2").transform);
		transformList.Add(treeSnow);
		land = GameObject.Find("Land").transform;
		transformList.Add(land);

		transformList.Add(GameObject.Find("Land").transform);

		sunIcon = GameObject.Find("Canvas/Stats/Sun Icon").transform;
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
				currentColorState = 1;		
				updateColor(transformList, colorStates[0], skyStates[0]);

				dayCountText.text = "Day " + ++dayCount;
				GameMaster.dayCount = dayCount;
				if(dayCount % 5 == 0){
					if (snowParticles.isPlaying) {
						snowParticles.Stop();
					} else {
						snowParticles.Play();
					}
					GameMaster.isWinter = !GameMaster.isWinter;
					GameMaster.fadeMusic(GameMaster.isWinter);
					if(GameMaster.isWinter){
						season.text = "Winter";
						land.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Snow");
						treeSnow.GetComponent<SpriteRenderer>().enabled = true;
					} else {
						season.text = "Summer";
						land.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Grass");
						treeSnow.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
			}
			fadingIn = true;
		}

		if(fadeTime <= 0 && fadingIn){
			//spawn message in a bottle when day starts
			if(bottleHasSpawned != true && dayCount <= 5){
				GameObject bottle = Instantiate(Resources.Load<GameObject>("messageBottle"));
				bottle.GetComponent<Item>().message = "Message " + dayCount;
				bottleHasSpawned = true;
			}
			fade.GetComponent<ScreenFade>().FadeToClear();
		}
		if(fade.GetComponent<SpriteRenderer>().color.a < 0.05f && fadingIn){
			dayTime = dayLength;
			fadeTime = 2f;
			fadingIn = false;
		}

		if (dayTime > 0) {
			if (((dayLength - dayTime) / dayLength) > ((1f / (colorStates.Length - 1)) * (currentColorState)) && (currentColorState + 1) < colorStates.Length) {
				currentColorState++;
			}

			float transition = (((dayLength - dayTime) / dayLength) * (colorStates.Length - 1)) % 1;		
			Color tintColour = interpolateColor(colorStates[currentColorState], colorStates[currentColorState-1], transition);
			Color skyColour = interpolateColor(skyStates[currentColorState], skyStates[currentColorState-1], transition);
			updateColor(transformList, tintColour, skyColour);
		}
		

		sunIcon.localPosition = Vector3.Lerp(new Vector3(0-sunIcon.parent.GetComponent<RectTransform>().rect.width/2, sunIcon.localPosition.y), new Vector3(0+sunIcon.parent.GetComponent<RectTransform>().rect.width/2, sunIcon.localPosition.y),((dayLength - dayTime) / dayLength));
	}

	Color interpolateColor(Color firstState, Color secondState, float transition) {
		return new Color(((firstState.r - secondState.r) * transition) + secondState.r, 
								 ((firstState.g - secondState.g) * transition) + secondState.g,
								 ((firstState.b - secondState.b) * transition) + secondState.b,
								   1);
	}

	void updateColor(List<Transform> transformList, Color tintColour, Color skyColour) {
		foreach (Transform toColor in transformList) {
			toColor.GetComponent<SpriteRenderer>().color = new Color(tintColour.r, tintColour.g, tintColour.b, toColor.transform.GetComponent<SpriteRenderer>().color.a);
		}
		Camera.main.backgroundColor = skyColour;
	}
}
