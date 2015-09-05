using UnityEngine;
using System.Collections;
using BladeCast;

public class GameController : MonoBehaviour {

	public int bpm = 160;
	public float damp = 10f;
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
	}
}
