using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject selectorPrefab;
	public GameObject arrowPrefab;

	private GameController controller;

	void Start () {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	// Update is called once per frame
	void Update()
	{
		//detect mouse position in gameArea
		Vector3 gameAreaPoint = controller.GetMouseGameAreaPoint();

		//find what square this point is on
		int x = Mathf.FloorToInt(gameAreaPoint.x);
		int y = Mathf.FloorToInt(gameAreaPoint.y);

		//if it's a valid place to put an arrow, put the selector box there
		if (controller.OnBoard(x, y) && (controller.GetPiece(x, y) == null))
		{
			controller.MoveSelector(selectorPrefab, x, y);
			//TODO: fix project input settings and use Axes instead of GetKey
			if (Input.GetKey(KeyCode.W))
			{
				//place an up arrow
				controller.PlaceArrow(arrowPrefab, x, y);
			}
		}
		else
		{
			controller.RemoveSelector();
		}
	}
}
