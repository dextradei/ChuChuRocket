using UnityEngine;
using System.Collections;

public enum Direction { Up, Right, Down, Left };

public class Mouse : MonoBehaviour {

	public int velocity;

	private Direction direction = Direction.Up;

	public GameObject board;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
