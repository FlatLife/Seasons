using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {
	public Player player;

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
			Vector3 pos = player.transform.position;
			pos.y += 2f; 
			popup.transform.position = pos;
		}else{
			Destroy(popup);
		}
	}
}
