using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    public int obj;
    ShopObject[] shop;
    void Start()
    {
        shop = new ShopObject[6];
        shop[0] = new ShopObject("Objeto1", 20);
        shop[1] = new ShopObject("Objeto2", 50);
        shop[2] = new ShopObject("Objeto3", 75);
        shop[3] = new ShopObject("Objeto4", 100);
        shop[4] = new ShopObject("Objeto5", 150);
        shop[5] = new ShopObject("Objeto6", 200);
        Comprar();
    }

    void Update()
    {

    }

    public void Comprar()
    {
       //if (GameManager.instance.gm_coins >= shop[0].(uint)GetValue)
       //{
       //    Debug.Log("¡Has comprado el objeto" + obj + " !");
       //    GameManager.instance.AddCoin(-20);
       //}
       //else
       //{
       //    Debug.Log("¡Aun no tienes el suficiente dinero!");
       //}
    }
}
