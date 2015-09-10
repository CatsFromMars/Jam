using UnityEngine;
using System.Collections;
using BladeCast;

public class ControllerManager : MonoPersistentSingleton<ControllerManager>{

	bool connected = false;
	float connect = 0f;
	int numPlayers = 2;
	string curLevel;

	//for final results
	//posibile todo: make player results struct
	public int[] score;
	public int[] maxCombo;
	public int[] missedNotes;
	public int victor;
	public int victoryType;

	// Use this for initialization
	void Start () {
		loadLevel (Application.loadedLevelName);
		score = new int[4];
		maxCombo = new int[4];
		missedNotes = new int[4];
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
			if (curLevel == "loadingScene" || curLevel == "resultsScene") {
				//controller one will get the menu when loading
				BCMessenger.Instance.SendToListeners ("set_scene", "scene", "loadMenu", 1);
			}
			connected = true;
		}
	}

	void syncController(ControllerMessage msg) {
		int controller = -1;
		//find out which controller msged
		int.TryParse(msg.Payload.GetField("index").ToString(), out controller);
		switch (Application.loadedLevelName) {
			case "resultsScreen":
			case "loadingScene":
				if (controller == 1) {
					//player 1 loads the menu while the other players wait
					BCMessenger.Instance.SendToListeners ("set_scene", "scene", "loadMenu", 1);
				}
				break;
			case "mainGame": 
				if (GameObject.Find("GameController").GetComponent<GameController>().players[controller - 1].activeInHierarchy) {
					GameObject.Find("GameController").GetComponent<GameController>().dropIn(controller);
				}
				BCMessenger.Instance.SendToListeners ("set_scene", "scene", Application.loadedLevelName, controller);
				break;
			default:
				BCMessenger.Instance.SendToListeners ("set_scene", "scene", Application.loadedLevelName, controller);
				break;
		}
	}
	void menuOptions(ControllerMessage msg) {
		if (Application.loadedLevelName == "loadingScene" || Application.loadedLevelName == "resultsScreen") {
			switch (msg.Payload.GetField("button").ToString().Replace("\"", "")) {
				case "twoPlayer":
					numPlayers = 2;
					break;
				case "threePlayer":
					numPlayers = 3;
					break;
				case "fourPlayer":
					numPlayers = 4;
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

	//handles shake
	void shakeIt(ControllerMessage msg) {
		Debug.Log ("SHAKE IT");
		if (Application.loadedLevelName == "mainGame") {
			int player = 0;
			int.TryParse (msg.Payload.GetField ("index").ToString (), out player);
			GameObject.Find ("GameController").GetComponent<GameController>().players[--player].GetComponent<PlayerClass>().attack();
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
			case "resultsScreen":
			case "loadingScene":
				BCMessenger.Instance.RegisterListener("menu_click",0,this.gameObject,"menuOptions");
				break;
			case "mainGame":
				BCMessenger.Instance.RegisterListener("click",0,this.gameObject,"playerClick");
				BCMessenger.Instance.RegisterListener("shake",0,this.gameObject,"shakeIt");
				break;
		}
		BCMessenger.Instance.RegisterListener("request_sync",0,this.gameObject,"syncController");
	}

	public void vibrate(int milliseconds, int player) {
		BCMessenger.Instance.SendToListeners ("vibrate", "time", milliseconds, player);
	}
}
