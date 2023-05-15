using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTextController : MonoBehaviour
{
    TMP_Text coinText;
    void Start()
    {
        coinText = transform.Find("Coins").gameObject.GetComponent<TMP_Text>();
    }
    void Update()
    {
        coinText.text = "Coins: " + GameManager.instance.gm_coins.ToString("000");
    }
}
