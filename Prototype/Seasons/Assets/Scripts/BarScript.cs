using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private float fillAmount;
	[SerializeField]
	private Image content;


	public float Value {
		set {
			fillAmount = value/100;
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar();
		
	}

	private void HandleBar() {
		if (fillAmount != content.fillAmount) {
			content.fillAmount = fillAmount;
		}
	}

}
