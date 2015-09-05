using UnityEngine;
using System.Collections;

public class NoteSpawner : MonoBehaviour {
	public Transform PNote;
	public Transform ONote;
	public float startingPoint = 0f;
	GameController controller;
	AudioSource mainMusic;
	private float rate;

	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mainMusic = GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>();
		rate = 0.80f;
		StartCoroutine (spawnNote ());
	}

	void FixedUpdate() {
		Debug.Log (mainMusic.time);
		//if (mainMusic.time > 24)
			//rate = 0.4f;
		//else
			//rate = 0.8f;
	}

	void changeRate(int r) {
		StopCoroutine (spawnNote());
		rate = r;
		StartCoroutine (spawnNote());
	}

	IEnumerator spawnNote() {
		while (true) {
			yield return new WaitForSeconds(rate);
			int randNote = Random.Range(1,3);
			if(randNote == 1) Instantiate (PNote, transform.position, Quaternion.identity);
			else Instantiate (ONote, transform.position, Quaternion.identity);
		}
	}
}
