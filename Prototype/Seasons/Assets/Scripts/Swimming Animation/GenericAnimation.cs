using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAnimation : MonoBehaviour {
	public string animationName;
	public float animationSpeed;
	public bool loopBackward;
	private Sprite[] usedAnimation;
	private float timeToNext = 0;
	private SpriteRenderer animRenderer;
	private int frameIndex = 0;
	private bool frameDirection = false;

	// Use this for initialization
	void Start () {
		usedAnimation = Resources.LoadAll<Sprite>(animationName);
		animRenderer = transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timeToNext >= animationSpeed) {
			animRenderer.sprite = usedAnimation[frameIndex];
			frameIndex += frameDirection ? -1 : 1;
			if ((frameIndex+1) >= usedAnimation.Length || frameIndex <= 0) {
				if (!loopBackward) {
					frameIndex = 0;
				} else {
					frameDirection = !frameDirection;
				}
			}
			timeToNext = 0;
		} else {
			timeToNext += Time.deltaTime;
		}
	}
}
