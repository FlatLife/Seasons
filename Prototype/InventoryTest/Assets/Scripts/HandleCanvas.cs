using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleCanvas : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject itemPrefab2;
	private CanvasScaler scaler;

	void Start () {
		scaler = GetComponent<CanvasScaler>();
		scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

		//Instantiating test item
		GameObject itemImage = (GameObject)Instantiate(itemPrefab);
		Transform itemImageRect = itemImage.GetComponent<Transform>();
		itemImageRect.transform.SetParent(this.transform.parent);
		itemImageRect.localPosition = new Vector3(500, 162);

		GameObject itemImage2 = (GameObject)Instantiate(itemPrefab2);
		Transform itemImageRect2 = itemImage2.GetComponent<Transform>();
		itemImageRect2.transform.SetParent(this.transform.parent);
		itemImageRect2.localPosition = new Vector3(100, 162);
	}
}
