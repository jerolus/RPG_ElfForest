using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
	private static InventorySystem m_instance;

	public List<InventoryItem> inventory = new List<InventoryItem>();
	public Transform inventoryParent;
	public int maxSlots = 15;

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

	public void AddInventoryItem(InventoryItem.Type typeToCompare)
	{
		InventoryItem inventoryItemToCheck = GetPrefabByType(typeToCompare).GetComponent<InventoryItem>();
		if (inventoryItemToCheck.stackLimit != 1)
		{
			if (CheckAllItemsFull(typeToCompare))
			{
				GameObject newInventorySlot = Instantiate(GetPrefabByType(typeToCompare), inventoryParent);
				InventoryItem newInventoryItem = newInventorySlot.GetComponent<InventoryItem>();
				inventory.Add(newInventoryItem);
			}
			else
			{
				int stacksToFill = inventoryItemToCheck.stacks;
				for (int i = 0; i < inventory.Count; i++)
				{
					if (inventory[i].type == typeToCompare && inventory[i].stacks < inventory[i].stackLimit)
					{
						inventory[i].SetStacks(inventory[i].stacks + stacksToFill);

						if (inventory[i].stacks > inventory[i].stackLimit)
						{
							stacksToFill = inventory[i].stacks - inventory[i].stackLimit;
						}
						else
						{
							break;
						}
					}
				}
			}		
		}
		else
		{
			GameObject newInventorySlot = Instantiate(GetPrefabByType(typeToCompare), inventoryParent);
			InventoryItem newInventoryItem = newInventorySlot.GetComponent<InventoryItem>();
			inventory.Add(newInventoryItem);
		}

	}

	private bool CheckAllItemsFull(InventoryItem.Type typeToCompare)
	{
		bool toReturn = true;

		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].type == typeToCompare && inventory[i].stacks < inventory[i].stackLimit)
			{
				toReturn = false;
				break;
			}
		}

		return toReturn;
	}

	public bool CanAddStackType(InventoryItem.Type typeToCompare)
	{
		bool toReturn = false;

		if (inventory.Count < maxSlots)
		{
			toReturn = true;
		}
		else
		{
			InventoryItem inventoryItemToCheck = GetPrefabByType(typeToCompare).GetComponent<InventoryItem>();
			if (inventoryItemToCheck.stackLimit != 1)
			{
				int avaiableStacks = 0;

				for (int i = 0; i < inventory.Count; i++)
				{
					if (inventory[i].type == typeToCompare)
					{
						avaiableStacks = avaiableStacks + (inventory[i].stackLimit - inventory[i].stacks);
					}
				}

				if (avaiableStacks != 0)
				{
					toReturn = true;
				}
			}
		}

		return toReturn;
	}

	public void RemoveStackType(InventoryItem.Type typeToCompare)
	{
		if (CheckAllItemsFull(typeToCompare))
		{
			for (int i = 0; i < inventory.Count; i++)
			{
				if (inventory[i].type == typeToCompare)
				{
					inventory[i].SetStacks(inventory[i].stacks - 1);
					if (inventory[i].stacks == 0)
					{
						Destroy(inventory[i].gameObject);
						inventory.Remove(inventory[i]);
					}
					break;
				}
			}
		}
		else
		{
			for (int i = 0; i < inventory.Count; i++)
			{
				if (inventory[i].type == typeToCompare && inventory[i].stacks < inventory[i].stackLimit)
				{
					inventory[i].SetStacks(inventory[i].stacks - 1);
					if (inventory[i].stacks == 0)
					{
						Destroy(inventory[i].gameObject);
						inventory.Remove(inventory[i]);
					}
					break;
				}
			}
		}
	}

	public int GetStacksNumber(InventoryItem.Type typeToCompare)
	{
		int numberToReturn = 0;
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].type == typeToCompare)
			{
				numberToReturn += inventory[i].stacks;
			}
		}
		return numberToReturn;
	}

	private GameObject GetPrefabByType(InventoryItem.Type typeToCompare)
	{
		GameObject itemToReturn = null;

		switch (typeToCompare)
		{
			case InventoryItem.Type.Sword:
				itemToReturn = inventorySlotSword;
				break;
			case InventoryItem.Type.Bow:
				itemToReturn = inventorySlotBow;
				break;
			case InventoryItem.Type.Arrow:
				itemToReturn = inventorySlotArrow;
				break;
			case InventoryItem.Type.LifePotion:
				itemToReturn = inventorySlotLifePotion;
				break;
		}

		return itemToReturn;
	}
}
