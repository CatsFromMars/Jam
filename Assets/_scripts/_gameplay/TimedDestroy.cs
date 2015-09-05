using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {
	public float t = 10f;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, t);
	}
}
