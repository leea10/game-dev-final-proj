using UnityEngine;
using System.Collections;

public class followOrb_Prefab : MonoBehaviour {

	public GameObject followOrb_original;
	public float spawnRate;
	public float minimumNodeDistance = 1.0f;
	GameObject followOrb_clone;
	float previous_spawn;

	Vector3 previous_node = new Vector3(0.0f, 0.0f, 0.0f);
	followOrb_dataStorage previous_data = null;

	public Vector3 starting_node;
	public Vector3 second_node;
	public Vector3 stopping_node;
	uint nodes_spawned = 0;

	public GameObject follower_original;
	GameObject follower_clone;

	bool spawning = false;

	void Update () {
		if (Time.time - previous_spawn > spawnRate && spawning == true &&
			Mathf.Pow(Mathf.Pow(transform.position.x-previous_node.x, 2) + Mathf.Pow(transform.position.z-previous_node.z, 2), 0.5f) >= minimumNodeDistance)
		{
			spawn_orb();
			previous_spawn = Time.time;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			toggle_spawning();
		}
	}

	void spawn_orb () {
		followOrb_clone = Instantiate(followOrb_original, transform.position, Quaternion.identity) as GameObject;

		followOrb_dataStorage temp = followOrb_clone.GetComponent<followOrb_dataStorage>();
		temp.deltaTime = spawnRate;
		temp.previous_node = new Vector3(previous_node.x, previous_node.y, previous_node.z);

		if (previous_data != null) {
			if (transform.position.x - previous_node.x > 0 && transform.position.z - previous_node.z >= 0) {			//Quadrant 1
				temp.angle_to_prev = 90.0f - Mathf.Atan((transform.position.z - previous_node.z)/(transform.position.x - previous_node.x)) * Mathf.Rad2Deg;
			} else if (transform.position.x - previous_node.x >= 0 && transform.position.z - previous_node.z < 0) {		//Quadrant 2
				temp.angle_to_prev = 180.0f - Mathf.Atan(-1*(transform.position.z - previous_node.z)/(transform.position.x - previous_node.x)) * Mathf.Rad2Deg;
			} else if (transform.position.x - previous_node.x < 0 && transform.position.z - previous_node.z <= 0) {		//Quadrant 3
				temp.angle_to_prev = 270.0f - Mathf.Atan(-1*(transform.position.z - previous_node.z)/-1*(transform.position.x - previous_node.x)) * Mathf.Rad2Deg;
			} else {																									//Quadrant 4
				temp.angle_to_prev = 360.0f - Mathf.Atan((transform.position.z - previous_node.z)/-1*(transform.position.x - previous_node.x)) * Mathf.Rad2Deg;
			}
			temp.angle_to_prev = (temp.angle_to_prev - 180.0f) % 360;

			previous_data.angle_to_next = temp.angle_to_prev + 180.0f;
			previous_data.next_node = new Vector3(transform.position.x, transform.position.y, transform.position.z);

			if (nodes_spawned == 2) {
				second_node = previous_node;
			}
		} else {
			starting_node = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		}

		previous_node = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		previous_data = temp;

		nodes_spawned += 1;
	}

	void spawn_follower () {
		follower_clone = Instantiate(follower_original, starting_node, Quaternion.identity) as GameObject;
		Rigidbody rb = follower_clone.GetComponent<Rigidbody>();
		rb.velocity = new Vector3((second_node.x-starting_node.x), (second_node.y-starting_node.y), (second_node.z-starting_node.z));
	}

	void toggle_spawning () {
		if (spawning == false) {
			previous_spawn = Time.time;
			spawning = true;
			previous_data = null;
			nodes_spawned = 0;
		} else {
			spawning = false;
			stopping_node = new Vector3(previous_node.x, previous_node.y, previous_node.z);
			spawn_follower();
		}
	}
}
