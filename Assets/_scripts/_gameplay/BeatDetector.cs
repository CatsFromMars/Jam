using UnityEngine;
using System.Collections;

public class BeatDetector : MonoBehaviour {
	public Transform currentNote;

	void Awake() {
		currentNote = null;
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "ONote" || other.gameObject.tag == "PNote") currentNote = other.gameObject.transform;
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "ONote" || other.gameObject.tag == "PNote") currentNote = null;
		int playerNum = -1;
		string player = this.transform.parent.name;
		if (player.Equals ("Player 1"))
			playerNum = 0;
		else
			playerNum = 1;
		GameObject.Find ("GameController").GetComponent<GameController> ().players[playerNum].miss ();
	}

//	public void hitBeat(string tag) {
//		Debug.Log ("tag = " + tag);
//		if (currentNote != null && currentNote.tag == tag) {
//			Destroy (currentNote.gameObject);
//			currentNote = null;
//		} else {
//		}
//	}
//
//	//Debug Controls
//	void FixedUpdate () {
//		if (Input.GetKeyDown (KeyCode.A))
//			hitBeat ("ONote");
//	}
}
