using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTestControle : StateMachineBehaviour {
	public string EtatQuete="";
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	int i;
	
	public Transform cibleQuete;
	public GameObject cibleQuete2;
	public string texteExplicatif;
	public Collider NPC;
	
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		i=0;
		Debug.Log("enter "+stateInfo.nameHash);
		main.Caq.GestionQuete(EtatQuete,0);
		if(cibleQuete!=null) {
			//afficher Fleche
		}
		if(texteExplicatif!="") {
			//afficher texte
		}
	}
	public Transform ciblequete;

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
		
		//vérification condition
		if(main.Caq.GestionQuete(EtatQuete,1)) {
			main.Caq.GestionQuete(EtatQuete,2);
			//action
			//sortie.
			animator.Play(animator.GetNextAnimatorStateInfo(0).nameHash);
			if(cibleQuete!=null) {
			//cacher Fleche
			}
		}
		if(cibleQuete!=null) {
			GestionFleche();
		}
		
	}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log("exit"+stateInfo.nameHash);
		
	}
	
	
	void GestionFleche() {
		
		
		
		
		
	}



}
