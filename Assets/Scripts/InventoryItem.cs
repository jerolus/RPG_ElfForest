using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public int stackLimit = 1;
	public int stacks = 1;
	public Text stacksText;

	private void Start()
	{
		UpdateStacksText();
	}

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

	public void SetStacks(int newStacks)
	{
		Debug.Log(newStacks);
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
}
