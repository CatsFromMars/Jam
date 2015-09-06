using UnityEngine;
using System.Collections;

public class NoteSpawner : MonoBehaviour {
	public Transform PNote;
	public Transform ONote;
	public float startingPoint = 0f;
	GameController controller;
	AudioSource mainMusic;
	private float rate;
	public Transform otherSpawner;

	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mainMusic = GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>();
		StartCoroutine (spawnNotes());
		rate = (controller.bpm/controller.songLength)/1.5f;
	}

	void Update() {
		//Debug.Log (mainMusic.time);
		if (!mainMusic.isPlaying) {
			controller.endGame();
		}
	}

	IEnumerator spawnNotes() {
		yield return new WaitForSeconds(rate/2);
		while (mainMusic.time < controller.songLength-3) {
			float wait = rate;//*Random.Range(1,4);
			yield return new WaitForSeconds(wait);
			int randNote = Random.Range(1,5);
			if(randNote == 1) instantiateNotes(PNote);
			else if (randNote == 2) instantiateNotes(ONote);
		}
	}

	void instantiateNotes(Transform note) {
		Instantiate (note, transform.position, Quaternion.identity);
		Instantiate (note, otherSpawner.transform.position, Quaternion.identity);
	}
}
