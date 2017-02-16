using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static int dayCount;
	public static bool isWinter;

	public AudioSource music;
	public AudioClip summerMusic;
	public AudioClip winterMusic;

	public Player player;
	private static float fadeVolume = 2;
	private static bool fadingMusic = false;
	private static bool fadingOut;
	private static bool enteringWinter;
	private static AudioLowPassFilter filter;

	void Start(){
		music = GetComponent<AudioSource>();
		music.clip = summerMusic;
		music.Play();
		music.loop = true;
		filter = GetComponent<AudioLowPassFilter>();

	}
	void Update(){
		if(player.isSwimming){
			filter.cutoffFrequency = 700;
		} else {
			filter.cutoffFrequency = 22000;
		}

		if(fadingMusic){
			if(fadingOut){
				fadeVolume -= Time.deltaTime;
				music.volume = fadeVolume/2;
				if(music.volume == 0){
					fadingOut = false;
					if(enteringWinter){
						music.clip = winterMusic;
					} else {
						music.clip = summerMusic;
					}
					music.volume = 0;
					music.Play();
					music.loop = true;
				}
			} else {
				fadeVolume += Time.deltaTime;
				music.volume = fadeVolume/2;
				if(music.volume == 2){
					fadingMusic = false;
				}
			}
		}
		
	}

	public static void fadeMusic(bool winter){
		Debug.Log("MUSIC CHANGE");
		fadingMusic = true;
		fadingOut = true;
		fadeVolume = 2f;
		if(winter){
			enteringWinter = true;
		} else {
			enteringWinter = false;
		}
	}
	
}
