using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour {
	private StatsMaster statsMaster;
	private DayCycle dayCycle;
	public Sprite[] iconSprites;
	public GameObject player;
	private Transform context;

	private bool healthLock = false;
	private bool foodLock = false;
	private bool thirstLock = false;
	private bool warmthLock = false;
	private bool nightLock = false;
	private bool bottleLock = false;

	private bool inventoryFlag = false;
	private bool healthFlag = false;
	private bool foodFlag = false; 
	private bool thirstFlag = false; 
	private bool warmthFlag = false;
	private bool bottleFlag = false;
	private bool nightFlag = false;

	// 0 means free, 1 means inventory, 2 means health, 3 means food, 4 means thirst, 5 means warmth 
	private int playingState = 0;
	private float startTime;
	private bool startTimeSet = false;

	public bool InventoryFlag {
		get { return inventoryFlag; }
		set { inventoryFlag = value; }
	}

	public bool BottleFlag {
		get { return bottleFlag; }
		set { bottleFlag = value; }
	}

	void Start() {
		statsMaster = GameObject.Find("GameMaster").GetComponent<StatsMaster>();
		dayCycle = GameObject.Find("DayCycle").GetComponent<DayCycle>();
		context = transform.GetChild(0);
	}
	
	void Update () {
		transform.position = player.GetComponent<Player>().isSwimming ? player.transform.position + new Vector3(0.4f, 0.2f) : player.transform.position + new Vector3(0.4f, 1.26f);

		healthFlag = healthFlag || statsMaster.Health < 0.2f && !healthLock;
		foodFlag = foodFlag || statsMaster.Hunger < 0.2f && !foodLock;
		thirstFlag = thirstFlag || statsMaster.Thirst < 0.2f && !thirstLock;
		warmthFlag = warmthFlag || statsMaster.Warmth < 0.2f && !warmthLock;
		nightFlag = nightFlag || dayCycle.dayTime < 5f && !nightLock;
		if (DayCycle.dayCount > 1 && DayCycle.dayCount <= 5 ){
			bottleFlag = bottleFlag || dayCycle.dayTime < (dayCycle.dayLength - 5f) && !bottleLock;
		}

		healthLock = healthLock ? !(statsMaster.Health > 0.5f) : statsMaster.Health < 0.2f;
		foodLock = foodLock ? !(statsMaster.Hunger > 0.5f) : statsMaster.Hunger < 0.2f;
		thirstLock = thirstLock ? !(statsMaster.Thirst > 0.5f) : statsMaster.Thirst < 0.2f;
		warmthLock = warmthLock ? !(statsMaster.Warmth > 0.5f) : statsMaster.Warmth < 0.2f;
		nightLock = nightLock ? !(dayCycle.dayTime > (dayCycle.dayLength - 5f)) :  dayCycle.dayTime < 5f;
		bottleLock = bottleLock ? !(dayCycle.dayTime > (dayCycle.dayLength - 3f)) : dayCycle.dayTime < (dayCycle.dayLength - 5f) ;

		playingState = playingState == 0 && inventoryFlag ? 1 : playingState;
		playingState = playingState == 0 && healthFlag ? 2 : playingState;
		playingState = playingState == 0 && foodFlag ? 3 : playingState;
		playingState = playingState == 0 && thirstFlag ? 4 : playingState;
		playingState = playingState == 0 && warmthFlag ? 5 : playingState;
		playingState = playingState == 0 && bottleFlag ? 6 : playingState;
		playingState = playingState == 0 && nightFlag ? 7 : playingState;

		if (playingState > 0) {
			switch (playingState) {
				case 1: 
					inventoryFlag = false;
					break;
				case 2: 
					healthFlag = false;
					break;
				case 3: 
					foodFlag = false;
					break;
				case 4: 
					thirstFlag = false;
					break;
				case 5: 
					warmthFlag = false;
					break;
				case 6:
					bottleFlag = false;
					break;
				case 7:
					nightFlag = false;
					break;
			}

			context.GetComponent<SpriteRenderer>().sprite = iconSprites[(playingState - 1)];

			startTime = startTimeSet ? startTime : Time.time;
			startTimeSet = true;
			if ((Time.time - startTime) < 0.5f) {
				float size = Mathf.SmoothStep(0, 1, (Time.time - startTime)/0.5f);
				transform.localScale = new Vector3(size*0.6f, size*0.6f, 1);
			} else if ((Time.time - startTime) > 0.5f && (Time.time - startTime) < 0.7f) {
				float size = Mathf.SmoothStep(0, 1, (Time.time - startTime - 0.5f)/0.2f);
				context.transform.localScale = new Vector3(size, size, 1);
			} else if ((Time.time - startTime) > 3f && (Time.time - startTime) < 4f) {
				float alpha = Mathf.SmoothStep(0, 1, 1-(Time.time - startTime - 3f)/1f);
				context.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, alpha);
				transform.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, alpha);
			} else if ((Time.time - startTime) > 4f) {
				playingState = 0;
				startTimeSet = false;
				transform.localScale = new Vector3(0, 0, 1);
				context.transform.localScale = new Vector3(0, 0, 1);
				context.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
				transform.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
			}
		}
	}
}
