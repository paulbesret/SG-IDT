using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;
using static UnityEngine.Mathf;
using static UnityEngine.Profiling.Profiler;
[ExecuteInEditMode]
public class floatAnimvalue : MonoBehaviour {
	public bool ToujoursAffiche=false;
	//public bool ToujoursAffiche=false;
	public float f1=1;
	public float f2;
	public float f3;
	public float whatAnim;
	public float TempsDepartAnim;
	public string type;
	public static float timelinetime=0;
	float lastimeanim;
	Animator a;
	int aL;
	int creneau;
	public float tempsanim;
	//string animencours;
	public string animencours;
	//f1 = alpha.  si 0  : setactive = false.
	public string[] animations;
	public string[] animations2;

	public int typee=0;
	public bool reAwake=false;
	AnimationCurve Linecurve;
	AnimationCurve LinecurveMere;
	public static Color[] colortypes;
	public Color colorbase;
	
	public float HueA;
	public float SaturationA;
	public float BrightnessA;
	
	public TMPro.TextMeshPro TMP;
	//public gereTempsTextFX1 GTT;
	public ParticleSystem PS;
	ParticleSystem.EmissionModule em;
	public TrailRenderer TR;
	//public TrailRenderer3d TRD;
	public SpriteRenderer SR;
	
	//AnimationCurve Linecurve;
	void Awake() {
		
	}
	//DimBoxes.BoundBox b;
	void Start() {
		/*ToujoursAffiche = false;
		if(GetComponent<DimBoxes.BoundBox>()==null) {
			b = gameObject.AddComponent<DimBoxes.BoundBox>();
			
		} else {
			b = gameObject.GetComponent<DimBoxes.BoundBox>();
		b.lineColor = GameObject.Find("Main").GetComponent<main>().colortypes[typee];// colortypes[typee];
		b = GetComponent<DimBoxes.BoundBox>();
			b.Reset();
		}*/
		
	}
	float precF2;
	float precF1;
	float precF3;
	
	void Update() {
		
		
	}
	
}
