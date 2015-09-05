using UnityEngine;
using System.Collections;
using BladeCast;

public class GameController : MonoBehaviour {

	public int bpm = 160;
	public float damp = 10f;
<<<<<<< HEAD
	public int player1Score = 0;
	public int player2Score = 0;

	float connect = 0;

	//Creates all controller listeners
	void Start () {
		BCMessenger.Instance.RegisterListener("click",0,this.gameObject,"playerClick");
	}

	void Update () {
		//make sure unity stays connected
		connect += Time.deltaTime;
		if (connect >= 5) {
			BCMessenger.Instance.SendToListeners("check_connection", -1);
			connect = 0;
		}
	}

	//handles player click
	void playerClick(ControllerMessage msg) {
		//TODO CALL PROPER PLAYER BASED ON INDEX
		int player = 0;
		int.TryParse (msg.Payload.GetField ("index").ToString (), out player);
		string button = msg.Payload.GetField ("button").ToString ();
=======
	Player player1;
	Player player2;
	public Transform b1; //Beat Detector1
	public Transform b2; //Beat Detector2
	public TextMesh scorep1;
	public TextMesh scorep2;

	void Awake() {
		player1 = new Player (b1.GetComponent<BeatDetector>(), null);
		player2 = new Player (b2.GetComponent<BeatDetector>(), null);
	}

	void takeInput(int playerIndex, string tag) {
		//Tag has to either be ONote or PNote
		if (playerIndex == 1)
			player1.hit (tag);
		else if (playerIndex == 2)
			player2.hit (tag);
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.A)) takeInput(1, "ONote");
		if(Input.GetKeyDown(KeyCode.S)) takeInput(1, "PNote");

		//Score
		scorep1.text = "Player 1 Score: " + player1.score.ToString ();
		scorep2.text = "Player 2 Score: " + player2.score.ToString ();
>>>>>>> origin/master
	}
}
