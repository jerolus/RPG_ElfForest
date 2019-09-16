using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
	public EnemyStaticBehaviour enemyBehaviour;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Attack")
		{
			ArenaController.GetInstance().KillEnemy(enemyBehaviour);
			Destroy(this.transform.parent.gameObject);
		}
	}
}
