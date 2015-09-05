﻿using UnityEngine;
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
