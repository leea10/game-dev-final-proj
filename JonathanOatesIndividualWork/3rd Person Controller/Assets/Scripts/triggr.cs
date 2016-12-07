using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggr : MonoBehaviour {
	private bool a=false;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other) {
		if (other.gameObject.name == "AudioSpere") {
			print ("hear "+other.gameObject.name);
			a = true;
		}
			
	}

}