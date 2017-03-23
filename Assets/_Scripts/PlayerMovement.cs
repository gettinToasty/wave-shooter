using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	public float fireDelta;
	public GameObject projectile;

	private Rigidbody rb;

	private Camera mainCamera;
	private float nextFire = 0.5f;
	private float myTime = 0.0f;
	private GameObject newProjectile;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
	}

	void Update() {
		myTime = myTime + Time.deltaTime;

		if(Input.GetButton("Fire1") && myTime > nextFire) {
			nextFire = myTime + fireDelta;
			newProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;

			nextFire = nextFire - myTime;
			myTime = 0.0f;
		}
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

			Vector3 playerLook = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);

			transform.LookAt(playerLook);
		}

		rb.velocity = velocity;
	}
}
