using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTrigger : MonoBehaviour
{

	public SplashController controller;

	public void FireTrigger ()
	{
		controller.NextScene ();
	}
}
