using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

	public Canvas canvas;
	public BarrelUI BarrelUI;
	//public float waterPurify = 3f;
	// Use this for initialization

	void Awake () {
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		BarrelUI = Instantiate(Resources.Load<BarrelUI>("BarrelUI"));
		BarrelUI.name = "BarrelUI";
		BarrelUI.Initialize();
		BarrelUI.transform.SetParent(canvas.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
