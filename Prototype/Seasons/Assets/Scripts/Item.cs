using UnityEngine;


public enum ItemType {NONE, FISHINGROD, STICK, WOOD, VINE, RAWFISH, COOKEDFISH, BURNTFISH, ROCK,
 SEAWATER, ICE, HATCHET, BUCKET, SEED, HOE, WATER, CARROT, CLOTHES};


public class Item : MonoBehaviour {

	public ItemType type;
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;
	public int maxSize;
	public bool isFood;

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
			case ItemType.RAWFISH:
				Debug.Log("Fished");
				break;
		}
	}
}

