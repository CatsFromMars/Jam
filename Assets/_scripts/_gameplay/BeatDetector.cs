using UnityEngine;
using System.Collections;

public class BeatDetector : MonoBehaviour {
	private Transform currentNote;

	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "ONote" || other.gameObject.tag == "PNote") currentNote = other.gameObject.transform;
		Debug.Log (other.name);
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "ONote" || other.gameObject.tag == "PNote") currentNote = null;
	}

	public void hitBeat(string tag) {
		if (currentNote != null && currentNote.tag == tag) {
			Debug.Log ("You did it!");
			Destroy (currentNote.gameObject);
			currentNote = null;
		} else
			Debug.Log ("BOOOOO");
	}

	//Debug Controls
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.A))
			hitBeat ("ONote");
	}
}
