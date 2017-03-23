using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

	private Camera mainCamera;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
	}
	
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		Vector3 velocity = movement * speed;

		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength;

		if(groundPlane.Raycast(cameraRay, out rayLength)) {
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);

			transform.LookAt(pointToLook);
		}

		rb.velocity = velocity;
	}
}
