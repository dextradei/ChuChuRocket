using UnityEngine;
using System.Collections;

public enum Direction { Up, Right, Down, Left };

public class Mouse : MonoBehaviour {

	public GameObject mouseImage;
	
	public Direction direction = Direction.Up;
	public int velocity;
	private float startMoveTime;
	private Vector3 destination;
	private Vector3 position;

	private Board board;

	public float lifeTime;
	private float startTime;

	void Start () {
		startTime = Time.time;
		board = GameObject.FindWithTag("Board").GetComponent<Board>();
		//make sure we're centered
		SetPosition();
		transform.localPosition = position;
		//force Update() to call SetDestination()
		destination = position;
	}
	
	// Update is called once per frame
	void Update () {
		//make mice have limited life time
		if (Time.time - startTime > lifeTime)
		{
			board.RemoveMouse(gameObject);
			return;
		}

		//if we reached destination
		if (Vector3.Distance(transform.localPosition, destination) <= (velocity * Time.deltaTime) / 2f)
		{
			SetPosition();
			if (Trapped())
			{
				//TODO: figure out which player owns the trap and update score
				board.RemoveMouse(gameObject);
				return;
			}
			//set a new destination and start moving to it
			startMoveTime = Time.time;
			SetDestination();
		}
		//animate movement
		transform.localPosition = Vector3.Lerp(position, destination, (Time.time - startMoveTime) * velocity);
	}

	void SetPosition()
	{
		//position is the nearest grid point to current location
		position = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), 0f);
	}

	bool Trapped()
	{
		int x = Mathf.RoundToInt(position.x);
		int y = Mathf.RoundToInt(position.y);
		MouseTrap trap = board.GetTrap(x, y);
		return (trap != null);
	}

	void SetDestination()
	{
		int x = Mathf.RoundToInt(position.x);
		int y = Mathf.RoundToInt(position.y);
		int i;
		//If we can't go direction, Turn
		//Only try 4 times and then give up with error.
		for(i = 0; i < 4; i++)
		{
			if (!board.CanGo(x, y, direction))
			{
				//Debug.Log("Turn x,y: " + x + "," + y);
				Turn();
				continue;
			}
			//Debug.Log("CanGo x,y: " + x + "," + y);
			break;
		}
		if (i == 4)
		{
			//Debug.Log("Mouse can't move");
			destination = position;
			return;
		}
		
		SetRotation();
		switch (direction)
		{
			case Direction.Up:
				destination = new Vector3((float)x, (float)(y + 1), 0f);
				break;
			case Direction.Right:
				destination = new Vector3((float)(x + 1), (float)y, 0f);
				break;
			case Direction.Down:
				destination = new Vector3((float)x, (float)(y - 1), 0f);
				break;
			case Direction.Left:
				destination = new Vector3((float)(x - 1), (float)y, 0f);
				break;
		}
		//Debug.Log("position: " + position);
		//Debug.Log("destination: " + destination);
	}

	void SetRotation()
	{
		switch (direction)
		{
			case Direction.Up:
				mouseImage.transform.eulerAngles = new Vector3(0f, 0f, 0f);
				break;
			case Direction.Right:
				mouseImage.transform.eulerAngles = new Vector3(0f, 0f, 270f);
				break;
			case Direction.Down:
				mouseImage.transform.eulerAngles = new Vector3(0f, 0f, 180f);
				break;
			case Direction.Left:
				mouseImage.transform.eulerAngles = new Vector3(0f, 0f, 90f);
				break;
		}
	}
	
	void Turn()
	{
		switch(direction)
		{
			case Direction.Up:
				direction = Direction.Right;
				break;
			case Direction.Right:
				direction = Direction.Down;
				break;
			case Direction.Down:
				direction = Direction.Left;
				break;
			case Direction.Left:
				direction = Direction.Up;
				break;
		}
	}
}
