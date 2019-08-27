using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	public enum Type
	{
		Money,
		Life
	}

	public Type type;

	public int value;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			switch (type)
			{
				case Type.Money:
					GameController.GetInstance().money += value;
					break;
				case Type.Life:
					PlayerBehaviour.GetInstance().life += value;
					break;
			}
			Destroy(gameObject);
		}
	}
}
