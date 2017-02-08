using UnityEngine;


public enum ItemType {NONE, FISHINGROD, STICK, WOOD, VINE, RAWFISH, COOKEDFISH, BURNTFISH, ROCK,
	ICE, HATCHET, BUCKET, CARROTSEED, HOE, SALTWATER, FRESHWATER, CARROT, CLOTHES, FIREPREP, BOTTLE, 
	WATERPURIFIER, SEAWEED, POTATOSEED, PINEAPPLESEED, STRAWBERRYSEED, POTATO, PINEAPPLE, STRAWBERRIES,
	RAWTROUT, RAWSALMON, RAWGUPPY, COOKEDTROUT, COOKEDSALMON, COOKEDGUPPY };


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
	public int cookTime;
	public int growTime;
	public string nextItem;
	public float zOffset;

	// Variables for item falling
	float fallSpeed = 5;
	float yAxisEnd;
	bool falling;
	public bool isFalling {
		get { return falling; }
		set { falling = value;}
	}

	// Use this for initialization
	void Start () {
		yAxisEnd = Random.Range(-2.5f, 4.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(falling && yAxisEnd < transform.position.y){
			transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + zOffset);
		} else { 
			falling = false;
		}
	}

	// Returns boolean for whether the item should be deleted or not
	public bool Use() {
		bool toBeDeleted = false;
		switch(type) {
			case ItemType.COOKEDGUPPY:
				toBeDeleted = true;
				hunger  = GameObject.Find("HungerBar");
				hunger.GetComponent<BarScript>().increment(0.15f);
				break;
			case ItemType.COOKEDTROUT:
				toBeDeleted = true;
				hunger  = GameObject.Find("HungerBar");
				hunger.GetComponent<BarScript>().increment(0.30f);
				break;
			case ItemType.COOKEDSALMON:
				toBeDeleted = true;
				hunger  = GameObject.Find("HungerBar");
				hunger.GetComponent<BarScript>().increment(0.45f);
				break;
			case ItemType.CARROT:
				toBeDeleted = true;
				hunger = GameObject.Find("HungerBar");
				hunger.GetComponent<BarScript>().increment(0.1f);
				break;
			case ItemType.POTATO:
				toBeDeleted = true;
				hunger = GameObject.Find("HungerBar");
				hunger.GetComponent<BarScript>().increment(0.2f);
				break;
			case ItemType.STRAWBERRIES:
				toBeDeleted = true;
				hunger = GameObject.Find("HungerBar");
				hunger.GetComponent<BarScript>().increment(0.3f);
				break;
			case ItemType.PINEAPPLE:
				toBeDeleted = true;
				hunger = GameObject.Find("HungerBar");
				thirst = GameObject.Find("ThirstBar");
				hunger.GetComponent<BarScript>().increment(0.3f);
				thirst.GetComponent<BarScript>().increment(0.1f);
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

