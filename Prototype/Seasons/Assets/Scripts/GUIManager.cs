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
		if(player.atUse){
			if(popup == null){
				popup = Instantiate(Resources.Load<GameObject>("EKey"), this.transform);
			}
			if(player.canTouch){
				string item = player.itemColliderID.gameObject.GetComponent<Item>().itemName;
				popup.GetComponentInChildren<Text>().text = "Pick up " + item;
			}else if(player.atFish){
				if(!player.GetComponent<Fishing>().isFishing){
					if(player.canFish){
						popup.GetComponentInChildren<Text>().text = "Cast Rod";
						popup.GetComponent<Image>().enabled = true;
					}else{
						popup.GetComponent<Image>().enabled = false;
						popup.GetComponentInChildren<Text>().text = "I need to make a fishing rod";
					}
				}else if(player.GetComponent<Fishing>().isFishing && !player.GetComponent<Fishing>().minigame){
					popup.GetComponentInChildren<Text>().text = "Exit Fishing";
				}else if(player.GetComponent<Fishing>().isFishing && player.GetComponent<Fishing>().minigame){
					popup.GetComponentInChildren<Text>().text = "Catch Fish!";
				}
			}else if(player.atFire){
				if(!player.playingFireStart){
					int state = player.fireColliderID.gameObject.GetComponent<Fire>().fireState;
					if(state <= 0){
						popup.GetComponentInChildren<Text>().text = "Start Fire";
					}else {
						popup.GetComponentInChildren<Text>().text = "Open Fire";
					}
				}else if(player.playingFireStart){
					popup.GetComponentInChildren<Text>().text = "Mash to start Fire";
				}
			}else if(player.atFarm){
				popup.GetComponentInChildren<Text>().text = "Open Farm";
			}else if(player.atBarrel){
				popup.GetComponentInChildren<Text>().text = "Open Barrel";
			}else if(player.switchSwimMode){
				if(player.isSwimming){
					popup.GetComponentInChildren<Text>().text = "Stop swimming";
				}else if(!player.isSwimming){
					popup.GetComponentInChildren<Text>().text = "Start Swimming";
				}
			}else if(player.atWaterPurifier){
				popup.GetComponentInChildren<Text>().text = "Open Water purifier";
			}
			Vector3 pos = new Vector3(550f - (0.3f * popup.GetComponentInChildren<Text>().preferredWidth), 75, 0);
			popup.transform.position = pos;
		}else{
			Destroy(popup);
		}

		if (Input.GetButtonDown("Pause") && !player.openUI)
        {
            // If user presses ESC, show the pause menu in pause mode
            pauseMenu.GetComponent<PauseMenuManager>().ShowPause();
        }
	}
}
