using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Transform itemContent;
    public GameObject itemButton, itemSelectFrame, InventoryGO;

    InventoryObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        items = InventoryManager.instance.items;

        for (int i = 0; i < items.Length; i++)
        {
            //instancia los placeholders de los items
            GameObject obj = Instantiate(itemButton, itemContent); obj.name = i.ToString();

            //encuentra dentro de los placeholders sus diferentes hijos
            var itemAmnt = obj.transform.Find("ItemAmount").GetComponent<TMP_Text>();
            var itemDesc = obj.transform.Find("ItemDescript").GetComponent<TMP_Text>();
            var itemSprt = obj.transform.Find("ItemSprite").GetComponent<Image>();

            //cambia sus valores respecto a los valores del item
            obj.transform.Find("ItemName").GetComponent<TMP_Text>().text = items[i].itemName;
            itemDesc.text = items[i].itemDescription + " (" + items[i].itemActiveTime + "s)";
            itemSprt.sprite = items[i].itemSprite;
            itemAmnt.text = InventoryManager.instance.getItemAmount[i].ToString();

            //si no tiene 0 items de ese tipo se desactiva el boton
            if(InventoryManager.instance.getItemAmount[i] == 0)
            {
                itemSprt.color = new Color(1, 1, 1, 0.25f);
                Destroy(obj.GetComponent<Button>());
            }

            //les da a cada item sus datos para que los almacenen localmente
            obj.GetComponent<ItemController>().Item = items[i];

            //le da a cada boton su onClick correspondiente
            obj.GetComponent<Button>().onClick.AddListener(() => { ChangeFrameSprite(obj); });
        }

    }

    void ChangeFrameSprite(GameObject o)
    {
        //coje el hijo sprite del itemframe y le cambia la imagen a la del item elejido
        itemSelectFrame.transform.Find("itemSpriteHolder").GetComponent<Image>().sprite = o.transform.Find("ItemSprite").GetComponent<Image>().sprite;

        /*if(o.transform.Find("ItemName").GetComponent<TMP_Text>().text == "Invencibility")
        {
            Animator itemAnim = itemSelectFrame.transform.Find("itemSpriteHolder").gameObject.AddComponent<Animator>();
            itemAnim.runtimeAnimatorController = InventoryManager.instance.starAnimator;
        }*/

        //asigna el item al itemframe para que el jugador lo use
        itemSelectFrame.GetComponent<ItemFrame>().holdingObject = o;

        itemSelectFrame.GetComponent<ItemFrame>().amountText.SetActive(true);

        //oculta el inventario
        InventoryGO.SetActive(false);
    }

}
