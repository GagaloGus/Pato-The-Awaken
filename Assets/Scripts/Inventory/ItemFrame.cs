using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemFrame : MonoBehaviour
{
    public GameObject Inventory, amountText;
    GameObject cooldownText;

    public InventoryObject holdingObject;

    int itemOrderNum;

    private void Start()
    {
        cooldownText = transform.Find("Cooldown").gameObject;
        amountText = transform.Find("Amount").gameObject;
        cooldownText.SetActive(false);
        amountText.SetActive(false);
    }

    private void Update()
    {
        //si le da a la I mientras el juego no ha empezado, abre el inventario
        if (!GameManager.instance.gameStarted && Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!Inventory.activeInHierarchy);
        }

        itemOrderNum = holdingObject.order;

        //cambia el texto de los usos del objeto
        amountText.GetComponent<TMP_Text>().text = InventoryManager.instance.getItemAmount[itemOrderNum].ToString();
    }

    //onclick del itemframe
    public void CheckIfGameStarted()
    {
        if (!GameManager.instance.gameStarted)
        {
            Inventory.SetActive(true);
        }
        else
        {
            //si se ha seleccionado un item
            if(holdingObject != null) 
            { 
                //usa el item que tiene
                holdingObject.Use();

                //activa el cooldown
                StartCoroutine(ItemUseCooldown(holdingObject.itemActiveTime));

                //reduce la cantidad de items por 1
                InventoryManager.instance.ChangeItemAmount(itemOrderNum, -1);
            }
            else { print("not holding any object!"); }
        }
    }

    IEnumerator ItemUseCooldown(float useTime)
    {
        //desactiva el boton
        GetComponent<Button>().enabled = false;
        transform.Find("itemSpriteHolder").GetComponent<Image>().color = new Color(0, 0, 0, 0.25f);

        //activa el cooldown respecto a la duracion del item
        cooldownText.SetActive(true);
        for (float i = useTime + 0.2f; i > 0; i-= 0.1f)
        {
            cooldownText.GetComponent<TMP_Text>().text = (Mathf.Round(i * 100)/100).ToString();
            yield return new WaitForSeconds(0.1f);
        }

        //reactiva el boton si tiene mas objetos

        if (InventoryManager.instance.getItemAmount[itemOrderNum] > 0)
        {
            GetComponent<Button>().enabled = true;
            transform.Find("itemSpriteHolder").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        cooldownText.SetActive(false);
    }
}
