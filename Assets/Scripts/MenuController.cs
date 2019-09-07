using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	public void StartGame()
	{
		GameController.GetInstance().StartCoroutine(GameController.GetInstance().GoToTownCoroutine());
	}

	public void Options()
	{
		
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
