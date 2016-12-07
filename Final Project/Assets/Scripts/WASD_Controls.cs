using UnityEngine;
using System.Collections;

public class WASD_Controls : MonoBehaviour {

	public Transform cameraBase;
	public GameObject playerModel;
	Animator anim;
	public GameObject compassFollow;
	public float moveSpeed = 1.0f;
	public float moveAccellerationTime = 1.0f;
	public float rotationDampener = 3.0f;
	public float dragCoefficient = 10.0f;
	Rigidbody rb;
	CapsuleCollider cc;
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
	public GameObject audioSpotlight;
	public float minimumLightHeight = -0.68f;
	public float maximumLightHeight = 0.70f;
	public float minimumLightIntensity = 1.0f;
	public float maximumLightIntensity = 5.0f;
	public float minimumLightAngle = 90.0f;
	public float maximumLightAngle = 120.0f;
	public GameObject audioPointlight;
	public float minimumPointIntensity = 0.5f;
	public float maximumPointIntensity = 1.35f;
	float maximum_xz_magnitude = 5f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		cc = GetComponent<CapsuleCollider>();
		anim = playerModel.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Forward and Backard Controls
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
			anim.SetBool ("walking", true);
			anim.SetBool ("backwards", false);
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
			playerModel.transform.localEulerAngles -= new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			compassFollow.transform.localEulerAngles -= new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			cameraBase.localEulerAngles = new Vector3(cameraBase.localEulerAngles.x, 0.0f, 0.0f);

			startingPositionX = Input.mousePosition.x;
		} else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) {
			anim.SetBool ("walking", true);
			anim.SetBool ("backwards", true);
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
			playerModel.transform.localEulerAngles -= new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			compassFollow.transform.localEulerAngles -= new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			cameraBase.localEulerAngles = new Vector3(cameraBase.localEulerAngles.x, 0.0f, 0.0f);

			startingPositionX = Input.mousePosition.x;
		} else {
			anim.SetBool ("walking", false);
			anim.SetBool ("backwards", false);
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
		if (xz_magnitude > maximum_xz_magnitude) {
			maximum_xz_magnitude = xz_magnitude;
		}
		audioSpotlight.transform.position = new Vector3(transform.position.x,
			transform.position.y + ((xz_magnitude)/maximum_xz_magnitude) * (maximumLightHeight-minimumLightHeight) + minimumLightHeight,
			transform.position.z);

		float audioSphere_scale = ((xz_magnitude)/maximum_xz_magnitude) * (7.0f - 3.0f) + 3.0f;
		audioSphere.transform.localScale = new Vector3(audioSphere_scale, audioSphere_scale, audioSphere_scale);
		Light light_component = audioSpotlight.GetComponent<Light>();
		light_component.intensity = ((xz_magnitude)/maximum_xz_magnitude) * (maximumLightIntensity-minimumLightIntensity) + minimumLightIntensity;
		light_component.spotAngle = ((xz_magnitude)/maximum_xz_magnitude) * (maximumLightAngle - minimumLightAngle) + minimumLightAngle;

		light_component = audioPointlight.GetComponent<Light>();
		light_component.intensity = ((xz_magnitude)/maximum_xz_magnitude) * (maximumPointIntensity-minimumPointIntensity) + minimumLightIntensity;
		playerModel.transform.localEulerAngles = new Vector3(playerModel.transform.localEulerAngles.x,
			Mathf.LerpAngle(playerModel.transform.localEulerAngles.y, 0.0f, Time.deltaTime * 10),
			playerModel.transform.localEulerAngles.z);

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			anim.SetBool ("crouching", true);
			cc.height = 1.5f;
			moveSpeed *= 0.6f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			anim.SetBool ("crouching", false);
			cc.height = 3.0f;
			moveSpeed /= 0.6f;
		}
		
	}

	void FixedUpdate () {

	}
}
