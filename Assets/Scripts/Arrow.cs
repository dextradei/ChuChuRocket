using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public Direction direction = Direction.Up;
	public GameObject arrowImage;

	// Use this for initialization
	void Start () {
		SetRotation();
	}

	void SetRotation()
	{
		switch (direction)
		{
			case Direction.Up:
				arrowImage.transform.eulerAngles = new Vector3(0f, 0f, 0f);
				break;
			case Direction.Right:
				arrowImage.transform.eulerAngles = new Vector3(0f, 0f, 270f);
				break;
			case Direction.Down:
				arrowImage.transform.eulerAngles = new Vector3(0f, 0f, 180f);
				break;
			case Direction.Left:
				arrowImage.transform.eulerAngles = new Vector3(0f, 0f, 90f);
				break;
		}
	}
}
