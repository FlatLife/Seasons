using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat{
	public BarScript bar;
	public float currentVal;

	public float CurrentVal {
		get {
			return currentVal;
		}

		set {
			this.currentVal = value;
			bar.Value = currentVal;
		}
	}

	public void Initialize() {
		this.CurrentVal = currentVal;
	}
}
