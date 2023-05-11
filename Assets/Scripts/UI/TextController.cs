using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public TMP_Text coinText;
    // Update is called once per frame
    void Start()
    {

    }
    void Update()
    {
        UpdateCoins();
    }
    
    public void UpdateCoins()
    {
        coinText.text = "Coins: " + GameManager.instance.gm_coins.ToString("000");
    }

}
