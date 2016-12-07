using UnityEngine;
using System.Collections;

public class CompassLogic : MonoBehaviour {

	public GameObject[] objectives_array;

	// Use this for initialization
	void Start () {
		objectives_array = GameObject.FindGameObjectsWithTag("Objective");
		Landmark.OnTreeMark += UpdateObjectives;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject nearest_objective = get_nearest_objective();
		rotate_to_face(nearest_objective.transform);
	}

	GameObject get_nearest_objective () {
		if (objectives_array.Length == 0)
			return null;
		float nearest_object_distance = -1.0f;
		uint nearest_objective_index = 0;
		for (uint i = 0; i < objectives_array.Length; ++i) {
			float temp_object_distance = Mathf.Pow(Mathf.Pow(transform.position.x - objectives_array[i].transform.position.x, 2)
				+ Mathf.Pow(transform.position.z - objectives_array[i].transform.position.z, 2), 0.5f);
			if (temp_object_distance < nearest_object_distance || nearest_object_distance < 0) {
				nearest_objective_index = i;
				nearest_object_distance = temp_object_distance;
			}
		}
		return objectives_array[nearest_objective_index];
	}

	void rotate_to_face (Transform target) {
		transform.LookAt(target);
		transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
	}

	void OnDestroy() {
		Landmark.OnTreeMark -= UpdateObjectives;
	}

	void UpdateObjectives() {
		objectives_array = GameObject.FindGameObjectsWithTag("Objective");
	}
}
