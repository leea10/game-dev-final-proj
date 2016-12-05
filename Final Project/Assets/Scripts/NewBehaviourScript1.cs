using UnityEngine;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {
	int i=0;
	int inc = 1;
	public Transform[] targets;
	public float speed;
	public GameObject player;

	void Update() {
			
		float step = speed * Time.deltaTime;
		Vector3 targetDir = targets[i].position - transform.position;
		float step2 = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step2, 0.0F);
	
		transform.rotation = Quaternion.LookRotation(newDir);
		transform.position = Vector3.MoveTowards(transform.position, targets[i].position, step);
		if (transform.position == targets [i].position || Vector3.Distance(targets[i].position, transform.position)<=1.0f) {
			if (i == targets.Length - 1)
				inc = -1;
			else if (i == 0)
				inc = 1;
			i += inc;
		}
	}
}
