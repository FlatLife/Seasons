using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenuManager : MonoBehaviour {
	// Indicates whether the game in paused mode
	public bool pauseGame = false;
	// Use this for initialization

	// Show the pause menu in pause mode (the
	// first option will say "Resume")
	public void ShowPause() {
		gameObject.SetActive(true);
		// Pause the game
		pauseGame = true;
		// Show the panel
		
	}
		
		// Hide the menu panel
	public void Hide() {
		// Deactivate the panel
		pauseGame = false;
		gameObject.SetActive(false);
		// Resume the game (if paused)
		Time.timeScale = 1f;
		}

	public void Quit(){
		pauseGame = false;
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}
		// Update is called once per frame
		void Update () {
			// If game is in pause mode, stop the timeScale value to 0
			if(pauseGame) {
				Time.timeScale = 0;
			}
			if(Input.GetButtonDown("Pause")){
				Hide();
			}
		}
}