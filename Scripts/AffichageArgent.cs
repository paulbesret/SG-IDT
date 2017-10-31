using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageArgent : MonoBehaviour {   
  
	//private float _ArgentAffichage;
	/*public float ArgentAffichage
{
    get { return _ArgentAffichage; }
    set { ArgentAffichage = value; //radius is validated as always positive }
}*/
	// Use this for initialization 
	void Start () {  
		   
	}

	float _ArgentAffichage=0;
	float _ArgentAffichageBut=0;



	public void VarierArgent(float quantite) {
		_ArgentAffichageBut+=quantite;
		if(quantite>0) {
			//fait couleur verte


		} else {
			//fait couleur rouge


		}

	}
	// Update is called once per frame
	void Update () {
		//fait rapprocher la valeur argentAffichage de ArgentAffichage but
	}
}




























