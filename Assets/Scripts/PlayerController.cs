using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject selectorPrefab;
	public GameObject arrowPrefab;

	public int maxArrows = 3;

	private GameObject selector;
	private GameObject[] arrows;
	private int arrowIndex;

	protected GameController controller;

	private Transform gameArea;

	void Start () {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gameArea = GameObject.FindGameObjectWithTag("GameArea").transform;
		arrows = new GameObject[maxArrows];
		for(int i = 0; i < maxArrows; i++)
		{
			arrows[i] = null;
		}
		arrowIndex = 0;
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
}
