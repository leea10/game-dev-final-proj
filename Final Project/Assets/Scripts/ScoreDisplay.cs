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
		total = GameObject.FindGameObjectsWithTag ("landmark").Length;
		UpdateScore (0);
	}
	
	void OnEnable() {
		LevelManager.OnObjectiveComplete += UpdateScore;
	}

	void OnDisable() {
		LevelManager.OnObjectiveComplete -= UpdateScore;
	}

	void UpdateScore(int treesMarked) {
		score.text = "Trees Marked: " + treesMarked + " / " + total;
	}
}
