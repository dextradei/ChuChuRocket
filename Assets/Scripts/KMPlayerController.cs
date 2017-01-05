using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMPlayerController : PlayerController {
	
	void Update()
	{
		//detect mouse position in gameArea
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 gameAreaPoint = controller.GetWorldGameAreaPoint(worldPoint);

		//find what square this point is on
		int x = Mathf.FloorToInt(gameAreaPoint.x);
		int y = Mathf.FloorToInt(gameAreaPoint.y);

		//if it's a valid place to put an arrow, put the selector box there
		if (controller.OnBoard(x, y) && (controller.GetPiece(x, y) == null))
		{
			MoveSelector(x, y);
			if (Input.GetKey(KeyCode.W))
			{
				PlaceArrow(x, y, Direction.Up);
			}
			else if (Input.GetKey(KeyCode.A))
			{
				PlaceArrow(x, y, Direction.Left);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				PlaceArrow(x, y, Direction.Down);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				PlaceArrow(x, y, Direction.Right);
			}
		}
		else
		{
			RemoveSelector();
		}
	}
}
