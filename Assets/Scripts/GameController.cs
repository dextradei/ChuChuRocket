using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private GameObject selector = null;

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

	public Vector3 GetMouseGameAreaPoint()
	{
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPoint.z = 0f;
		Vector3 gameAreaPoint = gameArea.InverseTransformPoint(worldPoint);
		return gameAreaPoint;
	}

	public void MoveSelector(GameObject selectorPrefab, int x, int y)
	{
		if (selector == null)
			selector = Instantiate(selectorPrefab, gameArea);
		selector.transform.localPosition = new Vector3((float)x, (float)y, 0f);
	}

	public void RemoveSelector()
	{
		if (selector != null)
		{
			Destroy(selector);
			selector = null;
		}
	}

	public void PlaceArrow(GameObject arrowPrefab, int x, int y)
	{
		GameObject arrow = Instantiate(arrowPrefab, gameArea);
		arrow.transform.localPosition = new Vector3((float)x, (float)y, 0f);
		arrow.GetComponent<Arrow>().direction = Direction.Up;
		int index = board.GetIndex(x, y);
		gamePieces.Add(index, arrow);
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
