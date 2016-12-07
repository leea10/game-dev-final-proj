using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour {
	Text score;
	int total;

	// Use this for initialization
	void Start () {
		score = GetComponent<Text> ();
		total = GameObject.FindGameObjectsWithTag ("Objective").Length;
		UpdateScore (0);
	}
	
	void OnEnable() {
		LevelManager.OnObjectiveComplete += UpdateScore;
	}

	void OnDisable() {
		LevelManager.OnObjectiveComplete -= UpdateScore;
	}

	void UpdateScore(int treesMarked) {
		score.text = "Marks Placed: " + treesMarked + " / " + total;
	}
}
