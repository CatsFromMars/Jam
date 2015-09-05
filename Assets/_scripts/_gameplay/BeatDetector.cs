using UnityEngine;
using System.Collections;

public class BeatDetector : MonoBehaviour {
	public Transform currentNote;

	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "ONote" || other.gameObject.tag == "PNote") currentNote = other.gameObject.transform;
		Debug.Log (other.name);
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "ONote" || other.gameObject.tag == "PNote") currentNote = null;
	}
}
