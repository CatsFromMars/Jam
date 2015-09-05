using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
	public Transform hitEffect;
	GameController controller;
	float rate;
	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		rate = controller.bpm / controller.damp;
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector3.left*(rate)*Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Destroy")
			Destroy (this.gameObject);
	}

	public void destroySelf() {
		Instantiate (hitEffect, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}
