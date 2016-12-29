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
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
