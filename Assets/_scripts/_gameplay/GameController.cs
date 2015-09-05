using UnityEngine;
using System.Collections;
using BladeCast;

public class GameController : MonoBehaviour {
	ControllerManager master;
	public int bpm = 160;
	public float damp = 10f;
	Player[] players; //holds all the players
	public Transform b1; //Beat Detector1
	public Transform b2; //Beat Detector2
	public TextMesh combo1;
	public TextMesh combo2;
	public TextMesh scorep1;
	public TextMesh scorep2;

	void Awake() {
		master = GameObject.Find ("ControllerManager").GetComponent<ControllerManager> ();
		master.loadLevel (Application.loadedLevelName);
		int numPlayers = master.getNumPlayers ();
		players = new Player[2];
		players[0] = new Player (b1.GetComponent<BeatDetector>(), null, 1);
		players[1] = new Player (b2.GetComponent<BeatDetector>(), null, 2);

		if (numPlayers == 1) {
			GameObject.Find ("Player 2").SetActive(false);
		}
	}

	public void takeInput(int player, string tag) {
		//Tag has to either be ONote or PNote
		if (player > 0 && player <= players.Length) {
			players[player-1].hit (tag);
		}
	}


	void Update() {
		if(Input.GetKeyDown(KeyCode.A)) takeInput(1, "ONote");
		if(Input.GetKeyDown(KeyCode.S)) takeInput(1, "PNote");

		//Score
		scorep1.text = "Player 1 Score: " + players[0].score.ToString ();
		scorep2.text = "Player 2 Score: " + players[1].score.ToString ();

		//current combo
		combo1.text = "Combo: " + players[0].combo.ToString ();
		combo2.text = "Combo: " + players[1].combo.ToString ();
	}

	public void endGame() {
		master.score1 = players [0].score;
		master.maxCombo1 = players [0].bestCombo;
		master.missedNotes1 = players [0].missed;
		if (players.Length > 1) {
			master.score2 = players [1].score;
			master.maxCombo2 = players [1].bestCombo;
			master.missedNotes2 = players [1].missed;
		}
		StartCoroutine (gotoResults());
	}
	IEnumerator gotoResults() {
		yield return new WaitForSeconds (1.0f);
		Application.LoadLevel ("resultsScreen");
	}
}
