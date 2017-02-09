using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {

	// Use this for initialization
	public Slider volumeSlider;
	public AudioSource volumeAudio;

	public void VolumeController(){
		volumeAudio.volume = volumeSlider.value;
	}
}
