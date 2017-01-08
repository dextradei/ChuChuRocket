using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] gamepads = Input.GetJoystickNames();
		Debug.Log("Number of Gamepads: " + gamepads.Length);
		foreach (string name in gamepads)
		{
			Debug.Log(name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
