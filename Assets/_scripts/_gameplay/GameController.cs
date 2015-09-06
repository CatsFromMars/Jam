using UnityEngine;
using System.Collections;
using BladeCast;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	ControllerManager master;
	public Player[] players; //holds all the players
	public int songLength = 160;
	public float damp = 40f;
	public float bpm = 140f;
	public Transform b1; //Beat Detector1
	public Transform b2; //Beat Detector2
	public TextMesh combo1;
	public TextMesh combo2;
	public TextMesh scorep1;
	public TextMesh scorep2;
	public TextMesh health1;
	public TextMesh health2;
	public Slider hbar1;
	public Slider hbar2;

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

		//current health
		health1.text = "Health: " + players[0].health.ToString ();
		health2.text = "Health: " + players[1].health.ToString ();
		hbar1.value = players [0].health;
		hbar2.value = players [1].health;

		if (players [0].health <= 0) {
			endGame (2);
		} else if (players [1].health <= 0) {
			endGame (1);
		}
	}

	public void endGame(int victor) {
		master.score1 = players [0].score;
		master.maxCombo1 = players [0].bestCombo;
		master.missedNotes1 = players [0].missed;
		master.score2 = players [1].score;
		master.maxCombo2 = players [1].bestCombo;
		master.missedNotes2 = players [1].missed;
		if (victor == -1) {
			master.victor = (players [0].score > 
				players [1].score) ? 1 : 2;
			master.victoryType = 0;
		} else {
			master.victor = victor;
			master.victoryType = 1;
		}

		StartCoroutine (gotoResults());
	}
	IEnumerator gotoResults() {
		yield return new WaitForSeconds (1.0f);
		Application.LoadLevel ("resultsScreen");
	}
	IEnumerator fightReset() {
		yield return new WaitForSeconds (0.5f);
		GameObject.Find ("Fight").GetComponent<Animals> ().normal();
	}
}
