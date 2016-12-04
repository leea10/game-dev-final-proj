using UnityEngine;
using System.Collections;

public class PlayerRaycaster : MonoBehaviour {
	// Event dispatcher for when player comes in or out of reach of an interactable object.
	// When the player can reach an interactable object.
	public delegate void ObjectInReachAction(GameObject reachObj);
	public static ObjectInReachAction OnObjectInReach;
	// When the player can no longer reach the interactable object.
	public delegate void ObjectOutReachAction(GameObject reachObj);
	public static ObjectOutReachAction OnObjectOutReach;

	// The player's max interact distance.
	public float interactDistance = 5.0f;
	// The game object that the player is in reach of, according to the raycast.
	GameObject reachObj = null;

	void FixedUpdate () {
		RaycastHit hit;
		// Check if we are in reach of an object.
		if (Physics.Raycast (transform.position, transform.forward, out hit, interactDistance)) {
			GameObject newReachObj = hit.collider.gameObject;
			// Check if the object we can reach is a new object.
			if (newReachObj != reachObj) {
				unsetReachObj ();
				if (newReachObj.GetComponent<Interactable> () != null) {
					// The player is in reach of a new interactable object.
					reachObj = newReachObj;
					Debug.Log ("[PlayerRaycaster OnObjectInReach] " + reachObj.name + " came into reach of player");
					if (OnObjectInReach != null) {
						OnObjectInReach (reachObj);
					} else {
						Debug.Log ("[PlayerRaycaster OnObjectInReach] No delegates!");
					}
				}
			}
		} else {
			unsetReachObj ();
		}
	}
		
	void unsetReachObj() {
		if (reachObj != null) {
			// The player went out of reach from the current object.
			Debug.Log ("[PlayerRaycaster OnObjectOutReach] " + reachObj.name + " went out of reach from player" );
			if (OnObjectOutReach != null) {
				OnObjectOutReach (reachObj);
			} else {
				Debug.Log ("[PlayerRaycaster OnObjectOutReach] No delegates!");
			}
			reachObj = null;
		}		
	}

	// Gizmos
	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, transform.forward * interactDistance);
	}
}
