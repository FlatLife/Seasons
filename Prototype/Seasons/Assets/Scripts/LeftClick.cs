using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class LeftClick : MonoBehaviour, IPointerDownHandler// required interface when using the OnPointerDown method.
{

	public GameObject canvas;

	void Awake(){
		canvas = GameObject.Find ("Canvas");
	}	
	//Do this when the mouse is clicked over the selectable object this script is attached to.
	public void OnPointerDown (PointerEventData eventData) 
	{
		if(eventData.button == PointerEventData.InputButton.Left){
			Debug.Log (gameObject);
			canvas.GetComponentInChildren<Backpack> ().MoveItem(gameObject);
		}
	}
}