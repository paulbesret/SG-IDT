using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionsActionQuetes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	
	public bool GestionQuete (string stadeQuete,int moment) {
		switch (stadeQuete)
		{
		case "doitAllerSurOrdi":
			if(moment==0) {
				main.m.showMail(0);
			} else if(moment==1) {
				
			} else if(moment==2) {
				
			} 
		
			break;
		default:
			break;
		}
		return false;
	}
	
	
}
