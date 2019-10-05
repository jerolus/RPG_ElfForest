using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	private GameController m_controller;

	private void Start()
	{
		m_controller = GameController.GetInstance();
	}
	public void StartGame()
	{
		StartCoroutine(m_controller.GoToSelectMenuCoroutine());
	}

	public void Options()
	{
		
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
