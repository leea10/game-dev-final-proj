using UnityEngine;
using System.Collections;

public class ScoutAreaTrigger : MonoBehaviour {
	public string areaName;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "PlayerModel") {
			Debug.Log ("Player collided with " + areaName);
		}
	}
}
