using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPopup : MonoBehaviour {

	public string info;

	public string image;

	private bool notOpened = true;
	GameObject popup;
	public GameObject canvas;
	private float flash = 1f;
	public int height;
public int width;
	// Use this for initialization
		void Start () {
			canvas = GameObject.Find("Canvas");
		}

		void Update()
		{
			flash -= Time.deltaTime;
			if(flash <= 0 && notOpened){
				if(image == "questionMark"){
					image = "questionMarkHL";
				}else{
					image = "questionMark";
				}
				gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(image);
				flash = 1f;
			}
		}

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
			notOpened = false;
		}
}
