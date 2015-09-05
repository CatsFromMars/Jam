using UnityEngine;
using System.Collections;

public class NoteSpawner : MonoBehaviour {
	public Transform note;
	public float startingPoint = 0f;
	GameController controller;
	AudioSource mainMusic;
	private float rate;

	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mainMusic = GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>();
		rate = 0.4f;
		StartCoroutine (spawnNote ());
	}

	IEnumerator spawnNote() {
		while (true) {
			yield return new WaitForSeconds(rate);
			Instantiate (note, transform.position, Quaternion.identity);
		}
	}
}
