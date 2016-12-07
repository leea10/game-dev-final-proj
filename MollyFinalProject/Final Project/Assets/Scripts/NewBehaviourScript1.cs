using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {
	int i=0;
	public Image img;
	int inc = 1;
	public Transform[] targets;
	public float speed;
	private float speedo;
	public GameObject player;
	ArrayList ar=new ArrayList();
	void Start(){
		speedo = speed;
		
	}
	void Update() {
		ar.Clear ();	
		RaycastHit hit;
		for (int i = -2; i < 3; i++) {
			Vector3 forward = transform.TransformDirection (Vector3.forward+new Vector3(i*0.25f,0,0))*100;
			Debug.DrawRay (transform.position, forward, Color.green);
			ar.Add (forward);
		}
		for (int i = -2; i < 3; i++) {
			Vector3 forward = transform.TransformDirection (Vector3.forward+new Vector3(i*0.25f,1,0))*100;
			Debug.DrawRay (transform.position, forward, Color.green);
			ar.Add (forward);
		}
		for (int i = -2; i < 3; i++) {
			Vector3 forward = transform.TransformDirection (Vector3.forward+new Vector3(i*0.25f,-1,0))*100;
			Debug.DrawRay (transform.position, forward, Color.green);
			ar.Add (forward);
		}
		for (int i = -2; i < 3; i++) {
			Vector3 forward = transform.TransformDirection (Vector3.forward+new Vector3(i*0.25f,0.5f,0))*100;
			Debug.DrawRay (transform.position, forward, Color.green);
			ar.Add (forward);
		}
		for (int i = -2; i < 3; i++) {
			Vector3 forward = transform.TransformDirection (Vector3.forward+new Vector3(i*0.25f,-0.5f,0))*100;
			Debug.DrawRay (transform.position, forward, Color.green);
			ar.Add (forward);
		}
		speed = speedo;
		for (int i = 0; i < 19; i++) {

			Vector3 temp = (Vector3)ar [i];
			if (Physics.Raycast (transform.position, temp, out hit, 100)) {
				Debug.DrawRay (transform.position, temp, Color.green);
				if (hit.collider.gameObject.name == "PlayerModel") {
					speed = 0;
				}





			}
		}
			
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
