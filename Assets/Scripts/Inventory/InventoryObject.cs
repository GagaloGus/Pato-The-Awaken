using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Create new item")]
public class InventoryObject : ScriptableObject
{
    public string itemName, description;
    public Sprite itemSprite;
    public float activeTime;
}
