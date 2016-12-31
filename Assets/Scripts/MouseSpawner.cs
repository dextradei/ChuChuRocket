using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawner : MonoBehaviour {

	public float frequency;
	public Direction direction;

	private GameController controller;

	private float startTime;
	[System.NonSerialized]
	public Vector3 position;

	void Awake()
	{
		//Make sure we're centered
		position = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), 0f);
		transform.localPosition = position;
	}

	// Use this for initialization
	void Start () {
		controller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float sec = 1f / frequency;
		if (Time.time - startTime > sec)
		{
			controller.AddMouse(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), direction);
			startTime = Time.time;
		}
	}
}
