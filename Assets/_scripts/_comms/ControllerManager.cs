using UnityEngine;
using System.Collections;
using BladeCast;

public class ControllerManager : MonoPersistentSingleton<ControllerManager>{

	bool connected = false;
	float connect = 0f;
	int numPlayers = 1;
	string curLevel;

	// Use this for initialization
	void Start () {
		loadLevel (Application.loadedLevelName);
	}
	
	// Update is called once per frame
	void Update () {
		connect += Time.deltaTime;
		if (connect >= 5) {
			BCMessenger.Instance.SendToListeners("check_connection", -1);
			connect = 0;
		}
		//wait till connected to sync controller with level
		if (!connected && GameObject.Find ("BCLibProvider").GetComponent<BCLibProvider>().BladeCast_Game_IsConnected()) {
			BCMessenger.Instance.SendToListeners ("set_scene", "scene", curLevel, -1);
			connected = true;
		}
	}

	void syncController(ControllerMessage msg) {
		int controller = -1;
		//find out which controller msged
		int.TryParse(msg.Payload.GetField("index").ToString(), out controller);
		switch (Application.loadedLevelName) {
			case "loadingScene":
				if (controller == 1) {
					//player 1 loads the menu while the other players wait
					BCMessenger.Instance.SendToListeners ("set_scene", "scene", "loadMenu", 1);
				}
				break;
			default:
				BCMessenger.Instance.SendToListeners ("set_scene", "scene", Application.loadedLevelName, -1);
				break;
		}
	}
	void menuOptions(ControllerMessage msg) {
		if (Application.loadedLevelName == "loadingScene") {
			switch (msg.Payload.GetField("button").ToString().Replace("\"", "")) {
			case "singleplayer":
				numPlayers = 1;
				break;
			case "multiplayer":
				numPlayers = 2;
				break;
			}
			Application.LoadLevel("mainGame");
		}
	}

	//handles player click
	void playerClick(ControllerMessage msg) {
		Debug.Log (Application.loadedLevelName);
		if (Application.loadedLevelName == "mainGame") {
			int player = 0;
			int.TryParse (msg.Payload.GetField ("index").ToString (), out player);
			string tag = msg.Payload.GetField ("button").ToString ().Replace ("\"", "");
			Debug.Log ("button = " + tag);
			GameObject.Find ("GameController").GetComponent<GameController> ().takeInput (player, tag);
		}
	}

	public int getNumPlayers() {
		return numPlayers;
	}

	public void loadLevel(string level) {
		connected = false;
		curLevel = level;
		//need this to re-add appropiate unity listeners
		switch (level) {
			case "loadingScene":
				BCMessenger.Instance.RegisterListener("menu_click",0,this.gameObject,"menuOptions");
				break;
			case "mainGame":
				BCMessenger.Instance.RegisterListener("click",0,this.gameObject,"playerClick");
				break;
		}
		BCMessenger.Instance.RegisterListener("request_sync",0,this.gameObject,"syncController");
	}
}
