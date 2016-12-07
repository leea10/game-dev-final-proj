using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyDetectionLogic : MonoBehaviour {
	int i = 0;
	public Image image;
	int inc = 1;
	public Transform[] targets;
	public float moveSpeed = 3.0f;
	float moveSpeed_saved;
	ArrayList ar = new ArrayList();
	bool bule = false;

	void Start () {
		moveSpeed_saved = moveSpeed;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "PlayerModel")
			bule = true;
	}
	void OnTriggerExit (Collider other) {
		if (other.gameObject.name == "PlayerModel")
			bule = false;
	}

	void Update () {
		ar.Clear();
		RaycastHit hit;
		for (int j = -2; j < 3; ++j) {
			Vector3 temp_vector = transform.TransformDirection(Vector3.forward + new Vector3(j * 0.25f, 0, 0)) * 100;
			ar.Add(temp_vector);
		}
		for (int j = -2; j < 3; ++j) {
			Vector3 temp_vector = transform.TransformDirection(Vector3.forward + new Vector3(j * 0.25f, 1, 0)) * 100;
			ar.Add(temp_vector);
		}
		for (int j = -2; j < 3; ++j) {
			Vector3 temp_vector = transform.TransformDirection(Vector3.forward + new Vector3(j * 0.25f, -1, 0)) * 100;
			ar.Add(temp_vector);
		}
		for (int j = -2; j < 3; ++j) {
			Vector3 temp_vector = transform.TransformDirection(Vector3.forward + new Vector3(j * 0.25f, 0.5f, 0)) * 100;
			ar.Add(temp_vector);
		}
		for (int j = -2; j < 3; ++j) {
			Vector3 temp_vector = transform.TransformDirection(Vector3.forward + new Vector3(j * 0.25f, -0.5f, 0)) * 100;
			ar.Add(temp_vector);
		}
		moveSpeed = moveSpeed_saved;
		bool temp_bool = false;
		for (int j = 0; j < 20; ++j) {
			Vector3 temp_vector = (Vector3) ar[j];
			if (Physics.Raycast(transform.position, temp_vector, out hit, 30)) {
				if (hit.collider.gameObject.name == "PlayerModel" || bule == true) {
					moveSpeed = 0;
					Color temp_color = image.color;
					temp_color.a += Time.deltaTime / 2.0f;
					if (temp_color.a > 0.8f) {
						Scene temp_scene = SceneManager.GetActiveScene();
						SceneManager.LoadScene(temp_scene.name);
					}
					image.color = temp_color;
					temp_bool = true;
				}
			}
		}
		if (temp_bool == false) {
			Color temp_color = image.color;
			temp_color.a -= Time.deltaTime / 2.0f;
			if (temp_color.a < 0.0f)
				temp_color.a = 0.0f;
			image.color = temp_color;
		}

		float step = moveSpeed * Time.deltaTime;
		Vector3 targetDirection = targets[i].position - transform.position;
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
		newDirection.y = 0.0f;

		transform.rotation = Quaternion.LookRotation(newDirection);
		transform.position = Vector3.MoveTowards(transform.position, targets[i].position, step);
		if (transform.position == targets[i].position || Vector3.Distance(targets[i].position, transform.position) <= 1.0f) {
			if (i == targets.Length - 1)
				inc = -1;
			else if (i == 0)
				inc = 1;
			i += inc;
		}
	}
}