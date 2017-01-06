using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject cursorPrefab;
	public GameObject selectorPrefab;
	public GameObject arrowPrefab;
	public GameObject scoreBoard;

	public int maxArrows = 3;

	protected GameObject cursor;
	private GameObject selector;
	private GameObject[] arrows;
	private int arrowIndex;

	protected GameController controller;

	[System.NonSerialized]
	public int score = 0;

	private Transform gameArea;

	void Start () {
		Init();
	}

	protected void Init()
	{
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gameArea = GameObject.FindGameObjectWithTag("GameArea").transform;
		arrows = new GameObject[maxArrows];
		cursor = Instantiate(cursorPrefab, gameArea);
		for (int i = 0; i < maxArrows; i++)
		{
			arrows[i] = null;
		}
		arrowIndex = 0;
	}

	protected void MoveCursor(Vector3 move)
	{
		Vector3 newPosition = cursor.transform.position + move;
		//confine cursor to screen
		if (!Camera.main.rect.Contains(Camera.main.WorldToViewportPoint(newPosition)))
		{
			Vector3 bl = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMin, Camera.main.rect.yMin, 0f));
			Vector3 tr = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMax, Camera.main.rect.yMax, 0f));
			if (newPosition.x < bl.x)
				newPosition.x = bl.x;
			else if (cursor.transform.position.x > tr.x)
				newPosition.x = tr.x;
			if (newPosition.y < bl.y)
				newPosition.y = bl.y;
			else if (newPosition.y > tr.y)
				newPosition.y = tr.y;
		}
		newPosition.z = 0f;
		cursor.transform.position = newPosition;
	}
	
	protected void MoveSelector(int x, int y)
	{
		if (selector == null)
			selector = Instantiate(selectorPrefab, gameArea);
		selector.transform.localPosition = new Vector3((float)x, (float)y, 0f);
	}

	protected void RemoveSelector()
	{
		if (selector != null)
		{
			Destroy(selector);
			selector = null;
		}
	}

	protected void PlaceArrow(int x, int y, Direction dir)
	{
		GameObject arrow = Instantiate(arrowPrefab, gameArea);
		arrow.transform.localPosition = new Vector3((float)x, (float)y, 0f);
		arrow.GetComponent<Arrow>().direction = dir;
		controller.AddPiece(arrow, x, y);
		if (arrows[arrowIndex] != null)
		{
			Destroy(arrows[arrowIndex]);
			controller.RemovePiece(arrows[arrowIndex]);
		}
		arrows[arrowIndex] = arrow;
		arrowIndex++;
		if (arrowIndex >= maxArrows)
			arrowIndex = 0;
	}

	public void AddScore(int points)
	{
		score += points;
		scoreBoard.GetComponent<Text>().text = score.ToString();
	}
}
