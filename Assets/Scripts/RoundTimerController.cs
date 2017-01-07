using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimerController : MonoBehaviour {

	public GameObject countdown;

	public float delayTime;
	public float roundTime;

	public string goMessage;

	private float delayLeft;
	private float roundLeft;

	void Start () {
		delayLeft = delayTime + 1;
		roundLeft = roundTime;
	}

	void Update () {
		if (delayLeft > 0) {
			delayLeft -= Time.deltaTime;

			int integerDelayLeft = ((int) delayLeft);

			if (integerDelayLeft == 0) {
				countdown.GetComponent<Text>().text = goMessage;
			} else {
				countdown.GetComponent<Text>().text = ((int) delayLeft).ToString();
			}

		} else if (roundLeft > 0) {
			roundLeft -= Time.deltaTime;
			countdown.GetComponent<Text>().text = roundLeft.ToString().Substring(0,4);
			
		} else {
			delayLeft = delayTime + 1;
			roundLeft = roundTime;
		}
	}
}
