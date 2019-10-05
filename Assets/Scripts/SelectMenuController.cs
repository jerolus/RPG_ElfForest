using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuController : MonoBehaviour
{
	public GameObject characterToPlay;
	private GameController m_controller;

	private void Start()
	{
		m_controller = GameController.GetInstance();
		characterToPlay = m_controller.character;
	}

	public void SelectCharacter(GameObject characterSelected)
	{
		characterToPlay = characterSelected;
	}

	public void PlayArena()
	{
		StartCoroutine(m_controller.StartRoundCoroutine(characterToPlay));
	}
}
