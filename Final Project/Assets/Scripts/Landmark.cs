using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour {
	public delegate void TreeMark();
	public static TreeMark OnTreeMark;

	public GameObject mark;
	bool marked = false;

	void Start() {
		mark.SetActive (false);
	}

	void OnEnable() {
		PlayerInteract.OnObjectInteract += PlayerInteractHandler;
	}

	void OnDisable() {
		PlayerInteract.OnObjectInteract -= PlayerInteractHandler;
	}

	void PlayerInteractHandler(GameObject obj) {
		if(this.gameObject == obj && !marked) {
			Debug.Log ("[Landmark OnTreeMark] Tree was marked.");
			marked = true;
			gameObject.tag = "Untagged";
			mark.SetActive (true);
			if (OnTreeMark != null) {
				OnTreeMark ();
			} else {
				Debug.Log ("[Landmark OnTreeMark] No delegates!");
			}
		}
	}
}
