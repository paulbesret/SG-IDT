using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;
//using static UnityEngine.Debug;
//using  UnityEngine.Mathf;

public class MonoBehaviour2 : MonoBehaviour {
	[HideInInspector]
	public Transform t;
	void Awake() {
		t = transform;

	}

	public Renderer renderer {
	     get {
	         return GetComponent<Renderer>();
	     }
	 }

	 public Renderer r {
	     get {
	         return GetComponent<Renderer>();
	     }
	 }
	 public Animation anim {
	     get {
	         return GetComponent<Animation>();
	     }
	 }

	 public Animation a {
	     get {
	         return GetComponent<Animation>();
	     }
	 }
	public Animator ac {
	     get {
	         return GetComponent<Animator>();
	     }
	 }
	 public Collider collider {
	     get {
	         return GetComponent<Collider>();
	     }
	 }
	 public Collider c {
	     get {
	         return GetComponent<Collider>();
	     }
	 }
	 public static void l(string s) {
	 	Debug.Log(s);
	 }
}


 