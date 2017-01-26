using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Waves : MonoBehaviour {

	public Color c1;
	public int lengthOfLineRenderer = 20;

	void Start() {

		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.material = new Material (Shader.Find("Particles/Additive (Soft)"));
		lineRenderer.startWidth = 1.2f;
		lineRenderer.endWidth = 1.2f;
		lineRenderer.numPositions = lengthOfLineRenderer;
		lineRenderer.numCornerVertices = 10;
		lineRenderer.numCapVertices = 10;
		lineRenderer.sortingLayerName = "Water";

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
		effector.surfaceLevel = points[(int)Math.Ceiling(player.transform.position.x+7.3f)].y - 1;
		lineRenderer.SetPositions(points);
		collider.points = points2D;
	}
}
