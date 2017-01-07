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
	private Text cachedText;

	void Start () {
		cachedText = countdown.GetComponent<Text>();
		InitTimes();
	}

	void Update () {
		if (delayLeft > 0) {
			delayLeft -= Time.deltaTime;

			int secondsLeft = ((int) delayLeft);

			if (secondsLeft == 0) {
				cachedText.text = goMessage;

			} else {
				cachedText.text = ((int) delayLeft).ToString();
			}

		} else if (roundLeft > 0) {
			roundLeft -= Time.deltaTime;
			cachedText.text = roundLeft.ToString().Substring(0, 4);
			
		} else {
			InitTimes();
		}
	}

	void InitTimes() {
		delayLeft = delayTime + 1;
		roundLeft = roundTime;
	}
}
