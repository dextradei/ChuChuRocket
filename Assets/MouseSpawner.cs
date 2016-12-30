using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawner : MonoBehaviour {

    public GameObject BoardObject;
    public float frequency;
    public Direction direction;

    private Board board;

    private float startTime;
    private Vector3 position;

	// Use this for initialization
	void Start () {
        board = BoardObject.GetComponent<Board>();
        startTime = Time.time;
        position = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), 0f);
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
