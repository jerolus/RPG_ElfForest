using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSelected : MonoBehaviour
{
	public bool avaiable = false;
	public InventoryItem.Type type;
	public Image icon;
	public Text stacksText;
	public GameObject selectedItemObject;

	[HideInInspector]
	public InventoryItem inventoryItem;
}
