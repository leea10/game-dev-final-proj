using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	// The areas to scout in the level.
	// (Mapping: Area name -> has been scouted?)
	Dictionary<string, bool> scoutAreas;

	void Start () {
		// Find all scout areas in the level.
		GameObject[] areas = GameObject.FindGameObjectsWithTag("ScoutArea");
		Debug.Log (areas.Length + " area to scout in this level");
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
