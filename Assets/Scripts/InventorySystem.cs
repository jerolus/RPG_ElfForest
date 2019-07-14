using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
	private static InventorySystem m_instance;

	public List<InventoryItem> inventory = new List<InventoryItem>();
	public Transform inventoryParent;

	public GameObject inventorySlotSword;
	public GameObject inventorySlotBow;
	public GameObject inventorySlotArrow;
	public GameObject inventorySlotLifePotion;

	[HideInInspector]
	public InventoryItem currentWeapon;

	private void Awake()
	{
		SetInstance();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			inventoryParent.gameObject.SetActive(!inventoryParent.gameObject.activeSelf);
		}
	}

	public static InventorySystem GetInstance()
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

	public void AddInventoryItem(InventoryItem.Type item)
	{
		GameObject inventorySlotToInstantiate = null;

		switch (item)
		{
			case InventoryItem.Type.Sword:
				inventorySlotToInstantiate = inventorySlotSword;
				break;
			case InventoryItem.Type.Bow:
				inventorySlotToInstantiate = inventorySlotBow;
				break;
			case InventoryItem.Type.Arrow:
				inventorySlotToInstantiate = inventorySlotArrow;
				break;
			case InventoryItem.Type.LifePotion:
				inventorySlotToInstantiate = inventorySlotLifePotion;
				break;
		}

		GameObject newInventorySlot = Instantiate(inventorySlotToInstantiate, inventoryParent);
		InventoryItem newInventoryItem = newInventorySlot.GetComponent<InventoryItem>();
		inventory.Add(newInventoryItem);
	}

	public void RemoveArrow()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].type == InventoryItem.Type.Arrow)
			{
				Destroy(inventory[i].gameObject);
				inventory.Remove(inventory[i]);
				break;
			}
		}
	}

	public int GetTypeNumber(InventoryItem.Type typeToCompare)
	{
		int numberToReturn = 0;
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].type == typeToCompare)
			{
				numberToReturn++;
			}
		}
		return numberToReturn;
	}
}
