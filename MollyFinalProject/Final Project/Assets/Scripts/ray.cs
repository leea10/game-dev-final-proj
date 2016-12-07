using UnityEngine;
using System.Collections;

public class ray : MonoBehaviour {
		ArrayList ar=new ArrayList();
		public GameObject player;
	// Use this for initialization
	void Start () {
				
		
	}
	
	// Update is called once per frame
	void Update () {
				

				//transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y+1, 0);
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
				for (int i = 0; i < 19; i++) {

						Vector3 temp = (Vector3)ar [i];
						if (Physics.Raycast (transform.position, temp, out hit, 100)) {
								Debug.DrawRay (transform.position, temp, Color.green);
								print (hit.collider.gameObject.name);


						}
				}


	}
}
