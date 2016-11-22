using UnityEngine;
using System.Collections;

public class WASD_Controls : MonoBehaviour {

	public Transform cameraBase;
	public float moveSpeed = 1.0f;
	public float rotationDampener = 3.0f;
	public float dragCoefficient = 10.0f;

	Rigidbody rb;
	float startingPositionX;
	float startingRotationY;
	float startingPositionY;
	float startingRotationX;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		//Forward and Backard Controls
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
			Vector3 temp_v3 = rb.velocity;
			temp_v3.z = moveSpeed * Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad);
			temp_v3.x = moveSpeed * Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad);
			rb.velocity = temp_v3;

			transform.localEulerAngles += new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			cameraBase.localEulerAngles = new Vector3(cameraBase.localEulerAngles.x, 0.0f, 0.0f);
			startingPositionX = Input.mousePosition.x;
		} else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) {
			Vector3 temp_v3 = rb.velocity;
			temp_v3.z = -1 * moveSpeed * Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad);
			temp_v3.x = -1 * moveSpeed * Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad);
			rb.velocity = temp_v3;

			transform.localEulerAngles += new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			cameraBase.localEulerAngles = new Vector3(cameraBase.localEulerAngles.x, 0.0f, 0.0f);
			startingPositionX = Input.mousePosition.x;
		}
		//Strafe Left and Strafe Right Controls
		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
			Vector3 temp_v3 = rb.velocity;
			temp_v3.z = moveSpeed * Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad);
			temp_v3.x = -1 * moveSpeed * Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad);
			rb.velocity = temp_v3;

			transform.localEulerAngles += new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			cameraBase.localEulerAngles = new Vector3(cameraBase.localEulerAngles.x, 0.0f, 0.0f);
			startingPositionX = Input.mousePosition.x;
		} else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
			Vector3 temp_v3 = rb.velocity;
			temp_v3.z = -1 * moveSpeed * Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad);
			temp_v3.x = moveSpeed * Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad);
			rb.velocity = temp_v3;

			transform.localEulerAngles += new Vector3(0.0f, cameraBase.localEulerAngles.y, 0.0f);
			cameraBase.localEulerAngles = new Vector3(cameraBase.localEulerAngles.x, 0.0f, 0.0f);
			startingPositionX = Input.mousePosition.x;
		}
		//Deceleration on No Input
		if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
			Vector3 temp_v3 = rb.velocity;
			temp_v3.x /= 1 + (dragCoefficient * Time.deltaTime);
			temp_v3.z /= 1 + (dragCoefficient * Time.deltaTime);
			rb.velocity = temp_v3;
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
	}

	void FixedUpdate () {

	}
}
