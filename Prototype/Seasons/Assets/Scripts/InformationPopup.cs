using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPopup : MonoBehaviour {

	public string info;

	GameObject popup;
	public GameObject canvas;
	public int height;
public int width;
	// Use this for initialization
		void Start () {
			canvas = GameObject.Find("Canvas");
		}
		
		// Update is called once per frame
		// public void Show(){
		// 	popup = Instantiate(Resources.Load<GameObject>(info));
		// 	popup.transform.SetParent(canvas.transform);
		// 	popup.GetComponent<RectTransform>().anchoredPosition = canvas.GetComponent<RectTransform>().anchoredPosition;
		// 	popup.transform.position = new Vector3(431.5f,323.5f);
		// }

		public void Hover(){
			popup = Instantiate(Resources.Load<GameObject>("Popup"));
			popup.name = "popup";
			gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("questionMarkHL");
			RectTransform tooltipRect = popup.GetComponent<RectTransform> ();
			tooltipRect.sizeDelta = new Vector2(width, height);
			Text popupText = popup.GetComponentInChildren<Text> ();
			popupText.text = info;
			RectTransform popupTextRect = popupText.gameObject.GetComponent<RectTransform> ();
			popupTextRect.sizeDelta = new Vector2 (width, height);
			popup.transform.SetParent(GameObject.Find("Canvas").transform, false);
			popup.transform.position = this.transform.position;
			popup.transform.position = new Vector3(popup.transform.position.x ,popup.transform.position.y - height/2 -20f , -50f);
		}

		public void Exit(){
			gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("questionMark");
			Destroy(popup.gameObject);
		}
}
