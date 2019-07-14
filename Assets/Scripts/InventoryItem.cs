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

	public void OnClickItemInventory()
	{
		switch (type)
		{
			case Type.Sword:
				InventorySystem.GetInstance().currentWeapon = this;
				break;
			case Type.Bow:
				InventorySystem.GetInstance().currentWeapon = this;
				break;
			case Type.Arrow:
				break;
			case Type.LifePotion:
				break;
		}
	}
}
