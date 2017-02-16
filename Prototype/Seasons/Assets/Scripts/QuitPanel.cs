using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void ActivatePanel() {
		gameObject.SetActive(true);
		// Show the panel
	}

	public void DeactivatePanel() {
		gameObject.SetActive(false);
	}
}
