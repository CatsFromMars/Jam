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

	public Player(BeatDetector d, Transform a, int num)
	{
		detector = d;
		score = 0;
		combo = 0;
		bestCombo = 0;
		missed = 0;
		avatar = a;
		playerNum = num;
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
			missed++;
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
		combo = 0;
	}
}
