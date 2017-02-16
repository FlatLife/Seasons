using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Text DaysSurvived;
	public Text HighScoreDaysSurvived;

	// Use this for initialization
	void Start () {
		if(DaysSurvived != null){
			DaysSurvived.text = "You Survived " + GameMaster.dayCount + " Days!";
			HighScoreDaysSurvived.text = "Highscore: " + GameMaster.dayCount + " Days!";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Retry(){
		SceneManager.LoadScene("StoryComic");
	}

	public void Quit(){
		SceneManager.LoadScene("MainMenu");
	}
}
