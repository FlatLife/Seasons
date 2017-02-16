using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingAnimation : MonoBehaviour {
	public Transform head;
	public Transform torso;
	public Transform legs;
	public float rotationSpeed=0.2f;
	private float oldAngle;
	private float angleDifference = 0;
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontalInput = Input.GetAxis("Horizontal");
	  	float verticalInput = Input.GetAxis("Vertical");
		float angle = (Mathf.Atan2(horizontalInput, verticalInput) * (180 / Mathf.PI)) * -1;
		head.localRotation = Quaternion.Slerp(head.localRotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 2);

		Debug.Log(angleDifference);
		if (angle != oldAngle) {
			angleDifference += oldAngle - angle;
			angleDifference %= 170;
		}
		//angleDifference = angleDifference > 120 ? 120 : angleDifference < -45 ? -45 : angleDifference;
		angleDifference += angleDifference > 1 ? -1 : angleDifference < 1 ? 1 : 0;
		legs.localRotation = Quaternion.Slerp(legs.localRotation, Quaternion.Euler(0,0,angleDifference), Time.deltaTime);
		oldAngle = angle;

		head.GetComponent<SpriteRenderer>().flipX = horizontalInput < 0;
		torso.GetComponent<SpriteRenderer>().flipX = horizontalInput < 0;
		legs.GetComponent<SpriteRenderer>().flipX = horizontalInput < 0;
	}
}