using UnityEngine;
using System.Collections;

public class PlayerRaycaster : MonoBehaviour {
	public float interactDistance = 5.0f;

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, transform.forward * interactDistance);
	}

	void FixedUpdate () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, interactDistance)) {
			Debug.Log ("The player is facing something!");
		}
	}
}
