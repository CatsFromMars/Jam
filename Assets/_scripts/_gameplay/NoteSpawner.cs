using UnityEngine;
using System.Collections;

public class NoteSpawner : MonoBehaviour {
	public Transform PNote;
	public Transform ONote;
	public float startingPoint = 0f;
	GameController controller;
	AudioSource mainMusic;
	private float rate;
	//public Transform spawner

	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mainMusic = GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>();
		StartCoroutine (spawnNotes());
		rate = (controller.bpm/controller.songLength)/1.5f;
	}

	void Update() {
		//Debug.Log (mainMusic.time);
		if (!mainMusic.isPlaying) {
			controller.endGame(-1);
		}
	}

	IEnumerator spawnNotes() {
		yield return new WaitForSeconds(rate/2);
		while (mainMusic.time < controller.songLength-3) {
			float wait = rate;//*Random.Range(1,4);
			yield return new WaitForSeconds(wait);
			int randNote = Random.Range(1,5);
			if(randNote == 1) Instantiate (PNote, transform.position, Quaternion.identity);
			else if (randNote == 2) Instantiate (ONote, transform.position, Quaternion.identity);
		}
	}
}
