using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public InventoryObject[] items;

    public Transform itemContent;
    public GameObject itemButton, itemSelectFrame, InventoryGO;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject obj = Instantiate(itemButton, itemContent); obj.name = i.ToString();
            var itemAmnt = obj.transform.Find("ItemAmount").GetComponent<TMP_Text>();
            var itemDesc = obj.transform.Find("ItemDescript").GetComponent<TMP_Text>();
            var itemSprt = obj.transform.Find("ItemSprite").GetComponent<Image>();

            obj.transform.Find("ItemName").GetComponent<TMP_Text>().text = items[i].itemName;
            itemDesc.text = items[i].itemDescription + " (" + items[i].itemActiveTime + "s)";
            itemSprt.sprite = items[i].itemSprite;
            itemAmnt.text = Random.Range(0,2).ToString();

            if(int.Parse(itemAmnt.text) == 0)
            {
                itemSprt.color = new Color(1, 1, 1, 0.25f);
                Destroy(obj.GetComponent<Button>());
            }

            obj.GetComponent<ItemController>().Item = items[i];

            obj.GetComponent<Button>().onClick.AddListener(() => { ChangeFrameSprite(obj); });
        }

    }

    void ChangeFrameSprite(GameObject o)
    {
        itemSelectFrame.transform.Find("itemSpriteHolder").GetComponent<Image>().sprite = o.transform.Find("ItemSprite").GetComponent<Image>().sprite;
        itemSelectFrame.GetComponent<ItemFrame>().holdingObject = o;
        InventoryGO.SetActive(false);
    }

}
