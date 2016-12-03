using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	void Start () {
		// Find all scout areas in the level.
	}

	void OnEnable () {
		// Event Listeners.
		ScoutArea.OnPlayerCollide += TestPrint; // Player enters a scout area.
	}

	void OnDisable () {
		// Stop listening to events.
		ScoutArea.OnPlayerCollide -= TestPrint;
	}

	// Event handler for when player enters a scout area.
	void TestPrint (string s) {
		Debug.Log ("Level manager detected that player collided with " + s);
	}
}
