using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPPlayerController : PlayerController {

	public float cursorVelocity;
	
	void Start () {
		Init();
	}
	
	void Update () {
		//Move the cursor
		float vx = Input.GetAxis("C1.MoveCursorHorizontal");
		float vy = Input.GetAxis("C1.MoveCursorVertical");
		Vector3 p = cursor.transform.position + new Vector3(vx, vy, 0) * Time.deltaTime * cursorVelocity;
		MoveCursor(p);

		//find what square the cursor is on
		int x = Mathf.FloorToInt(cursor.transform.localPosition.x);
		int y = Mathf.FloorToInt(cursor.transform.localPosition.y);

		//if it's a valid place to put an arrow, put the selector box there
		if (controller.OnBoard(x, y) && (controller.GetPiece(x, y) == null))
		{
			MoveSelector(x, y);
			if (Input.GetButtonDown("C1.Up"))
			{
				PlaceArrow(x, y, Direction.Up);
			}
			else if (Input.GetButtonDown("C1.Left"))
			{
				PlaceArrow(x, y, Direction.Left);
			}
			else if (Input.GetButtonDown("C1.Down"))
			{
				PlaceArrow(x, y, Direction.Down);
			}
			else if (Input.GetButtonDown("C1.Right"))
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
