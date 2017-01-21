using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour {

	//the color selection spots
	public GameObject[] colorBox;
	//the keyboard/mouse and gamepad selector gameObjects
	public GameObject keyboardMouse;
	public GameObject[] gamepad;

	//number of gamepads detected at start
	private int gamepadCount;

	//each gamepad/keyboardMouse Position is 0->4 for the 5 possible spots none,red,blue,green,yellow
	private int keyboardMousePosition = 0;
	private int[] gamepadPosition;
	//when using the joystick, only move every so often
	public float gamepadMoveDelay = 0.2f;
	private float[] gamepadStartTime;

	void Start ()
	{
		UpdateGamepadCount();
		Debug.Log("Number of Gamepads: " + gamepadCount);
		UpdateGamepadVisibility();
		gamepadPosition = new int[gamepadCount];
		gamepadStartTime = new float[gamepadCount];
		for (int i = 0; i < gamepadCount; i++)
		{
			gamepadPosition[i] = 0;
			gamepadStartTime[i] = Time.time;
		}
	}

	//detect how many gamepads are connected
	private void UpdateGamepadCount()
	{
		string[] gamepadNames = Input.GetJoystickNames();

		gamepadCount = 0;
		foreach (string name in gamepadNames)
		{
			Debug.Log(name);
			if (name.Length > 0)
				gamepadCount++;
		}
	}

	//hide gamepad selector images based on number of detected gamepads
	private void UpdateGamepadVisibility()
	{
		for (int i = 0; i < gamepad.Length; i++)
		{
			if (i < gamepadCount)
			{
				gamepad[i].GetComponent<SpriteRenderer>().enabled = true;
			}
			else
			{
				gamepad[i].GetComponent<SpriteRenderer>().enabled = false;
			}
		}
	}

	void Update () {
		DoKeyboardMouseInput();
		for (int i = 0; i < gamepadCount; i++)
			DoGamepadInput(i);
	}

	void DoGamepadInput(int i)
	{
		float h = Input.GetAxis("C" + (i + 1) + ".Horizontal");
		float mh = Input.GetAxis("C" + (i + 1) + ".MoveHorizontal");
		if (((h > 0f) || (mh > 0f)) && (Time.time - gamepadStartTime[i] > gamepadMoveDelay))
		{
			MoveGamepadRight(i);
		}
		else if (((h < 0f) || (mh < 0f)) && (Time.time - gamepadStartTime[i] > gamepadMoveDelay))
		{
			MoveGamepadLeft(i);
		}
	}

	private void MoveGamepadLeft(int i)
	{
		gamepadPosition[i]--;
		if (gamepadPosition[i] < 0) gamepadPosition[i] = 4;
		UpdateGamepadPosition(i);
		gamepadStartTime[i] = Time.time;
	}

	private void MoveGamepadRight(int i)
	{
		gamepadPosition[i]++;
		if (gamepadPosition[i] > 4) gamepadPosition[i] = 0;
		UpdateGamepadPosition(i);
		gamepadStartTime[i] = Time.time;
	}

	void UpdateGamepadPosition(int i)
	{
		float x = colorBox[gamepadPosition[i]].transform.localPosition.x;
		float y = gamepad[i].transform.localPosition.y;
		gamepad[i].transform.localPosition = new Vector3(x, y, 0);
	}

	void DoKeyboardMouseInput()
	{
		if (Input.GetButtonDown("MK.MoveRight"))
		{
			keyboardMousePosition++;
			if (keyboardMousePosition > 4) keyboardMousePosition = 0;
			UpdateKeyboardMousePosition();
		}
		else if (Input.GetButtonDown("MK.MoveLeft"))
		{
			keyboardMousePosition--;
			if (keyboardMousePosition < 0) keyboardMousePosition = 4;
			UpdateKeyboardMousePosition();
		}
	}

	void UpdateKeyboardMousePosition()
	{
		float x = colorBox[keyboardMousePosition].transform.localPosition.x;
		float y = keyboardMouse.transform.localPosition.y;
		keyboardMouse.transform.localPosition = new Vector3(x, y, 0);
	}
}
