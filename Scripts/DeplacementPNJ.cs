using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPNJ : MonoBehaviour2 {


	public Transform destination;
	Animator AC;

	private UnityEngine.AI.NavMeshAgent agent;

	void Start ()
	{
		agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		AC = t.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();
		
		agent.SetDestination(destination.position);
	}

	void Update() {
		if(agent.remainingDistance<0.1f) {
			AC.CrossFade("idle",0.1f);
		} else {
			AC.CrossFade("walk",0.1f);
			
		}
			
	}
	void OnAnimatorIK(int layerIndex)
	{
		/*if (viewTarget)
		{*/
			AC.SetLookAtPosition(destination.position);        
			
			AC.SetLookAtWeight(1.0f);
	//}
	}

}
