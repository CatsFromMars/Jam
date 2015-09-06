using UnityEngine;
using System.Collections;
using BladeCast;

public class Player{
	public int playerNum;
	public BeatDetector detector;
	public int score;
	public int missed;
	public int combo;
	public int bestCombo;
	public Transform avatar;
	public int health;
	public Animals fightManager;

	public Player(BeatDetector d, Transform a, int num)
	{
		detector = d;
		score = 0;
		combo = 0;
		bestCombo = 0;
		missed = 0;
		health = 100;
		avatar = a;
		playerNum = num;
		fightManager = GameObject.Find ("Fight").GetComponent<Animals> ();
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
			if (playerNum == 1) {
				GameObject.Find ("GameController").GetComponent<GameController>().players[1].health -= damage;
				fightManager.catAttackDog();
				GameObject.Find ("GameController").GetComponent<GameController>().StartCoroutine("fightReset");
			}
			else {
				GameObject.Find ("GameController").GetComponent<GameController>().players[0].health -= damage;
				fightManager.dogAttackCat();
				fightManager.normal();
			}
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
}
