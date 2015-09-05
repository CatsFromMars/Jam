using UnityEngine;
using System.Collections;

public class Player
	{
		public BeatDetector detector;
		public int score;
		public Transform avatar;

		public Player(BeatDetector d, Transform a)
		{
			detector = d;
			score = 0;
			avatar = a;
		}
		
		public void hit(string tag) {
			if (detector.currentNote != null && detector.currentNote.tag == tag) {
				Debug.Log ("You did it!");
				detector.currentNote.GetComponent<Note>().destroySelf();
				detector.currentNote = null;
				score++;
			} else
				Debug.Log ("BOOOOO");
		}


	}
