using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour {
	Vector3 Sun= new Vector3(0, 0, 0);
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 axis = new Vector3(0, 1, 1);
		this.transform.RotateAround(Sun, axis, 50 * Time.deltaTime);
	}
}