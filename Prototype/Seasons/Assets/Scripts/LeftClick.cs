using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class LeftClick : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler// required interface when using the OnPointerDown method.
{

	public GameObject canvas;
	public GameObject craft;
	public GameObject backpack;

	void Awake(){
		canvas = GameObject.Find ("Canvas");
		craft = GameObject.Find ("Canvas/CraftTableTest");
		backpack = GameObject.Find ("Canvas/Backpack");
	}	
	//Do this when the mouse is clicked over the selectable object this script is attached to.
	public void OnPointerClick (PointerEventData eventData) 
	{
		if(eventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift)){
			Item item = gameObject.GetComponent<Slot>().CurrentItem;
			CraftTable table = craft.GetComponent<CraftTable> ();
			if (gameObject.GetComponent<Slot>().canShift) {
				if (table.Slot1.GetComponent<Slot> ().isEmpty) {
					eventData.pointerPressRaycast.gameObject.GetComponentInParent<Backpack> ().MoveItem (gameObject);
					eventData.pointerPressRaycast.gameObject.GetComponentInParent<Backpack> ().MoveItem (table.Slot1);
				} else if (table.Slot2.GetComponent<Slot> ().isEmpty) {
					eventData.pointerPressRaycast.gameObject.GetComponentInParent<Backpack> ().MoveItem (gameObject);
					eventData.pointerPressRaycast.gameObject.GetComponentInParent<Backpack> ().MoveItem (table.Slot2);
				}
			} else {
					canvas.GetComponentInChildren<Backpack> ().MoveItem (gameObject);
				}
		}else if(eventData.button == PointerEventData.InputButton.Left){
			canvas.GetComponentInChildren<Backpack> ().MoveItem(gameObject);
		}

	}

	public void OnBeginDrag (PointerEventData eventData) 
	{
		if(eventData.button == PointerEventData.InputButton.Left){
			canvas.GetComponentInChildren<Backpack> ().MoveItem(gameObject);
		}
	}

	public void OnEndDrag (PointerEventData eventData){
		if(eventData.button == PointerEventData.InputButton.Left){
			if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>() != null) {
				canvas.GetComponentInChildren<Backpack> ().MoveItem (eventData.pointerCurrentRaycast.gameObject);
			}
		}
	}
}