using UnityEngine;
using System.Collections;
using BladeCast;

public class GameController : MonoBehaviour {
	public int bpm = 160;
	public float damp = 10f;
	float connect = 0f;
	Player[] players; //holds all the players
	public Transform b1; //Beat Detector1
	public Transform b2; //Beat Detector2
	public TextMesh scorep1;
	public TextMesh scorep2;

	void Awake() {
		players = new Player[2];
		players[0] = new Player (b1.GetComponent<BeatDetector>(), null);
		players[1] = new Player (b2.GetComponent<BeatDetector>(), null);
		BCMessenger.Instance.RegisterListener("click",0,this.gameObject,"playerClick");
	}

	void takeInput(int player, string tag) {
		//Tag has to either be ONote or PNote
		if (player > 0 && player <= players.Length) {
			players[player-1].hit (tag);
			Debug.Log ("button = " + tag);
		}
	}

	//handles player click
	void playerClick(ControllerMessage msg) {
		//TODO CALL PROPER PLAYER BASED ON INDEX
		int player = 0;
		int.TryParse (msg.Payload.GetField ("index").ToString (), out player);
		string tag = msg.Payload.GetField ("button").ToString ().Replace("\"", "");
		Debug.Log ("button = " + tag);
		takeInput (player, tag);
	}

	void Update() {
		connect += Time.deltaTime;
		if (connect >= 5) {
			BCMessenger.Instance.SendToListeners("check_connection", -1);
			connect = 0;
		}

		if(Input.GetKeyDown(KeyCode.A)) takeInput(1, "ONote");
		if(Input.GetKeyDown(KeyCode.S)) takeInput(1, "PNote");

		//Score
		scorep1.text = "Player 1 Score: " + players[0].score.ToString ();
		scorep2.text = "Player 2 Score: " + players[1].score.ToString ();
	}
}
