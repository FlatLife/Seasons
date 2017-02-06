using UnityEngine;


public enum ItemType {NONE, FISHINGROD, STICK, WOOD, VINE, RAWFISH, COOKEDFISH, BURNTFISH, ROCK,
	ICE, HATCHET, BUCKET, CARROTSEED, HOE, SALTWATER, FRESHWATER, CARROT, CLOTHES, FIREPREP, BOTTLE, 
	WATERPURIFIER, SEAWEED, POTATOSEED, PINEAPPLESEED, STRAWBERRYSEED, POTATO, PINEAPPLE, STRAWBERRIES, CARROTGROW,
	POTATOGROW, PINEAPPLEGROW, STRAWBERRYGROW};


public class Item : MonoBehaviour {

	public string itemName;
	public string itemUse;
	public ItemType type;
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;
	public int maxSize;
	public bool isFood;
	public bool isCrop;
	public bool isFinishedCrop;

	public string message;
	public Slot slot;
	private Item newItem;
	private BoxCollider2D collider;
	private GameObject hunger;
	private GameObject thirst;
	private PlaceObjects builder;
	public bool keepItem;
	public int durability;

	// Returns boolean for whether the item should be deleted or not
	public bool Use() {
		bool toBeDeleted = false;
		switch(type) {
		case ItemType.COOKEDFISH:
				toBeDeleted = true;
				hunger  = GameObject.Find("HungerBar");
				hunger.GetComponent<BarScript>().increment(0.1f);
				break;
			case ItemType.FIREPREP:
				toBeDeleted = true;
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
				slot.DestroyItem ();
				newItem = Instantiate(Resources.Load<GameObject>("Bucket").GetComponent<Item>(), slot.transform);
				collider = newItem.gameObject.GetComponent<BoxCollider2D>();
				if(collider != null){
					collider.enabled = false;
				}
				newItem.transform.position = new Vector3 (0,20f,0);
				newItem.GetComponent<SpriteRenderer>().enabled = false;
				slot.AddItem(newItem);
				break;
			case ItemType.SALTWATER:
				slot.DestroyItem ();
				newItem = Instantiate(Resources.Load<GameObject>("Bucket").GetComponent<Item>(), slot.transform);
				collider = newItem.gameObject.GetComponent<BoxCollider2D>();
				if(collider != null){
					collider.enabled = false;
				}
				newItem.transform.position = new Vector3 (0,20f,0);
				newItem.GetComponent<SpriteRenderer>().enabled = false;
				slot.AddItem(newItem);

				break;
			case ItemType.WATERPURIFIER:
				toBeDeleted = true;
				builder = GameObject.Find("Main Camera").GetComponent<PlaceObjects>();
				builder.build("WaterPurifier", "PlaceWater");
				break;
			case ItemType.BUCKET:
				bool nearWater = GameObject.Find ("Player").GetComponent<Player> ().switchSwimMode;
				if (nearWater) {
				slot.DestroyItem ();
				newItem = Instantiate(Resources.Load<GameObject>("SaltWater").GetComponent<Item>(), slot.transform);
				collider = newItem.gameObject.GetComponent<BoxCollider2D>();
				if(collider != null){
					collider.enabled = false;
				}
				newItem.transform.position = new Vector3 (0,20f,0);
				newItem.GetComponent<SpriteRenderer>().enabled = false;
				slot.AddItem(newItem);
				}
				break;
			case ItemType.BOTTLE:
				toBeDeleted = true;
				GameObject scroll = Instantiate (Resources.Load<GameObject> (message));
				scroll.transform.position = GameObject.Find ("Player").transform.position;
				break;
		}
		return toBeDeleted;
	}
}

