using UnityEngine;
using System.Collections;

public class followOrb_dataStorage : MonoBehaviour {
	public float deltaTime = 0.1f;
	public Vector3 previous_node;
	public Vector3 next_node;
	public bool starting_node = false;
	public bool stopping_node = false;
	public float angle_to_next = -1.0f;
	public float angle_to_prev;
}
