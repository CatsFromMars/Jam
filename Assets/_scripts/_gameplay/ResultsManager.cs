using UnityEngine;
using System.Collections;

public class ResultsManager : MonoBehaviour {

	ControllerManager master;

	public TextMesh[] score;
	public TextMesh[] combo;
	public TextMesh[] missed;
	public TextMesh victoryText;

	// Use this for initialization
	void Start () {
		master = GameObject.Find ("ControllerManager").GetComponent<ControllerManager> ();
		master.loadLevel (Application.loadedLevelName);
		int numPlayers = master.getNumPlayers ();
		for (int i = 0; i < numPlayers; i++) {
			score[i].text = "Player " + (i+1) + " Score: " + master.score[i];
			combo[i].text = "Largest Combo: " + master.maxCombo[i];
			missed[i].text = "Missed Notes: " + master.missedNotes[i];
		}

		string victoryType;
		if (master.victoryType == 0) {
			victoryType = " musical mastery!";
		} else {
			victoryType = " blood!";
		}
		victoryText.text = "Player " + master.victor + " wins by right of " + victoryType;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
