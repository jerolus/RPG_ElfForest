using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointBehaviour : MonoBehaviour
{
	public Transform teleportPoint;
	public string teleportPlaceName;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			StartCoroutine(PlayerBehaviour.GetInstance().TeleportTransition(teleportPoint, teleportPlaceName));
		}
	}
}
