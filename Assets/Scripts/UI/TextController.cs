using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    TMP_Text coinText, levelText;
    // Update is called once per frame
    void Start()
    {
        coinText = transform.Find("Coins").gameObject.GetComponent<TMP_Text>();
        levelText = transform.Find("Level Count").gameObject.GetComponent<TMP_Text>();

        levelText.text = "1 - " + GameManager.instance.gm_level;
    }
    void Update()
    {
        coinText.text = "Coins: " + GameManager.instance.gm_coins.ToString("000");
    }

}
