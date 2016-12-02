using UnityEngine;
using System.Collections;

public class WASD_Controls : MonoBehaviour {

	public Transform cameraBase;
	public float moveSpeed = 1.0f;
	public float moveAccellerationTime = 1.0f;
	public float rotationDampener = 3.0f;
	public float dragCoefficient = 10.0f;
	Rigidbody rb;
	float acceleration_modifer;


	float startingPositionX;
	float startingRotationY;
	float startingPositionY;
	float startingRotationX;
	public float speedH = 4.0f;
	public float speedV = 4.0f;
	public float sp=0.1f;
	float yaw = 0.0f;
	float pitch = 0.0f;
	public Object t;

	public GameObject audioSphere;
	public GameObject audioLight;
	public float minimumLightAngle;
	public float maximumLightAngle;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		//Forward and Backard Controls
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
			Vector3 temp_v3 = rb.velocity;
			temp_v3.z = (moveSpeed * Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad)) / (moveAccellerationTime / acceleration_modifer);
			temp_v3.x = (moveSpeed * Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad)) / (moveAccellerationTime / acceleration_modifer);
			rb.velocity = temp_v3;
			if (acceleration_modifer < moveAccellerationTime) {
				acceleration_modifer += Time.deltaTime;
				if (acceleration_modifer > moveAccellerationTime)
					acceleration_modifer = moveAccellerationTime;
			}

			transform.localEulerAngles += new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			cameraBase.localEulerAngles = new Vector3(cameraBase.localEulerAngles.x, 0.0f, 0.0f);
			startingPositionX = Input.mousePosition.x;
		} else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) {
			Vector3 temp_v3 = rb.velocity;
			temp_v3.z = (-1 * moveSpeed * Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad)) / (moveAccellerationTime / acceleration_modifer); ;
			temp_v3.x = (-1 * moveSpeed * Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad)) / (moveAccellerationTime / acceleration_modifer); ;
			rb.velocity = temp_v3;
			if (acceleration_modifer < moveAccellerationTime) {
				acceleration_modifer += Time.deltaTime;
				if (acceleration_modifer > moveAccellerationTime)
					acceleration_modifer = moveAccellerationTime;
			}

			transform.localEulerAngles += new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			cameraBase.localEulerAngles = new Vector3(cameraBase.localEulerAngles.x, 0.0f, 0.0f);
			startingPositionX = Input.mousePosition.x;
		} else {
			Vector3 temp_v3 = rb.velocity;
			temp_v3.x /= 1 + (dragCoefficient * Time.deltaTime);
			temp_v3.z /= 1 + (dragCoefficient * Time.deltaTime);
			rb.velocity = temp_v3;
			acceleration_modifer = 0.0f;
		}
		
		//Rotation on Mouse Right Click/Hold (does not work while moving)
		if (Input.GetMouseButtonDown(1)) {
			startingPositionX = Input.mousePosition.x;
			startingRotationY = cameraBase.localEulerAngles.y;
			startingPositionY = Input.mousePosition.y;
			startingRotationX = cameraBase.localEulerAngles.x;
		} else if (Input.GetMouseButton(1)) {
			cameraBase.localEulerAngles = new Vector3(
				cameraBase.localEulerAngles.x,
				startingRotationY + ((Input.mousePosition.x - startingPositionX) / rotationDampener),
				cameraBase.localEulerAngles.z);
			cameraBase.localEulerAngles = new Vector3(
				startingRotationX - ((Input.mousePosition.y - startingPositionY) / rotationDampener),
				cameraBase.localEulerAngles.y,
				cameraBase.localEulerAngles.z);
		}
		Screen.lockCursor = true;

		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");
		if (pitch < -20) {
			pitch = -20;
		} else if (pitch > 50) {
			pitch = 50;
		}
		cameraBase.eulerAngles = new Vector3(pitch, yaw, 0.0f);

		float xz_magnitude = Mathf.Pow(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2), 0.5f);
		//audioSphere.transform.localScale = new Vector3(xz_magnitude, xz_magnitude, xz_magnitude);
		audioSphere.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
		Light light_component = audioLight.GetComponent<Light>();
		light_component.spotAngle = ((xz_magnitude)/moveSpeed) * (maximumLightAngle - minimumLightAngle) + minimumLightAngle;
	}

	void FixedUpdate () {

	}
}
