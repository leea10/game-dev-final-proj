using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPrompt : MonoBehaviour {
	void Start () {
		hide (null);
		PlayerInteract.OnObjectInReach += display;
		PlayerInteract.OnObjectOutReach += hide;
	}

	void display(GameObject g_) {
		this.gameObject.SetActive (true);
	}

	void hide(GameObject g_) {
		this.gameObject.SetActive (false);
	}

	void OnDestroy() {
		PlayerInteract.OnObjectInReach -= display;
		PlayerInteract.OnObjectOutReach -= hide;	
	}
}
