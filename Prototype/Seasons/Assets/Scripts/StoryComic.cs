using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryComic : MonoBehaviour {
	public Transform slide1;
	public Transform slide2;
	public Transform slide3;
	private bool slide1Active = true;
	private bool slide2Active = false;
	private bool slide3Active = false;
	private float startTime1;
	private bool startTimeSet1 = false;
	private float startTime2;
	private bool startTimeSet2 = false;
	private float startTime3;
	private bool startTimeSet3 = false;
	private AsyncOperation result;

	void Start () {
		result = SceneManager.LoadSceneAsync("Seasons");
		result.allowSceneActivation = false;
	}

	void Update () {
		if (Input.anyKeyDown) {
			result.allowSceneActivation = slide3Active;
			slide3Active = slide2Active;
			slide2Active = slide1Active;
		}
		if (slide1Active) {
			startTime1 = startTimeSet1 ? startTime1 : Time.time;
			startTimeSet1 = true;
			if ((Time.time - startTime1) < 1f) {
				slide1.GetComponent<SpriteRenderer>().color = new Color(1,1,1,Mathf.SmoothStep(0, 1, Time.time - startTime1));
			}
		}
		if (slide2Active) {
			startTime2 = startTimeSet2 ? startTime2 : Time.time;
			startTimeSet2 = true;
			if ((Time.time - startTime2) < 1f) {
				slide2.GetComponent<SpriteRenderer>().color = new Color(1,1,1,Mathf.SmoothStep(0, 1, Time.time - startTime2));
			}
		}
		if (slide3Active) {
			startTime3 = startTimeSet3 ? startTime3 : Time.time;
			startTimeSet3 = true;
			if ((Time.time - startTime3) < 1f) {
				slide3.GetComponent<SpriteRenderer>().color = new Color(1,1,1,Mathf.SmoothStep(0, 1, Time.time - startTime3));
			}
		}
	}
}
