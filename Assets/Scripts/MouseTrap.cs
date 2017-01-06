using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour {

	public GameObject player;

	[System.NonSerialized]
	public Vector3 position;

	void Awake () {
		//Make sure we're centered.  This has to be done before Board::Start()
		position = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), 0f);
		transform.localPosition = position;
	}
}
