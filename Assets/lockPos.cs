﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockPos : MonoBehaviour {
	public Vector3 position;
	// Use this for initialization
	void Start () {
		position=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position=position;
	}
}
