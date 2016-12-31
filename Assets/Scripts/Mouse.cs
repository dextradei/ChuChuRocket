using UnityEngine;
using System.Collections;

public enum Direction { Up, Right, Down, Left };

public class Mouse : MonoBehaviour {

	public int velocity;
    public GameObject MouseImage;

	public Direction direction = Direction.Up;

    private float startMoveTime;
    private Vector3 destination;
    private Vector3 position;

    private Board board;

    //temporary?
    public float lifeTime;
    private float startTime;
    //end temporary

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        //make sure we're centered
        position = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), 0f);
        transform.localPosition = position;
        //force Update to SetDestination
        destination = position;
    }
	
	// Update is called once per frame
	void Update () {
        //temporary?
        //make mice have limited life time
        if (Time.time - startTime > lifeTime)
        {
            board.RemoveMouse(gameObject);
            return;
        }
        //end temporary
		if (Vector3.Distance(transform.localPosition, destination) < (velocity * Time.deltaTime))
        {
            startMoveTime = Time.time;
            //transform.localPosition = destination;
            position = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), 0f);
            if (!Trapped())
                SetDestination();
        }
        transform.localPosition = Vector3.Lerp(position, destination, (Time.time - startMoveTime) * velocity);
	}

    bool Trapped()
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        //if we're on a trap, remove the mouse
        MouseTrap trap = board.GetTrap(x, y);
        if (trap != null)
        {
            //mouse hit a trap
            //TODO: figure out which player owns the trap and update score
            board.RemoveMouse(gameObject);
            return true;
        }
        return false;
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
                MouseImage.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case Direction.Right:
                MouseImage.transform.eulerAngles = new Vector3(0f, 0f, 270f);
                break;
            case Direction.Down:
                MouseImage.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                break;
            case Direction.Left:
                MouseImage.transform.eulerAngles = new Vector3(0f, 0f, 90f);
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

    Vector3 GetDirectionVector()
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector3.up;
            case Direction.Right:
                return Vector3.right;
            case Direction.Down:
                return Vector3.down;
            case Direction.Left:
                return Vector3.left;
        }
        return Vector3.zero;
    }
}
