using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPerso : MonoBehaviour2 {


	public Transform destination;
	public Vector3 destv;
	public float distanceAvantAction=1.5f;
	public float distanceAvantArret=0.7f;
	public float distanceCible=0;
	public float speed=0;
	public float speedBeforeStop;
	public Camera CameraCutScene;
	
	//Transform placeHolderCamera;
	//Vector3 cameraPosition;
	Animator AC;

	private UnityEngine.AI.NavMeshAgent agent;

	void Start ()
	{
		agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		AC = t.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();
		//AC.CrossFade("wave",0.2f);
		//agent.SetDestination(destination.position);
	}
	
	public string etat;
	Vector3 precposition = Vector3.zero;
	float timeBeforeTryStopping = 0;
	
	
	void Update() {
		//stop walking after reaching his target
		//(when the target is not an interactable)
		if(etat=="marche" && agent.remainingDistance <= distanceAvantArret) {
			agent.ResetPath();
			AC.CrossFade("idle",0.1f);
			etat="idle";
		}
		speed = Vector3.Distance(t.position,precposition);
		if(timeBeforeTryStopping<0 && etat=="marche" && agent.remainingDistance < distanceAvantArret*2 && speed < speedBeforeStop) {
			agent.ResetPath();
			etat="idle";
			AC.CrossFade("idle",0.1f);
		}
		
		timeBeforeTryStopping-=Time.deltaTime;
		if(etat=="marche" && main.m.lastInteractable!=null) {
			distanceCible = Vector3.Distance(t.position,main.m.lastInteractable.transform.position);
		}
		
		//player is walking to an interactable
		if(etat=="marche" && main.m.lastInteractable!=null && Vector3.Distance(t.position,main.m.lastInteractable.transform.position)<distanceAvantAction) {
			etat="idle";
			//Player will interact with the chair
			if(main.m.lastInteractable.tag == "ordi"){
				//Player open PC and sit (currently dance)
				etat = "ordinateur";
				agent.ResetPath();
			//Player wants to interact with the bookshelf
			}else if(main.m.lastInteractable.tag == "armoire"){
				//Player open the bookshelf (anim: pickup)
				etat = "armoire";
				agent.ResetPath();
			} else {
				AC.CrossFade("idle",0.1f);
				agent.ResetPath();
			}
			
			if(main.m.lastInteractable.tag=="PNJ") {
				main.m.actionPNJ(main.m.lastInteractable.name);
			}
		}
		precposition = t.position;
	}
	
	//used to move the agent
	public void gotoo(Vector3 dist) {
		//if player is not in a cutscene, he can move
		if(CameraCutScene.enabled == false){
			agent.SetDestination(dist);
			timeBeforeTryStopping=1;
			etat="marche";
			AC.CrossFade("walk",0.1f);
		}
	}
	
	
	void OnAnimatorIK(int layerIndex)
	{
		/*if (viewTarget)
		{*/
			AC.SetLookAtPosition(destv);        
			
			AC.SetLookAtWeight(1.0f);
	//}
	}
	void LateUpdate()
	{
		/*if (viewTarget)
		{*/
		
		//each lines throw a non-important error
		//AC.SetLookAtPosition(destv);        
		//AC.SetLookAtWeight(1.0f);
	//}
	}
	
	

}
