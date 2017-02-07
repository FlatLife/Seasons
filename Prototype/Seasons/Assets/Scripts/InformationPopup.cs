﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPopup : MonoBehaviour {

	public string info;

	GameObject popup;
	public GameObject canvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	public void Show(){
		popup = Instantiate(Resources.Load<GameObject>(info));
		popup.transform.SetParent(canvas.transform);
	}

public void Hover(){
		Color highlight = new Color(1f, 1f, 0f ,1f);
		this.GetComponent<Image>().color = highlight;
}

public void Exit(){
	Color white = new Color(1f, 1f, 1f ,1f);
	this.GetComponent<Image>().color = white;
}


	public void Destroy(){
		Destroy(popup.gameObject);
	}
}