using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
	
	public string Action;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			//Work in progress
			if(main.m.lastInteractable != null){
				main.m.startInteraction(Action);
			} else{
				Debug.Log("last interactable is empty");
			}
		}
	}
}
