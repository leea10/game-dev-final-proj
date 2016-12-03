using UnityEngine;
using System.Collections;

public class ScoutArea : MonoBehaviour {
	public string areaName;

	public delegate void PlayerCollideAction(string areaName);
	public static event PlayerCollideAction OnPlayerCollide;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "PlayerModel") {
			Debug.Log ("[ScoutArea OnPlayerCollide] Player collided with " + areaName);
			if (OnPlayerCollide != null) {
				OnPlayerCollide (areaName);
			} else {
				Debug.Log ("[ScoutArea OnPlayerCollide] No event delegates!");
			}
		}
	}
}
