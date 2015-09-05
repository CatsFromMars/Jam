using UnityEngine;
using System.Collections;

public class ResultsManager : MonoBehaviour {

	ControllerManager master;

	public TextMesh score1;
	public TextMesh score2;
	public TextMesh combo1;
	public TextMesh combo2;
	public TextMesh missed1;
	public TextMesh missed2;

	// Use this for initialization
	void Start () {
		master = GameObject.Find ("ControllerManager").GetComponent<ControllerManager> ();
		master.loadLevel (Application.loadedLevelName);
		int numPlayers = master.getNumPlayers ();
		score1.text = "Player 1 Score: " + master.score1.ToString();
		combo1.text = "Largest Combo: " + master.maxCombo1.ToString();
		missed1.text = "Missed Notes: " + master.missedNotes1.ToString();
		if (numPlayers > 1) {
			score2.text = "Player 2 Score: " + master.score2.ToString();
			combo2.text = "Largest Combo: " + master.maxCombo2.ToString();
			missed2.text = " Missed Notes: " + master.missedNotes2.ToString();
		} else {
			GameObject.Find("Player 2").SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
