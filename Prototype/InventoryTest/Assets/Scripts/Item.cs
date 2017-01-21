using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType {NONE, FISHINGROD, STICK, VINE};


public class Item : MonoBehaviour {

	public ItemType type;
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;
	public int maxSize;

	

	public void Use() {
		switch(type) {
			case ItemType.FISHINGROD:
				Debug.Log("FishingRodded");
				break;
			case ItemType.STICK:
				Debug.Log("Sticked");
				break;
			case ItemType.VINE:
				Debug.Log("Vined");
				break;
		}
	}
}

