using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    public string[] shop;
    void Start()
    {
        shop = new string[6];
        shop[0] = "Objeto1";
        shop[1] = "Objeto2";
        shop[2] = "Objeto3";
        shop[3] = "Objeto4";
        shop[4] = "Objeto5";
        shop[5] = "Objeto6";
    }

    void Update()
    {

    }

    void Comprar()
    {

    }
}
