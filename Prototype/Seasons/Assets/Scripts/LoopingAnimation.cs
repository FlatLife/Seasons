using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingAnimation : MonoBehaviour {

	private int frameIndex;
    // An array with the sprites used for animation
	public float animationSpeed;
    public Sprite[] animSprites;

    // Reference to the renderer of the sprite
    // game object
    SpriteRenderer animRenderer;
    float timeSinceLastFrame; 
	// Use this for initialization
	void Start () {
		//Sets the animation to the first frame
		animRenderer = GetComponent<Renderer>() as SpriteRenderer;
        frameIndex = 0;
        timeSinceLastFrame = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(frameIndex < animSprites.Length - 1){				
			if(timeSinceLastFrame > animationSpeed){
			animRenderer.sprite = animSprites[frameIndex];
			timeSinceLastFrame = 0;
			frameIndex++;
			} else{
				timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
			}	
		} else{
			frameIndex = 0;
		}
	}
}
