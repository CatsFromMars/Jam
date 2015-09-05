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

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Destroy")
			destroySelf ();
	}

	public void destroySelf() {
		Destroy (this.gameObject);
	}
}
