using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	// Event dispatcher for player progress events.
	// When the playuer claims one objective.
	public delegate void ObjectiveCompleteAction(string areaName);
	public static ObjectiveCompleteAction OnObjectiveComplete;
	// When the player completes the entire level.
	public delegate void LevelCompleteAction();
	public static LevelCompleteAction OnLevelComplete;

	// The areas to scout in the level.
	// (Mapping: Area name -> has been scouted?)
	Dictionary<string, bool> scoutAreas;

	// The trees to mark in the leve.
	int totalTrees;
	int treesMarked = 0;

	void Start () {
		// Initialize variables.
		scoutAreas = new Dictionary<string, bool> ();
		// Find all scout areas in the level.
		GameObject[] areaObjs = GameObject.FindGameObjectsWithTag("ScoutArea");
		Debug.Log ("[LevelManager Init] " + areaObjs.Length + " areas to scout in this level");
		// Populate scoutAreas dictionary with scout areas found in level.
		foreach (GameObject areaObj in areaObjs) {
			string areaName = areaObj.GetComponent<ScoutArea> ().areaName;
			scoutAreas.Add (areaName, false);
			Debug.Log ("[LevelManager Init] Player must scout " + areaName);
		}
		// Find all trees that need to be marked.
		totalTrees = GameObject.FindGameObjectsWithTag("landmark").Length;
		Debug.Log ("[LevelManager Init] Player must mark " + totalTrees + " trees.");
	}

	void OnEnable () {
		// Event Listeners.
		ScoutArea.OnPlayerCollide += ScoutAreaHandler; // Player enters a scout area.
		Landmark.OnTreeMark += MarkTreeHandler;
	}

	void OnDisable () {
		// Stop listening to events.
		ScoutArea.OnPlayerCollide -= ScoutAreaHandler;
		Landmark.OnTreeMark -= MarkTreeHandler;
	}

	// Event handler for when player enters a scout area.
	void ScoutAreaHandler (string areaName) {
		// If the area is a required area that hasn't been scouted yet, note that the player just scouted it.
		if (scoutAreas.ContainsKey(areaName) && !scoutAreas[areaName]) {
			scoutAreas[areaName] = true;
			Debug.Log ("[LevelManager OnObjectiveComplete] Player scouted " + areaName);
			if (OnObjectiveComplete != null) {
				OnObjectiveComplete (areaName);
			}
		}
	}

	// Event handler for when player marks a tree.
	void MarkTreeHandler() {
		treesMarked++;
		Debug.Log ("[LevelManager OnLandmarkComplete] Player marked tree " + treesMarked + "/" + totalTrees);
		if (treesMarked == totalTrees) {
			if (OnLevelComplete != null) {
				Debug.Log ("[LevelManager OnLevelComplete] Player completed the level");
				OnLevelComplete ();
			} else {
				Debug.Log ("[LevelManager OnLevelComplete] No event delegates!");
			}
		}
	}
}
