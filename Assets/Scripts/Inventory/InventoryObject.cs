using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Create new item")]
public class InventoryObject : ScriptableObject
{
    public string itemName, itemDescription;
    public Sprite itemSprite;
    public float itemActiveTime;

    public void Use()
    {
        FindObjectOfType<Player_Controller>().StartBuffCoroutine(this);
    }
    public int ItemCost;
}
