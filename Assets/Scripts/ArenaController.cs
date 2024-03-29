﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
	private static ArenaController m_instance;

	public CameraBehaviour cameraBehaviour;
	public GameObject enemyPrefab;
	public Transform enemyParent;
	public List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();
	public List<Transform> enemiesSpawnPoints = new List<Transform>();
	public int totalRoundEnemies = 4;
	public int maxEnemiesAttacking = 4;
	public int remainingEnemies;

	private GameController m_controller;
	private GameObject player;

	private void Awake()
	{
		SetInstance();
	}

	private void Start()
    {
		m_controller = GameController.GetInstance();
		SetRound();
		SetEnemies();
		player = Instantiate(m_controller.character, Vector3.zero, Quaternion.identity);
		cameraBehaviour.target = player.transform;
	}

	public static ArenaController GetInstance()
	{
		if (m_instance != null)
		{
			return m_instance;
		}
		else
		{
			return null;
		}
	}

	private void SetInstance()
	{
		if (!m_instance)
		{
			m_instance = this;
		}
	}

	private void SetRound()
	{
		totalRoundEnemies = 4 + m_controller.actualRound;
		remainingEnemies = totalRoundEnemies;
	}

	private void SetEnemies()
	{
		for (int i = 0; i < maxEnemiesAttacking; i++)
		{
			GameObject newEnemy = Instantiate(enemyPrefab, enemiesSpawnPoints[i].position, Quaternion.identity, enemyParent);
			enemies.Add(newEnemy.GetComponent<EnemyBehaviour>());
			remainingEnemies--;
		}

		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].fireRate = 1.0f - (0.01f * m_controller.actualRound);
		}
	}

	public void KillEnemy(EnemyBehaviour enemyKilled)
	{
		enemies.Remove(enemyKilled);
		if (enemies.Count == 0)
		{
			StartCoroutine(m_controller.GoToSelectMenuCoroutine());
		}
		else
		{
			StartCoroutine(KillEnemyCoroutine(enemyKilled.transform.position));
		}
	}

	private IEnumerator KillEnemyCoroutine(Vector3 position)
	{
		yield return new WaitForSeconds(1.2f);
		CreateEnemy(position);
	}

	private void CreateEnemy(Vector3 position)
	{
		if (remainingEnemies > 0)
		{
			GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity, enemyParent);
			enemies.Add(newEnemy.GetComponent<EnemyBehaviour>());
			remainingEnemies--;
		}
	}
}
