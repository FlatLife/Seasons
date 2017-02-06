using UnityEngine;


public enum ItemType {NONE, FISHINGROD, STICK, WOOD, VINE, RAWFISH, COOKEDFISH, BURNTFISH, ROCK,
	ICE, HATCHET, BUCKET, CARROTSEED, HOE, SALTWATER, FRESHWATER, CARROT, CLOTHES, FIREPREP, BOTTLE, 
	WATERPURIFIER, SEAWEED, POTATOSEED, PINEAPPLESEED, STRAWBERRYSEED};


public class Item : MonoBehaviour {

	public string itemName;
	public ItemType type;
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;
	public int maxSize;
	public bool isFood;
	public bool isCrop;
	public bool isFinishedCrop;

	private GameObject hunger;
	private GameObject thirst;
	private PlaceObjects builder;
	public bool keepItem;
	public int durability;

	// Returns boolean for whether the item should be deleted or not
	public bool Use() {
		bool toBeDeleted = true;
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
			case ItemType.HOE:
				builder = GameObject.Find("Main Camera").GetComponent<PlaceObjects>();
				builder.build("Farm", "PlaceFarm");
				durability--;
				toBeDeleted = durability == 0;
				break;
			case ItemType.FRESHWATER:
				thirst  = GameObject.Find("ThirstBar");
				thirst.GetComponent<BarScript>().increment(0.3f);
				break;
			case ItemType.WATERPURIFIER:
				builder = GameObject.Find("Main Camera").GetComponent<PlaceObjects>();
				builder.build("WaterPurifier", "PlaceWater");
				break;
		}
		return toBeDeleted;
	}
}

