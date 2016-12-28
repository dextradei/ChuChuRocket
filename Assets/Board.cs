using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
	public bool[] HorizontalWalls;
	public bool[] VerticalWalls;

	public GameObject HorizontalWall;
	public GameObject VerticalWall;

	public int Width = 16;
	public int Height = 16;

	// Use this for initialization
	void Start () {
		int max = (Width + 1) * (Height + 1);

		int w = Width + 1;
		for(int index = 0; index < max; index++) {
			int x = index % w;
			int y = index / w;
			//Debug.Log ("x,y" + x + "," + y);
			if ((y == 0) || (y == 16)) {
				HorizontalWalls[index] = true;
			}
			if ((x == 0) || (x == 16)) {
				VerticalWalls[index] = true;
			}

			if (HorizontalWalls[index] && (x < 16)) {
                //Debug.Log ("x,y" + x + "," + y);
                //Instantiate (HorizontalWall, new Vector3((float)x, (float)y, 0f), Quaternion.identity, transform.parent);
                GameObject wall = Instantiate(HorizontalWall, transform.parent);
                wall.transform.localPosition = new Vector3((float)x, (float)y, 0f);
			}
			if (VerticalWalls[index] && (y < 16)) {
                //Debug.Log ("x,y" + x + "," + y);
                //Instantiate (VerticalWall, new Vector3((float)x, (float)y, 0f), Quaternion.identity, transform.parent);
                GameObject wall = Instantiate(VerticalWall, transform.parent);
                wall.transform.localPosition = new Vector3((float)x, (float)y, 0f);
            }
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
