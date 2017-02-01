﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour {

	public Canvas canvas;
    public int slotNum;
    private FarmingUI farmingUI;
	// Use this for initialization
	void Start () {
		farmingUI = Instantiate(Resources.Load<FarmingUI>("FarmingUI"));
        farmingUI.name = "FarmingUI";
        farmingUI.Initialize(slotNum);
        farmingUI.transform.SetParent(canvas.transform);
        GameObject.Find("Player").GetComponent<Player>().farmingUI = farmingUI;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}