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

	//maximum number of mice before AddMouse() will stop adding and become a no-op
	public int mouseLimit;
	private int mouseCount;

	//Keep an index of (x,y) position -> MouseTrap so we can quickly get the MouseTrap object at (x,y) if there is one
	private Dictionary<int, MouseTrap> mouseTraps = new Dictionary<int, MouseTrap>();

	// Use this for initialization
	void Start () {
		mouseCount = 0;
		//Build Horizontal Walls
		for(int y = 0; y < Height + 1; y++)
		{
			for(int x = 0; x < Width; x++)
			{
				int index = (y * (Width)) + x;
				if (HorizontalWalls[index])
				{
					GameObject wall = Instantiate(horizontalWallPrefab, transform.parent);
					wall.transform.localPosition = new Vector3((float)x, (float)y, 0f);
				}
			}
		}
		//Build Vertical Walls
		for(int y = 0; y < Height; y++)
		{
			for(int x = 0; x < Width + 1; x++)
			{
				int index = (y * (Width + 1)) + x;
				if (VerticalWalls[index])
				{
					GameObject wall = Instantiate(verticalWallPrefab, transform.parent);
					wall.transform.localPosition = new Vector3((float)x, (float)y, 0f);
				}
			}
		}
		//Index MouseTraps
		MouseTrap[] traps = transform.parent.GetComponentsInChildren<MouseTrap>();
		foreach (MouseTrap trap in traps)
		{
			int index = (Mathf.RoundToInt(trap.position.y) * Width) + Mathf.RoundToInt(trap.position.x);
			mouseTraps.Add(index, trap);
		}
		
	}

	//Get the MouseTrap at position (x,y) or return null if there isn't one
	public MouseTrap GetTrap(int x, int y)
	{
		MouseTrap ret;
		int index = (y * Width) + x;
		if (!mouseTraps.TryGetValue(index, out ret))
			ret = null;
		return ret;
	}

	//Create a mouse at (x,y) that moves direction
	public void AddMouse(int x, int y, Direction direction)
	{
		if (mouseCount >= mouseLimit)
			return;
		GameObject o = Instantiate(mousePrefab, transform.parent);
		o.transform.localPosition = new Vector3((float)x, (float)y, 0f);
		o.GetComponent<Mouse>().direction = direction;
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
	
	}
	
}
