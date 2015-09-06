using UnityEngine;
using System.Collections;

public class Animals : MonoBehaviour {
	public Animator p1;
	public Animator p2;
	public Animator p3;
	public Animator p4;
	//public ParticleSystem catBeam;

	public void animalAttack(int i) {
		p1.SetTrigger(Animator.StringToHash("Panic"));
		p2.SetTrigger(Animator.StringToHash("Panic"));
		p3.SetTrigger(Animator.StringToHash("Panic"));
		p4.SetTrigger(Animator.StringToHash("Panic"));
		if(i == 0) p1.SetTrigger(Animator.StringToHash("Attack"));
		if(i == 1) p2.SetTrigger(Animator.StringToHash("Attack"));
		if(i == 2) p3.SetTrigger(Animator.StringToHash("Attack"));
		if(i == 3) p4.SetTrigger(Animator.StringToHash("Attack"));
	}
}
