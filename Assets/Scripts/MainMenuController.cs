using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

	public void NextScene ()
	{
		Debug.Log ("NextScene");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void Quit ()
	{
		Debug.Log ("NextScene");
		Application.Quit ();
	}
}
