using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimerController : MonoBehaviour {

	public GameObject countdown;

	// Use this for initialization
	void Start () {
		countdown.GetComponent<Text>().text = "3…";
	}
	
	// Update is called once per frame
	void Update () {
		countdown.GetComponent<Text>().text = "2…";
	}
}
