using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
	public enum Type
	{
		Sword,
		Bow,
		Arrow,
		LifePotion
	}

	public Type type;
}
