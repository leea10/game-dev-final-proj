using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour {
	public delegate void TreeMark();
	public static TreeMark OnTreeMark;

	bool marked = false;

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
			if (OnTreeMark != null) {
				OnTreeMark ();
			} else {
				Debug.Log ("[Landmark OnTreeMark] No delegates!");
			}
		}
	}
}
