using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SetEffetPapier : MonoBehaviour {
	Camera cam;
	public Material matpapier;
	public Material matpapiervertnoise;
	public Material MatTransparent;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
	}
	bool precvalue=false;
	// Update is called once per frame
	void Update () {
		
		if(cam.enabled!=precvalue) {

			matpapier.SetFloat("_TailleTextures",0.15f);
			matpapiervertnoise.SetFloat("_TailleTextures",0.15f);
			MatTransparent.SetColor("_Color",new Color(1,1,1,(float)82/255));
			if (cam.enabled) {
				matpapier.SetFloat("_TailleTextures",0.38f);
				matpapiervertnoise.SetFloat("_TailleTextures",0.38f);
				MatTransparent.SetColor("_Color",new Color(1,1,1,1));
			} 
			precvalue=cam.enabled;
			
		}
	}
}
