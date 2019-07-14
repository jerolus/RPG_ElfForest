using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	public InventoryItem.Type inventoryItemType;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			InventorySystem.GetInstance().AddInventoryItem(inventoryItemType);
			Destroy(gameObject);
		}
	}
}
