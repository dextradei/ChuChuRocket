using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour {

	[System.NonSerialized]
	public Vector3 position;

	// Use this for initialization
	void Start () {
		//make sure we're centered
		position = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), 0f);
		transform.localPosition = position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
