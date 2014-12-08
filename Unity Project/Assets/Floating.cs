﻿using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {


	Vector3 zeroPos;
	float amplitude = 0.1f;
	float period = 3f;

	// Use this for initialization
	void Start () {
		zeroPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		var y = amplitude * Mathf.Sin (Time.time * Mathf.PI / period);
		transform.position = zeroPos + Vector3.up * y;
	}
}