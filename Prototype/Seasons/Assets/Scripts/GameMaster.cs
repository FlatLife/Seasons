using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static int dayCount;
	public static bool isWinter;

	public AudioSource music;
	public AudioClip summerMusic;
	public AudioClip winterMusic;
	private static float fadeVolume = 2;
	private static bool fadingMusic = false;
	private static bool fadingOut;
	private static bool enteringWinter;

	void Start(){
		music = GetComponent<AudioSource>();
		music.clip = summerMusic;
		music.Play();
		music.loop = true;
	}
	void Update(){
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
		fadingMusic = true;
		fadingOut = true;
		if(winter){
			enteringWinter = true;
		}
	}
	
}
