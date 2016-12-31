﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawner : MonoBehaviour {

    public float frequency;
    public Direction direction;

    private Board board;

    private float startTime;
    private Vector3 position;

	// Use this for initialization
	void Start () {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        startTime = Time.time;
        //Make sure we're centered
        position = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), 0f);
        transform.localPosition = position;
    }
	
	// Update is called once per frame
	void Update () {
        float sec = 1f / frequency;
        if (Time.time - startTime > sec)
        {
            board.AddMouse(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), direction);
            startTime = Time.time;
        }
	}
}