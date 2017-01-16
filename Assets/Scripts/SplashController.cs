using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour {
	void Start () {		
	}

	void Update () {
	}

	void NextScene () {
		SceneManager.LoadScene ( SceneManager.GetActiveScene().buildIndex + 1 );
	}
}
