using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
	public enum Type
	{
		Sword,
		Bow,
		Arrow,
		LifePotion
	}

	public Type type;
	public int stackLimit = 1;
	public int stacks = 1;
	public Text stacksText;
	public Sprite icon;
	public bool m_usable = false;

	[HideInInspector]
	public Vector3 returnPosition;

	private InventorySystem m_inventorySystem;

	private void Start()
	{
		m_inventorySystem = InventorySystem.GetInstance();
		UpdateStacksText();
		switch (type)
		{
			case Type.Sword:
				m_usable = true;
				break;
			case Type.Bow:
				m_usable = true;
				break;
			case Type.Arrow:
				m_usable = false;
				break;
			case Type.LifePotion:
				m_usable = true;
				break;
		}
	}

	public void SetStacks(int newStacks)
	{
		stacks = newStacks;
		UpdateStacksText();
	}

	private void UpdateStacksText()
	{
		if (stacks == 1 || stacks == 0)
		{
			stacksText.text = "";
		}
		else
		{
			stacksText.text = stacks.ToString();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		returnPosition = transform.localPosition;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (RectTransformUtility.RectangleContainsScreenPoint(m_inventorySystem.inventoryParent as RectTransform, Input.mousePosition))
		{
			transform.localPosition = returnPosition;
		}
		else if (RectTransformUtility.RectangleContainsScreenPoint(m_inventorySystem.inventoryItemsSelected[0].transform as RectTransform, Input.mousePosition))
		{
			SelectInventoryItem(0);
		}
		else if (RectTransformUtility.RectangleContainsScreenPoint(m_inventorySystem.inventoryItemsSelected[1].transform as RectTransform, Input.mousePosition))
		{
			SelectInventoryItem(1);
		}
		else if (RectTransformUtility.RectangleContainsScreenPoint(m_inventorySystem.inventoryItemsSelected[2].transform as RectTransform, Input.mousePosition))
		{
			SelectInventoryItem(2);
		}
		else
		{
			if (m_usable)
			{
				transform.localPosition = returnPosition;
			}
			else
			{
				m_inventorySystem.currentItemToDestroy = this;
				m_inventorySystem.destroyItemMessage.SetActive(true);
			}
		}
	}

	private void SelectInventoryItem(int index)
	{
		if (m_usable)
		{
			if (m_inventorySystem.inventoryItemsSelected[index].avaiable)
			{
				m_inventorySystem.inventoryItemsSelected[index].inventoryItem.gameObject.SetActive(true);
				m_inventorySystem.inventory.Add(m_inventorySystem.inventoryItemsSelected[index].inventoryItem);
				m_inventorySystem.inventoryItemsSelected[index].inventoryItem.transform.SetParent(m_inventorySystem.inventoryParent);
			}
			
			m_inventorySystem.inventoryItemsSelected[index].icon.gameObject.SetActive(true);
			m_inventorySystem.inventoryItemsSelected[index].icon.sprite = icon;
			m_inventorySystem.inventoryItemsSelected[index].avaiable = true;
			m_inventorySystem.inventoryItemsSelected[index].type = type;
			m_inventorySystem.inventoryItemsSelected[index].inventoryItem = this;
			m_inventorySystem.inventory.Remove(this);
			transform.SetParent(m_inventorySystem.inventoryItemsSelected[index].transform);
			gameObject.SetActive(false);

			if (m_inventorySystem.inventoryItemsSelected[index].selectedItemObject.activeSelf)
			{
				m_inventorySystem.SelectInventoryItem(m_inventorySystem.inventoryItemsSelected[index], true);
			}
		}
		else
		{
			transform.localPosition = returnPosition;
		}
	}
}
