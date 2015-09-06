using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour {

	void spawnEffect(Transform effect) {
		Instantiate (effect, transform.position, Quaternion.identity);
	}
}
