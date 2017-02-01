using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour {

	public GameObject item;
	public bool canBuild;
	public bool buildMode = true;

	private GameObject hoverObject;

	private string buildItem;
	private string placeItem;

	private Color buildColor = new Color (1,1,1, 0.5f);
	private Color cantBuild = new Color (1,0,0, 0.5f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(buildMode){
			if(hoverObject == null){
				hoverObject = Instantiate(Resources.Load<GameObject>("PlaceFire"));
				hoverObject.name = "Hover";
				hoverObject.layer = 2;
			}
			if(!hoverObject.GetComponent<Placeable>().colliding){
				canBuild = true;
				hoverObject.GetComponent<SpriteRenderer>().color = buildColor;
			}else{
				canBuild = false;
				hoverObject.GetComponent<SpriteRenderer>().color = cantBuild;
			}
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = pos.y - 0.3f;
			hoverObject.transform.position = pos;
			if (Input.GetMouseButtonDown(0)) {
				CastRay();
			}
		}
	}

	void CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1000);
		if(hit){
			if (hit.collider.gameObject.tag == "Build" && canBuild) {
				item = Instantiate(Resources.Load<GameObject>("Fire"));
				item.name = "Fire";
				item.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				item.transform.position = new Vector3 (item.transform.position.x, item.transform.position.y, item.transform.position.y + 0.2f);
				buildMode = false;
				Destroy(hoverObject);
			}
		}
	}

	public void build(string item, string placeItem){
		buildItem = item;
		this.placeItem = placeItem;
		buildMode = true;
	}

}
