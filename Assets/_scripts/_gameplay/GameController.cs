using UnityEngine;
using System.Collections;
using BladeCast;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	ControllerManager master;
	public GameObject[] players; //holds all the players
	public int songLength = 160;
	public float damp = 40f;
	public float bpm = 140f;
	public int remaningPlayers;

	void Awake() {
		master = GameObject.Find ("ControllerManager").GetComponent<ControllerManager> ();
		master.loadLevel (Application.loadedLevelName);
		int numPlayers = master.getNumPlayers ();
		remaningPlayers = numPlayers;
		for (int i = 0; i < players.Length; i++) {
			players[i].GetComponent<PlayerClass>().loadPlayer(i+1, 0, 50*numPlayers);
			if (i >= numPlayers) {
				players[i].SetActive(false);
			}
		}
	}

	public void takeInput(int player, string tag) {
		//Tag has to either be ONote or PNote
		if (player > 0 && player <= players.Length) {
			if (players[player - 1].activeInHierarchy){
				players[player - 1].GetComponent<PlayerClass>().hit (tag);
			}
		}
	}


	void Update() {
		if(Input.GetKeyDown(KeyCode.A)) takeInput(1, "ONote");
		if(Input.GetKeyDown(KeyCode.S)) takeInput(1, "PNote");

		for (int i = 0; i < players.Length; i++) {
			if (players[i].activeInHierarchy) {
				if (players[i].GetComponent<PlayerClass>().health <= 0) {
					//setscore and deactivate loser
					master.score[i] = players[i].GetComponent<PlayerClass>().score;
					master.maxCombo[i] = players[i].GetComponent<PlayerClass>().bestCombo;
					master.missedNotes[i] = players[i].GetComponent<PlayerClass>().missed;
					players[i].SetActive(false);
					remaningPlayers--;
				}
			}
		}
		if (remaningPlayers <= 1) {
			int winner = -1;
			for (int i = 0; i < players.Length; i++) {
				if (players[i].activeInHierarchy) {
					winner = i+1;
				}
			}
			endGame (winner);
		}
	}

	public void endGame(int victor) {
		if (victor == -1) {
			int max = 0;
			for (int i = 0; i < players.Length; i++) {
				if (players[i].activeInHierarchy) {
					//setscore
					master.score[i] = players[i].GetComponent<PlayerClass>().score;
					master.maxCombo[i] = players[i].GetComponent<PlayerClass>().bestCombo;
					master.missedNotes[i] = players[i].GetComponent<PlayerClass>().missed;

					if (players[i].GetComponent<PlayerClass>().score > max) {
						max = players[i].GetComponent<PlayerClass>().score;
						victor = i+1;
					}
					else if (players[i].GetComponent<PlayerClass>().score == max) {
						max = players[i].GetComponent<PlayerClass>().score;
						victor = -1;
					}
				}
			}
			master.victor = victor;
			master.victoryType = 0;
		} else {
			master.victor = victor;
			master.victoryType = 1;
		}

		StartCoroutine (gotoResults());
	}

	//allows a player to join halfway through the game with score of last place 
	//and the avg amount of health the remaining players have
	public void dropIn(int controller) {
		if (controller <= 4) {
			float avgHealth = 0;
			int maxHealth = 0;
			int lowScore = 9001;
			for (int i = 0; i < players.Length; i++) {
				if (i != controller - 1 && players[i].activeInHierarchy) {
					players[i].GetComponent<PlayerClass>().health += 50;
					players[i].GetComponent<PlayerClass>().hbar.maxValue += 50;
					maxHealth = (int)players[i].GetComponent<PlayerClass>().hbar.maxValue;
					avgHealth += players[i].GetComponent<PlayerClass>().health;
					if (players[i].GetComponent<PlayerClass>().score < lowScore) {
						lowScore = players[i].GetComponent<PlayerClass>().score;
					}
				}
			}
			avgHealth /= remaningPlayers;
			players [controller - 1].SetActive (true);
			players[controller - 1].GetComponent<PlayerClass>().loadPlayer(controller, lowScore, (int)avgHealth);
			players[controller - 1].GetComponent<PlayerClass>().hbar.maxValue = maxHealth;
			remaningPlayers++;
		}
	}
	IEnumerator gotoResults() {
		yield return new WaitForSeconds (1.0f);
		Application.LoadLevel ("resultsScreen");
	}
}
