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
			score++;
		} else {
			missed++;
			if (combo > bestCombo) {
				bestCombo = combo;
			}
			combo = 0;
			Debug.Log ("BOOOOO");
		}
	}
}
