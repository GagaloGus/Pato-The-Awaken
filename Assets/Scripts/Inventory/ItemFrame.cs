using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    public GameObject holdingObject ,Inventory;
    public void CheckIfGameStarted()
    {
        if (!GameManager.instance.gameStarted)
        {
            Inventory.SetActive(true);
        }
        else
        {
            if(holdingObject != null) { holdingObject.GetComponent<ItemController>().Item.Use(); }
            else { print("not holding any object!"); }
        }
    }
}
