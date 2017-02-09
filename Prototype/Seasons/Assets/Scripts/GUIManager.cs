using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	public Player player;

	public GameObject pauseMenu;

	private GameObject popup;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(player.atUse && !player.performingAction){
			if(popup == null){
				popup = Instantiate(Resources.Load<GameObject>("EKey"), this.transform);
			}
			if(player.canTouch){
				popup.GetComponentInChildren<Text>().text = "Pick up";
			}else if(player.canFish){
				popup.GetComponentInChildren<Text>().text = "Cast Rod";
			}else if(player.atFire){
				popup.GetComponentInChildren<Text>().text = "Start/Open Fire";
			}else if(player.atFarm){
				popup.GetComponentInChildren<Text>().text = "Open Farm";
			}else if(player.atBarrel){
				popup.GetComponentInChildren<Text>().text = "Open Barrel";
			}else if(player.switchSwimMode){
				popup.GetComponentInChildren<Text>().text = "Jump in water";
			}
			Vector3 pos = new Vector3(35, 65, 0);
			popup.transform.position = pos;
		}else{
			Destroy(popup);
		}

		if (Input.GetKey(KeyCode.Escape))
        {
            // If user presses ESC, show the pause menu in pause mode
            pauseMenu.GetComponent<PauseMenuManager>().ShowPause();
        }
	}
}
