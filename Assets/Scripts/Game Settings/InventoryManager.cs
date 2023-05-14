using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventoryObject[] items;

    int[] itemAmount = new int[6];
    void Awake()
    {
        if (!instance) //comprueba que instance no tenga informacion
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //si tiene info, ya existe un GM
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < itemAmount.Length; i++)
        {
            itemAmount[i] = Random.Range(0, 6);
            items[i].order = i;
        }
    }

    public int[] getItemAmount
    {
        get { return itemAmount; }
    }

    public void ChangeItemAmount(int number, int amount)
    {
        itemAmount[number] += amount;
        Mathf.Clamp(itemAmount[number], 0, Mathf.Infinity);
    }
}
