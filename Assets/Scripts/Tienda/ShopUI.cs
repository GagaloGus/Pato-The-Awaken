using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject KeySign, Shop;
    public void Start()
    {
        Shop = transform.Find("ShopUI").gameObject;
        KeySign = transform.Find("Key Sign").gameObject;
    }

    
}

