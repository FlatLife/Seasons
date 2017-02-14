using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour {
	public StatsMaster statsMaster;
	public Sprite[] iconSprites;
	private Transform context;

	private bool healthLock = false;
	private bool foodLock = false;
	private bool thirstLock = false;
	private bool warmthLock = false;

	private bool inventoryFlag = false;
	private bool healthFlag = false;
	private bool foodFlag = false; 
	private bool thirstFlag = false; 
	private bool warmthFlag = false;

	// 0 means free, 1 means inventory, 2 means health, 3 means food, 4 means thirst, 5 means warmth 
	private int playingState = 0;
	private float startTime;
	private bool startTimeSet = false;

	public bool InventoryFlag {
		get { return inventoryFlag; }
		set { inventoryFlag = value; }
	}

	void Start() {
		statsMaster = GameObject.Find("GameMaster").GetComponent<StatsMaster>();
		context = transform.GetChild(0);
	}
	
	void Update () {
		healthFlag = statsMaster.Health < 0.2f && !healthLock;
		foodFlag = statsMaster.Hunger < 0.2f && !foodLock;
		thirstFlag = statsMaster.Thirst < 0.2f && !thirstLock;
		warmthFlag = statsMaster.Warmth < 0.2f && !warmthLock;

		healthLock = healthLock ? !(statsMaster.Health > 0.5f) : statsMaster.Health < 0.2f;
		foodLock = foodLock ? !(statsMaster.Hunger > 0.5f) : statsMaster.Hunger < 0.2f;
		thirstLock = thirstLock ? !(statsMaster.Thirst > 0.5f) : statsMaster.Thirst < 0.2f;
		warmthLock = warmthLock ? !(statsMaster.Warmth > 0.5f) : statsMaster.Warmth < 0.2f;

		playingState = playingState == 0 && inventoryFlag ? 1 : playingState;
		playingState = playingState == 0 && healthFlag ? 2 : playingState;
		playingState = playingState == 0 && foodFlag ? 3 : playingState;
		playingState = playingState == 0 && thirstFlag ? 4 : playingState;
		playingState = playingState == 0 && warmthFlag ? 5 : playingState;

		if (playingState > 0) {
			startTime = startTimeSet ? startTime : Time.time;
			
			context.GetComponent<SpriteRenderer>().sprite = iconSprites[(playingState - 1)];
			playingState = 0;
		}
	}
}
