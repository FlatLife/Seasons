using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour {

	public float cooldown;

	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		Time.timeScale = 0f;
		cooldown -= Time.unscaledDeltaTime;
		if(Input.anyKeyDown && cooldown < 0){
			close();
		}
	}
	
	// Update is called once per frame
	public void close(){
		Time.timeScale = 1f;
		Destroy (this.gameObject);
	}
}
