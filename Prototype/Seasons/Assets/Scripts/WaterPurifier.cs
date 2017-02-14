using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPurifier : MonoBehaviour {

	public Canvas canvas;
	public WaterPurifierUI WaterUI;
	public float waterPurify = 20f;
	GameObject player;
	float distanceFromPlayer;
	AudioSource sound;

	// Use this for initialization

	void Awake () {
		player = GameObject.Find("Player");
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		WaterUI = Instantiate(Resources.Load<WaterPurifierUI>("WaterPurifierUI"));
		WaterUI.name = "WaterPurifierUI";
		WaterUI.Initialize();
		WaterUI.transform.SetParent(canvas.transform);
		sound = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		if(sound.isPlaying){
			//adjusting the sound volume
            distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
            sound.volume = (1 - distanceFromPlayer/5);
		} 
		if (!WaterUI.Slot1.isEmpty && !WaterUI.Slot2.isEmpty) {
			if (WaterUI.Slot1.CurrentItem.type == ItemType.SALTWATER
			   && WaterUI.Slot2.CurrentItem.type == ItemType.BUCKET) {
				waterPurify -= Time.deltaTime;
				//play audio 
				if(!sound.isPlaying){
					sound.Play();
				}
				if (waterPurify <= 0) {
					//stop audio 
					if(sound.isPlaying){
						sound.Stop();
					}
					WaterUI.Slot2.ClearSlot ();
					Item water = Instantiate(Resources.Load<Item> ("FreshWater"));
					water.transform.position = new Vector3 (0,20f,0);
					WaterUI.Slot2.AddItem (water);
					WaterUI.Slot1.ClearSlot ();
					Item bucket = Instantiate(Resources.Load<Item> ("Bucket"));
					bucket.transform.position = new Vector3 (0,20f,0);
					WaterUI.Slot1.AddItem(bucket);
					waterPurify = 20f;
				}
			}else if (WaterUI.Slot2.CurrentItem.type == ItemType.SALTWATER
			   && WaterUI.Slot1.CurrentItem.type == ItemType.BUCKET) {
				waterPurify -= Time.deltaTime;
				//play audio 
				if(!sound.isPlaying){
					sound.Play();
				}
				if (waterPurify <= 0) {
					//stop audio 
					if(sound.isPlaying){
						sound.Stop();
					}
					WaterUI.Slot1.ClearSlot ();
					Item water = Instantiate(Resources.Load<Item> ("FreshWater"));
					water.transform.position = new Vector3 (0,20f,0);
					WaterUI.Slot1.AddItem (water);
					WaterUI.Slot2.ClearSlot ();
					Item bucket = Instantiate(Resources.Load<Item> ("Bucket"));
					bucket.transform.position = new Vector3 (0,20f,0);
					WaterUI.Slot2.AddItem(bucket);
					waterPurify = 20f;
				}
			}
		}
	}
}
