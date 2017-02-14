using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSound : MonoBehaviour {

	GameObject player;
	AudioSource sound;
	float distanceFromPlayer;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {	
		//changing volume of sound
		distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
		sound.volume = (1/distanceFromPlayer*2)*0.3f+0.05f;
	}
}
