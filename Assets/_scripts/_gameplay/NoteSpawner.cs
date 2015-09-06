using UnityEngine;
using System.Collections;

public class NoteSpawner : MonoBehaviour {
	public Transform PNote;
	public Transform ONote;
	public float startingPoint = 0f;
	GameController controller;
	AudioSource mainMusic;
	private float rate = 0.4f;
	public int musicTime = 243;

	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mainMusic = GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>();
		StartCoroutine (spawnNotes());
	}

	void Update() {
		//Debug.Log (mainMusic.time);
		if (!mainMusic.isPlaying) {
			controller.endGame();
		}
	}

	IEnumerator spawnNotes() {
		while (mainMusic.time < musicTime) {
			float wait = rate*Random.Range(1,4);
			yield return new WaitForSeconds(wait);
			int randNote = Random.Range(1,3);
			if(randNote == 1) Instantiate (PNote, transform.position, Quaternion.identity);
			else Instantiate (ONote, transform.position, Quaternion.identity);
		}
	}
}
