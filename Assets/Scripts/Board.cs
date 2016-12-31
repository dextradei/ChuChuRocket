using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Board : MonoBehaviour {
	//These four have a custom editor which displays width/height boxes and a grid of checkboxes in the Inspector
	[HideInInspector]
	public int Width = 16;
	[HideInInspector]
	public int Height = 16;
	//for block (x,y), is there a horizontal wall blocking the south?
	[HideInInspector]
	public bool[] HorizontalWalls;
	//for block (x,y), is there a vertical wall blocking the west?
	[HideInInspector]
	public bool[] VerticalWalls;

	public GameObject horizontalWallPrefab;
	public GameObject verticalWallPrefab;

	//every game piece needs to be a child of the GameArea so that (x,y) corresponds to a spot on the board
	private Transform gameArea;

	void Start()
	{
		gameArea = GameObject.FindGameObjectWithTag("GameArea").transform;

		//Build Horizontal Walls
		for (int y = 0; y < Height + 1; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				int index = (y * (Width)) + x;
				if (HorizontalWalls[index])
				{
					GameObject wall = Instantiate(horizontalWallPrefab, gameArea);
					wall.transform.localPosition = new Vector3((float)x, (float)y, 0f);
				}
			}
		}
		//Build Vertical Walls
		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width + 1; x++)
			{
				int index = (y * (Width + 1)) + x;
				if (VerticalWalls[index])
				{
					GameObject wall = Instantiate(verticalWallPrefab, gameArea);
					wall.transform.localPosition = new Vector3((float)x, (float)y, 0f);
				}
			}
		}
		
	}

	//Can a mouse at (x,y) move direction?
	public bool CanGo(int x, int y, Direction direction)
	{
		bool ret = true;
		//add 1 for up and right
		switch (direction)
		{
			case Direction.Up:
				if (HorizontalWalls[((y + 1) * Width) + x])
					ret = false;
				break;
			case Direction.Right:
				if (VerticalWalls[(y * (Width + 1)) + (x + 1)])
					ret = false;
				break;
			case Direction.Down:
				if (HorizontalWalls[(y * Width) + x])
					ret = false;
				break;
			case Direction.Left:
				if (VerticalWalls[(y * (Width + 1)) + x])
					ret = false;
				break;
		}
		return ret;
	}

	public bool OnBoard(int x, int y)
	{
		return ((x >= 0) && (x < Width) && (y >= 0) && (y < Height));
	}
	
	public int GetIndex(int x, int y)
	{
		return y * Width + x;
	}

	public int GetIndex(Vector3 v)
	{
		return (Mathf.RoundToInt(v.y) * Width) + Mathf.RoundToInt(v.x);
	}
	// Update is called once per frame
	void Update () {

	}
	
	
}
