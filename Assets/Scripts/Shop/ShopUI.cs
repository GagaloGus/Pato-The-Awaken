using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject KeySignE, KeySignT, Shop;
    public void Start()
    {
        Shop = transform.Find("ShopUI").gameObject;
        KeySignE = transform.Find("Key Sign Elevator").gameObject;
        KeySignT = transform.Find("Key Sign Tienda").gameObject;
    }
}

