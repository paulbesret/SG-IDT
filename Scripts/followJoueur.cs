using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followJoueur : MonoBehaviour {
	public Transform tofollow;
	public float speed=1;
	// Use this for initialization
	void Start () {
		
	}
	Vector3 v3;
	// Update is called once per frame
	void Update () {
		v3 = transform.position;
		v3 += (tofollow.position-v3)*Time.deltaTime*speed;
		transform.position=v3;
	}
}
