using UnityEngine;
using System.Collections;

public class follower_Pathfinding : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.GetComponent<followOrb_dataStorage>()) {
			Rigidbody rb = gameObject.GetComponent<Rigidbody>();
			followOrb_dataStorage temp = other.gameObject.GetComponent<followOrb_dataStorage>();
			rb.velocity = new Vector3((temp.next_node.x-transform.position.x), (temp.next_node.y-transform.position.y), (temp.next_node.z-transform.position.z));
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, temp.angle_to_next + 90.0f, transform.localEulerAngles.z);
			Destroy(other.gameObject);
		}
	}
}
