using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType {REDBUTTON};


public class Item : MonoBehaviour {

	public ItemType type;
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;
	public int maxSize;

	public void Use() {
		switch(type) {
			case ItemType.REDBUTTON:
				Debug.Log("RedButtoned");
				break;
		}
	}
}

