using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
    [HideInInspector]
	public bool[] HorizontalWalls;
    [HideInInspector]
    public bool[] VerticalWalls;

	public GameObject HorizontalWall;
	public GameObject VerticalWall;

    [HideInInspector]
	public int Width = 16;
    [HideInInspector]
	public int Height = 16;

	// Use this for initialization
	void Start () {
        //Horizontal Walls
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
        //Vertical Walls
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
