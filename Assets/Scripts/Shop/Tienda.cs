using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    public int ItemSelected = 0;
    public GameObject ItemShop;
    InventoryObject[] shop;
    public GameObject[] Buttons;
    void Start()
    {
        shop = InventoryManager.instance.items;

        ItemShop = transform.Find("DatosObject").gameObject;
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
            if (GameManager.instance.gm_coins >= shop[ItemSelected].ItemCost)
            {
                Debug.Log("Has comprado" + shop[ItemSelected].itemName + " !");
                GameManager.instance.AddCoin(-shop[ItemSelected].ItemCost);
                InventoryManager.instance.ChangeItemAmount(shop[ItemSelected].order, 1);
                ItemShop.transform.Find("CantidadObject").gameObject.GetComponent<TMP_Text>().text = InventoryManager.instance.getItemAmount[ItemSelected].ToString();
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
        GameObject NameItem = ItemShop.transform.Find("NombreObject").gameObject;
        GameObject SpriteItem = ItemShop.transform.Find("SpriteObject").gameObject;
        GameObject CostItem = ItemShop.transform.Find("ValorObject").gameObject;
        GameObject CantObject = ItemShop.transform.Find("CantidadObject").gameObject;

        NameItem.GetComponent<TMP_Text>().text = shop[ItemSelected].itemName;
        CostItem.GetComponent<TMP_Text>().text = shop[ItemSelected].ItemCost.ToString() + " Coins";
        SpriteItem.GetComponent<Image>().sprite = shop[ItemSelected].itemSprite;
        CantObject.GetComponent<TMP_Text>().text = InventoryManager.instance.getItemAmount[ItemSelected].ToString();
    }
}
