using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    public int ItemSelected = 0;
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
            Obj.GetComponent<Button>().onClick.AddListener(() => { SelectItem(Obj); });
        }

        transform.Find("Confirm").gameObject.GetComponent<Button>().onClick.AddListener(() => { Comprar(); });
    }

    void Update()
    {
       
    }

    public void Comprar()
    {
        if(ItemSelected > -1)
        {
            if (GameManager.instance.gm_coins >= shop[ItemSelected].GetValue())
            {
                Debug.Log("Has comprado" + shop[ItemSelected].GetName() + " !");
                GameManager.instance.AddCoin(-shop[ItemSelected].GetValue());
            }

            else
            {
                Debug.Log("Aun no tienes el suficiente dinero!");
            }
        }
    }

    public void SelectItem(GameObject A)
    {
        ItemSelected = int.Parse(A.name);
    }
}
