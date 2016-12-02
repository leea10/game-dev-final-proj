using UnityEngine;
using System.Collections;

public class ScoutAreaTrigger : MonoBehaviour {
	public string areaName;
	
	void OnTriggerEnter(Collider other) {
		Debug.Log (areaName);
	}
}
