using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Waves : MonoBehaviour {

	public Color c1;
	public int lengthOfLineRenderer = 20;
	float distanceFromPlayer;
	AudioSource sound;

	void Start() {

		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.material = new Material (Shader.Find("Particles/Additive (Soft)"));
		lineRenderer.startWidth = 0.4f;
		lineRenderer.endWidth = 0.4f;
		lineRenderer.numPositions = lengthOfLineRenderer;
		lineRenderer.numCornerVertices = 10;
		lineRenderer.numCapVertices = 10;
		lineRenderer.sortingLayerName = "Background";
		lineRenderer.sortingOrder = -1;
		sound = GetComponent<AudioSource>();

	}

	void Update() {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		BuoyancyEffector2D effector = GetComponent<BuoyancyEffector2D> ();
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.startColor = c1;
		lineRenderer.endColor = c1;
		EdgeCollider2D collider = GetComponent<EdgeCollider2D> ();
		var points = new Vector3[lengthOfLineRenderer];
		var points2D = new Vector2[lengthOfLineRenderer];
		var t = Time.time;
		for(int i = 0; i < lengthOfLineRenderer; i++) {
			points[i] = new Vector3(i * 0.5f, 0.15f * Mathf.Sin(i + 2*t), 0.0f);
			points2D[i] = new Vector2(i * 0.5f, 0.15f * Mathf.Sin(i + 2*t));
		}
		effector.surfaceLevel = points[(int)Math.Abs(Math.Ceiling(player.transform.position.x+7.3f))].y - 1.5f;
		lineRenderer.SetPositions(points);
		collider.points = points2D;

		//changing volume of sound
		distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
		if(distanceFromPlayer/45 > 0.7){
			sound.volume = (1 - distanceFromPlayer/45);
		} else {
			sound.volume = 0.3f;
		}		
	}
}
