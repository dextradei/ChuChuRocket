using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject mousePrefab;
	
	//maximum number of mice before AddMouse() will stop adding and become a no-op
	public int mouseLimit;
	private int mouseCount;

	//Keep an index of (x,y) position -> MouseTrap/MouseSpawner/etc so we can quickly get the object at (x,y)
	private Dictionary<int, GameObject> gamePieces = new Dictionary<int, GameObject>();

	//every game piece needs to be a child of the GameArea so that (x,y) corresponds to a spot on the board
	private Transform gameArea;
	private Board board;

	void Start () {
		gameArea = GameObject.FindGameObjectWithTag("GameArea").transform;
		board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
		mouseCount = 0;

		//Index game pieces
		MouseTrap[] traps = gameArea.GetComponentsInChildren<MouseTrap>();
		foreach (MouseTrap trap in traps)
		{
			int index = board.GetIndex(trap.position);
			gamePieces.Add(index, trap.gameObject);
		}
		MouseSpawner[] spawners = gameArea.GetComponentsInChildren<MouseSpawner>();
		foreach (MouseSpawner spawner in spawners)
		{
			int index = board.GetIndex(spawner.position);
			gamePieces.Add(index, spawner.gameObject);
		}
	}

	public bool CanGo(int x, int y, Direction direction)
	{
		return board.CanGo(x, y, direction);
	}

	public Vector3 GetWorldGameAreaPoint(Vector3 worldPoint)
	{
		Vector3 gameAreaPoint = gameArea.InverseTransformPoint(worldPoint);
		return gameAreaPoint;
	}

	public bool OnBoard(int x, int y)
	{
		return board.OnBoard(x, y);
	}

	//Get the game piece at position (x,y) or return null if there isn't one
	public GameObject GetPiece(int x, int y)
	{
		GameObject ret;
		int index = board.GetIndex(x, y);
		if (!gamePieces.TryGetValue(index, out ret))
			ret = null;
		return ret;
	}

	public void AddPiece(GameObject obj, int x, int y)
	{
		int index = board.GetIndex(x, y);
		gamePieces.Add(index, obj);
	}

	public void RemovePiece(int x, int y)
	{
		int index = board.GetIndex(x, y);
		gamePieces.Remove(index);
	}

	public void RemovePiece(GameObject go)
	{
		foreach (KeyValuePair<int, GameObject> p in gamePieces)
		{
			if (p.Value == go)
			{
				gamePieces.Remove(p.Key);
				break;
			}
		}
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
	
}
