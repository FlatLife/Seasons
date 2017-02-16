using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour {
	private float timeRemaining = 3f;
	private float animationRemaining = 1f;
	public Transform player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.position + new Vector3(0f,2.3f);
		timeRemaining -= Time.deltaTime;
		if (timeRemaining < 0) {
			animationRemaining -= Time.deltaTime;
			if (animationRemaining < 0f) {
				Destroy(transform.gameObject);
			}
			transform.GetComponent<SpriteRenderer>().color = new Color(1,1,1,Mathf.SmoothStep(0,1,animationRemaining/2));
		}
	}
}
