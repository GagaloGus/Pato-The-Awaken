using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemFrame : MonoBehaviour
{
    public GameObject holdingObject ,Inventory;
    GameObject cooldownText;

    private void Start()
    {
        cooldownText = transform.Find("Cooldown").gameObject;
        cooldownText.SetActive(false);
    }

    private void Update()
    {
        if (!GameManager.instance.gameStarted && Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!Inventory.activeInHierarchy);
        }
    }

    public void CheckIfGameStarted()
    {
        if (!GameManager.instance.gameStarted)
        {
            Inventory.SetActive(true);
        }
        else
        {
            if(holdingObject != null) 
            { 
                holdingObject.GetComponent<ItemController>().Item.Use();
                StartCoroutine(ItemUseCooldown(holdingObject.GetComponent<ItemController>().Item.itemActiveTime));
            }
            else { print("not holding any object!"); }
        }
    }

    IEnumerator ItemUseCooldown(float useTime)
    {
        GetComponent<Button>().enabled = false;
        transform.Find("itemSpriteHolder").GetComponent<Image>().color = new Color(0, 0, 0, 0.25f);

        cooldownText.SetActive(true);
        for (float i = useTime + 0.2f; i > 0; i-= 0.1f)
        {
            cooldownText.GetComponent<TMP_Text>().text = (Mathf.Round(i * 100)/100).ToString();
            yield return new WaitForSeconds(0.1f);
        }

        cooldownText.SetActive(false);
        GetComponent<Button>().enabled = true;
        transform.Find("itemSpriteHolder").GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
}
