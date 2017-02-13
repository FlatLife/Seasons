using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {

	// Use this for initialization
	public Slider volumeSlider;

	public void VolumeController(){
		AudioListener.volume = volumeSlider.value;
	}
}
