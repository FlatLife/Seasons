using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFade : MonoBehaviour{

	public float fadeSpeed = 1.5f; // Speed that the screen fades to and from black.

	void Update (){
	}

	public void FadeToClear (){
		gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(gameObject.GetComponent<SpriteRenderer>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	public void FadeToBlack (){
		gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(gameObject.GetComponent<SpriteRenderer>().color, Color.black, fadeSpeed * Time.deltaTime);
	}

}