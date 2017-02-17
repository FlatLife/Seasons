using UnityEngine;


public enum ItemType {NONE, FISHINGROD, STICK, WOOD, VINE, RAWFISH, COOKEDFISH, BURNTFISH, ROCK,
	ICE, HATCHET, BUCKET, CARROTSEED, HOE, SALTWATER, FRESHWATER, CARROT, CLOTHES, FIREPREP, BOTTLE, 
	WATERPURIFIER, SEAWEED, POTATOSEED, PINEAPPLESEED, STRAWBERRYSEED, POTATO, PINEAPPLE, STRAWBERRIES,
	RAWTROUT, RAWSALMON, RAWGUPPY, COOKEDTROUT, COOKEDSALMON, COOKEDGUPPY, BAKEDPOTATO, ROASTEDCARROT,
	BARREL, SUSHI };


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
	private BoxCollider2D colliderBox;
	private GameObject hunger;
	private GameObject thirst;

	public GameObject scroll;
	private PlaceObjects builder;
	public bool keepItem;

	public bool isSwimming;
	private int durability;
	public int Durability {
		get { return durability; }
		set { durability = value;}
	}
	public int maxDurability;
	public int cookTime;
	public int growTime;
	public string nextItem;
	public float zOffset;

	// Variables for item falling
	float fallSpeed = 5;
	float yAxisEnd;
	bool falling;
	private Transform shadow;
	private float shadowPositionY;
	// Use this for initialization
	public void InitializeFall () {
		yAxisEnd = Random.Range(-2.5f, 4.5f);
		shadow = transform.Find("Shadow");
		if (shadow != null) {
			shadow.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			shadowPositionY = shadow.transform.localPosition.y;
			shadow.transform.position = new Vector3(transform.position.x, yAxisEnd, 0);
			shadow.transform.localPosition = new Vector3(shadow.transform.localPosition.x, shadow.transform.localPosition.y + shadowPositionY, shadow.transform.localPosition.z);
		}
		falling = true;
	}
	
	public void Awake() {
		durability = maxDurability;
	}

	// Update is called once per frame
	void Update () {
		
		if(falling && yAxisEnd < transform.position.y){
			transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, transform.position.y, yAxisEnd + zOffset);
			if (shadow != null) {
				shadow.transform.Translate(Vector3.up * fallSpeed * Time.deltaTime);
				shadow.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1/(transform.position.y-shadow.position.y));
			}
		} else if (falling) { 
			falling = false;
			transform.position = new Vector3(transform.position.x, yAxisEnd, transform.position.z);
			if (shadow != null) {
				shadow.localPosition = new Vector3(shadow.localPosition.x, shadowPositionY, shadow.localPosition.z);
			}
		}

		if(shadow != null && shadow.gameObject.GetComponent<SpriteRenderer>().enabled == false){
			shadow.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
	}


	// Returns boolean for whether the item should be deleted or not
	public bool Use() {
		hunger = GameObject.Find("Hunger");
		thirst = GameObject.Find("Thirst");
		bool toBeDeleted = false;
		switch(type) {
			case ItemType.COOKEDGUPPY:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.15f);
				break;
			case ItemType.COOKEDTROUT:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.30f);
				break;
			case ItemType.COOKEDSALMON:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.40f);
				break;
			case ItemType.SUSHI:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.45f);
				thirst.GetComponent<BarScript>().increment(0.1f);
				break;
			case ItemType.RAWGUPPY:
				toBeDeleted = true;
				thirst.GetComponent<BarScript>().increment(0.05f);
				break;
			case ItemType.RAWTROUT:
				toBeDeleted = true;
				thirst.GetComponent<BarScript>().increment(0.05f);
				break;
			case ItemType.RAWSALMON:
				toBeDeleted = true;
				thirst.GetComponent<BarScript>().increment(0.05f);
				break;
			case ItemType.CARROT:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.1f);
				break;
			case ItemType.ROASTEDCARROT:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.2f);
				break;
			case ItemType.POTATO:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.2f);
				break;
			case ItemType.BAKEDPOTATO:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.35f);
				break;
			case ItemType.STRAWBERRIES:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.3f);
				break;
			case ItemType.PINEAPPLE:
				toBeDeleted = true;
				hunger.GetComponent<BarScript>().increment(0.3f);
				thirst.GetComponent<BarScript>().increment(0.1f);
				break;
			case ItemType.FIREPREP:
			isSwimming = GameObject.Find ("Player").GetComponent<Player> ().isSwimming;
				if(!isSwimming){
					toBeDeleted = true;
					builder = GameObject.Find("Main Camera").GetComponent<PlaceObjects>();
					builder.build("Fire", "PlaceFire");
				}
				break;
			case ItemType.HOE:
				isSwimming = GameObject.Find ("Player").GetComponent<Player> ().isSwimming;
				if(!isSwimming){
					builder = GameObject.Find("Main Camera").GetComponent<PlaceObjects>();
					builder.build("Farm", "PlaceFarm");
					durability--;
					if (durability == 0) {
						toBeDeleted = true;
					}
				}
				break;
			case ItemType.FRESHWATER:
				thirst.GetComponent<BarScript>().increment(0.3f);
				slot.DestroyItem ();
				newItem = Instantiate(Resources.Load<GameObject>("Bucket").GetComponent<Item>(), slot.transform);
				colliderBox = newItem.gameObject.GetComponent<BoxCollider2D>();
				if(colliderBox != null){
					colliderBox.enabled = false;
				}
				newItem.transform.position = new Vector3 (0,20f,0);
				newItem.GetComponent<SpriteRenderer>().enabled = false;
				slot.AddItem(newItem);
				break;
			case ItemType.SALTWATER:
				slot.DestroyItem ();
				newItem = Instantiate(Resources.Load<GameObject>("Bucket").GetComponent<Item>(), slot.transform);
				colliderBox = newItem.gameObject.GetComponent<BoxCollider2D>();
				if(colliderBox != null){
					colliderBox.enabled = false;
				}
				newItem.transform.position = new Vector3 (0,20f,0);
				newItem.GetComponent<SpriteRenderer>().enabled = false;
				slot.AddItem(newItem);

				break;
			case ItemType.WATERPURIFIER:
				isSwimming = GameObject.Find ("Player").GetComponent<Player> ().isSwimming;
				if(!isSwimming){
					toBeDeleted = true;
					builder = GameObject.Find("Main Camera").GetComponent<PlaceObjects>();
					builder.build("WaterPurifier", "PlaceWater");
				}
				break;
			case ItemType.BARREL:
				isSwimming = GameObject.Find ("Player").GetComponent<Player> ().isSwimming;
				if(!isSwimming){
					toBeDeleted = true;
					builder = GameObject.Find("Main Camera").GetComponent<PlaceObjects>();
					builder.build("Barrel", "PlaceBarrel");
				}
				break;
			case ItemType.BUCKET:
				bool nearWater = GameObject.Find ("Player").GetComponent<Player> ().switchSwimMode;
				if (nearWater) {
				slot.DestroyItem ();
				newItem = Instantiate(Resources.Load<GameObject>("SaltWater").GetComponent<Item>(), slot.transform);
				colliderBox = newItem.gameObject.GetComponent<BoxCollider2D>();
				if(colliderBox != null){
					colliderBox.enabled = false;
				}
				newItem.transform.position = new Vector3 (0,20f,0);
				newItem.GetComponent<SpriteRenderer>().enabled = false;
				slot.AddItem(newItem);
				}
				break;
			case ItemType.BOTTLE:
				toBeDeleted = false;
				GameObject canvas = GameObject.Find ("Canvas");
				if(scroll == null){
					scroll = Instantiate (Resources.Load<GameObject> (message), canvas.transform);
					scroll.transform.localPosition = new Vector3 (0, 0);
				}
				break;
		}
		return toBeDeleted;
	}
}

