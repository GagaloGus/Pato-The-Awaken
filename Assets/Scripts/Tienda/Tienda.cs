using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    string x;
    ShopObject[] shop;
    public GameObject[] Buttons;
    void Start()
    {
        shop = new ShopObject[6];
        shop[0] = new ShopObject("Objeto1", 20);
        shop[1] = new ShopObject("Objeto2", 50);
        shop[2] = new ShopObject("Objeto3", 75);
        shop[3] = new ShopObject("Objeto4", 100);
        shop[4] = new ShopObject("Objeto5", 150);
        shop[5] = new ShopObject("Objeto6", 200);

        int count = 0;
        foreach(GameObject Obj in Buttons)
        {
            Obj.name = count.ToString(); count++;
            Obj.GetComponent<Button>().onClick.AddListener(() => { Comprar(Obj); });
        }

    }

    void Update()
    {

    }

    public void Comprar(GameObject B)
    {

       if (GameManager.instance.gm_coins >= shop[int.Parse(B.name)].GetValue())
       {
            Debug.Log("Has comprado" + shop[int.Parse(B.name)].GetName() + " !");
            GameManager.instance.AddCoin(-shop[int.Parse(B.name)].GetValue());
       }

       else
       {
           Debug.Log("Aun no tienes el suficiente dinero!");
       }
    }
}
