using UnityEngine;
using System.Collections;
using BladeCast;
using UnityEngine.UI;

public class PlayerClass: MonoBehaviour{
	public Transform b; //Beat 
	public TextMesh comborep;
	public TextMesh scorep;
	public TextMesh healthrep;
	public Slider hbar;

	public int playerNum;
	public BeatDetector detector;
	public int score;
	public int missed;
	public int combo;
	public int bestCombo;
	public Transform avatar;
	public float health;

	Animals fightManager;
	GameController master;

	void Start()
	{
		combo = 0;
		bestCombo = 0;
		missed = 0;
		hbar.maxValue = 100;
		fightManager = GameObject.Find ("Fight").GetComponent<Animals> ();
		master = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	void Update() {
		comborep.text = "Combo: " + combo;
		scorep.text = "Player " + playerNum + " Score: " + score;
		healthrep.text = "Player " + playerNum + " Health: " + health;
		hbar.value = health;
	}

	public void loadPlayer(int pNum, int points, float hp) {
		playerNum = pNum;
		score = points;
		health = hp;
		combo = 0;
		bestCombo = 0;
		missed = 0;
		hbar.maxValue = hp;
	}
	
	public void hit(string tag) {
		if (detector.currentNote != null && detector.currentNote.tag == tag) {
			Debug.Log ("You did it!");
			GameObject.Find("ControllerManager").GetComponent<ControllerManager>().vibrate(100, playerNum);
			detector.currentNote.GetComponent<Note> ().destroySelf ();
			detector.currentNote = null;
			combo++;
			//calculate combo bonus
			if (combo >= 20) {
				score += 10;
			}
			else if (combo >= 10) {
				score += 5;
			}
			else if (combo >= 5) {
				score += 2;
			}
			else {
				score++;
			}
		} else {
			if (combo > bestCombo) {
				bestCombo = combo;
			}
			combo = 0;
			if (score > 0) {
				score--;
			}
			Debug.Log ("BOOOOO");
		}
	}

	//call when player misses a note
	public void miss() {
		if (combo > bestCombo) {
			bestCombo = combo;
		}
		missed++;
		detector.miss ();
		combo = 0;
	}

	//call to attack
	public void attack() {
		if (combo >= 5) {
			if (combo > bestCombo) {
				bestCombo = combo;
			}
			int damage = combo;
			if (combo >= 30) {
				damage *= 3;
			}
			else if (combo >= 15) {
				damage *= 2;
			}
			//NO MERCY
			for (int i = 0; i < master.players.Length; i++) {
				if (playerNum != i+1) {
					master.players[i].GetComponent<PlayerClass>().health--;
				}
			}
			StartCoroutine("fightReset");
			combo = 0;
		}
	}

	public void heal() {
		if (combo > bestCombo) {
			bestCombo = combo;
		}
		health += combo;
		combo = 0;
	}

	IEnumerator fightReset() {
		yield return new WaitForSeconds (0.5f);
		fightManager.normal();
	}
}
