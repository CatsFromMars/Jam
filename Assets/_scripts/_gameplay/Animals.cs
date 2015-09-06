using UnityEngine;
using System.Collections;

public class Animals : MonoBehaviour {
	public Animator catAnimator;
	public Animator dogAnimator;
	public Transform cat;
	public Transform dog;
	//public ParticleSystem catBeam;

	void Awake() {
		StartCoroutine (testfight ());
	}
	
	void catAttackDog() {
		catAnimator.SetInteger (Animator.StringToHash ("CatState"), 1);
		dogAnimator.SetInteger (Animator.StringToHash ("DogState"), 3);
	}

	void dogAttackCat() {
		dogAnimator.SetInteger (Animator.StringToHash ("DogState"), 0);
		catAnimator.SetInteger (Animator.StringToHash ("CatState"), 2);
	}

	void normal() {
		catAnimator.SetInteger (Animator.StringToHash ("CatState"), 3);
		dogAnimator.SetInteger (Animator.StringToHash ("DogState"), 1);
	}

	IEnumerator testfight() {
		while (true) {
			yield return new WaitForSeconds (2f);
			catAttackDog ();
			yield return new WaitForSeconds (0.5f);
			normal ();
			yield return new WaitForSeconds (2f);
			dogAttackCat ();
			yield return new WaitForSeconds (0.5f);
			normal ();

		}
	}
}
