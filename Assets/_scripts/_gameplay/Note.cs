using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
	GameController controller;
	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector3.left*(controller.bpm/controller.damp)*Time.deltaTime);
	}
}
