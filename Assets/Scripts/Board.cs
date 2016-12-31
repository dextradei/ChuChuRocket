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

	public GameObject mousePrefab;

	public GameObject selectorPrefab;
	private GameObject selector = null;

	public GameObject arrowPrefab;

	//maximum number of mice before AddMouse() will stop adding and become a no-op
	public int mouseLimit;
	private int mouseCount;

	//Keep an index of (x,y) position -> MouseTrap/MouseSpawner/etc so we can quickly get the object at (x,y)
	private Dictionary<int, GameObject> boardPieces = new Dictionary<int, GameObject>();

	//every game piece needs to be a child of the GameArea so that (x,y) corresponds to a spot on the board
	private Transform gameArea;

	void Start()
	{
		gameArea = GameObject.FindGameObjectWithTag("GameArea").transform;

		mouseCount = 0;
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
		//Index MouseTraps
		MouseTrap[] traps = gameArea.GetComponentsInChildren<MouseTrap>();
		foreach (MouseTrap trap in traps)
		{
			int index = (Mathf.RoundToInt(trap.position.y) * Width) + Mathf.RoundToInt(trap.position.x);
			boardPieces.Add(index, trap.gameObject);
		}
		MouseSpawner[] spawners = gameArea.GetComponentsInChildren<MouseSpawner>();
		foreach (MouseSpawner spawner in spawners)
		{
			int index = (Mathf.RoundToInt(spawner.position.y) * Width) + Mathf.RoundToInt(spawner.position.x);
			boardPieces.Add(index, spawner.gameObject);
		}
	}

	//Get the game piece at position (x,y) or return null if there isn't one
	public GameObject GetPiece(int x, int y)
	{
		GameObject ret;
		int index = (y * Width) + x;
		if (!boardPieces.TryGetValue(index, out ret))
			ret = null;
		return ret;
	}

	//Create a mouse at (x,y) that moves direction
	public void AddMouse(int x, int y, Direction direction)
	{
		if (mouseCount >= mouseLimit)
			return;
		GameObject mouse = Instantiate(mousePrefab, gameArea);
		mouse.transform.localPosition = new Vector3((float)x, (float)y, 0f);
		mouse.GetComponent<Mouse>().direction = direction;
		mouseCount++;
	}

	//Remove a mouse
	public void RemoveMouse(GameObject mouse)
	{
		Destroy(mouse);
		mouseCount--;
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
	
	// Update is called once per frame
	void Update () {
		//detect mouse position on board
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPoint.z = 0f;
		Vector3 boardPoint = gameArea.InverseTransformPoint(worldPoint);

		//find what square this point is on
		int x = Mathf.FloorToInt(boardPoint.x);
		int y = Mathf.FloorToInt(boardPoint.y);

		//if it's a valid place to put an arrow, put the highlighter there
		if ((x >= 0) && (x < Width) && (y >= 0) && (y < Height) && (GetPiece(x, y) == null))
		{
			MoveSelector(x, y);
			//TODO: fix project input settings and use Axes instead of GetKey
			if (Input.GetKey(KeyCode.W))
			{
				//place an up arrow
				GameObject arrow = Instantiate(arrowPrefab, gameArea);
				arrow.transform.localPosition = new Vector3((float)x, (float)y, 0f);
				arrow.GetComponent<Arrow>().direction = Direction.Up;
				int index = (y * (Width)) + x;
				boardPieces.Add(index, arrow);
			}
		}
		else
		{
			RemoveSelector();
		}
	}

	void MoveSelector(int x, int y)
	{
		if (selector == null)
			selector = Instantiate(selectorPrefab, gameArea);
		selector.transform.localPosition = new Vector3((float)x, (float)y, 0f);
	}

	void RemoveSelector()
	{
		if (selector != null)
		{
			Destroy(selector);
			selector = null;
		}
	}
	
}
