using UnityEngine;


public enum ItemType {NONE, FISHINGROD, STICK, WOOD, VINE, RAWFISH, COOKEDFISH, BURNTFISH, ROCK,
 SEAWATER, ICE, HATCHET, BUCKET, SEED, HOE, WATER, CARROT, CLOTHES, FIREPREP};


public class Item : MonoBehaviour {

	public ItemType type;
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;
	public int maxSize;
	public bool isFood;
	public bool isCrop;

	private GameObject hunger;
	private GameObject thirst;
	private PlaceObjects builder;


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
			case ItemType.COOKEDFISH:
				hunger  = GameObject.Find("HungerBar");
				hunger.GetComponent<BarScript>().increment(0.1f);
				break;
			case ItemType.FIREPREP:
				builder = GameObject.Find("Main Camera").GetComponent<PlaceObjects>();
				builder.build("Fire", "PlaceFire");
				break;
		}
	}
}

