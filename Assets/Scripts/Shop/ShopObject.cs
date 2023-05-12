using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObject : MonoBehaviour
{
    //private Sprite sprite;
    private string name;
    private int Value;

    public ShopObject()
    {

    }

    public ShopObject(string n, int v)
    {
        //sprite = s;
        name = n;
        Value = v;
    }

    //public Sprite GetSprite()
    //{
    //    return sprite;
    //}

    public string GetName()
    {
        return name;
    }

    public int GetValue()
    {
        return Value;
    }
}
