using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public TMP_Text coinText, duckText, levelText;
    // Update is called once per frame
    void Start()
    {
        levelText.text = "1 - " + GameManager.instance.gm_level;
    }
    void Update()
    {
        UpdateCoins();
        UpdateDucks();
    }
    
    public void UpdateCoins()
    {
        coinText.text = "Coins: " + GameManager.instance.gm_coins.ToString("000");
    }

    public void UpdateDucks()
    {
        duckText.text = "Ducks: " + GameManager.instance.gm_ducks;
    }
}
