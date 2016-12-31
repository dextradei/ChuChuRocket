using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Board : MonoBehaviour {
    [HideInInspector]
	public bool[] HorizontalWalls;
    [HideInInspector]
    public bool[] VerticalWalls;

    public int MouseLimit;

	public GameObject HorizontalWall;
	public GameObject VerticalWall;

    [HideInInspector]
	public int Width = 16;
    [HideInInspector]
	public int Height = 16;

    public GameObject MousePrefab;

    [System.NonSerialized]
    private int mouseCount;

    [System.NonSerialized]
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
                    GameObject wall = Instantiate(HorizontalWall, transform.parent);
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
                    GameObject wall = Instantiate(VerticalWall, transform.parent);
                    wall.transform.localPosition = new Vector3((float)x, (float)y, 0f);
                }
            }
        }
        //Index Mouse Traps
        MouseTrap[] traps = transform.parent.GetComponentsInChildren<MouseTrap>();
        foreach (MouseTrap trap in traps)
        {
            int index = (Mathf.RoundToInt(trap.position.y) * Width) + Mathf.RoundToInt(trap.position.x);
            mouseTraps.Add(index, trap);
        }

        //Test
        /*
        AddMouse(2, 2, Direction.Up);
        AddMouse(2, 3, Direction.Right);
        AddMouse(5, 5, Direction.Left);
        AddMouse(10, 4, Direction.Down);
        */
	}

    public MouseTrap GetTrap(int x, int y)
    {
        MouseTrap ret;
        int index = (y * Width) + x;
        if (!mouseTraps.TryGetValue(index, out ret))
            ret = null;
        return ret;
    }

    public void AddMouse(int x, int y, Direction direction)
    {
        if (mouseCount >= MouseLimit)
            return;
        GameObject o = Instantiate(MousePrefab, transform.parent);
        o.transform.localPosition = new Vector3((float)x, (float)y, 0f);
        o.GetComponent<Mouse>().direction = direction;
        mouseCount++;
    }

    public void RemoveMouse(GameObject mouse)
    {
        Destroy(mouse);
        mouseCount--;
    }

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
