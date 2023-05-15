using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    uint coins, level = 1;
    public bool gameStarted;
    float speedLevel;

    void Awake()
    {
        if (!instance) //comprueba que instance no tenga informacion
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //si tiene info, ya existe un GM
        {
            Destroy(gameObject);
        }

        gameStarted = false;
        speedLevel = 0;

    }
    public void GameStarted()
    {
        gameStarted = true;
        speedLevel = 13;
    }

    public void Death—okas()
    {
        FindObjectOfType<StartAndDeathMenu>().gameObject.GetComponent<Animator>().Play("Deathscreen");
        speedLevel = 0; gameStarted = false;
    }

    public void LevelComplete()
    {
        FindObjectOfType<StartAndDeathMenu>().gameObject.GetComponent<Animator>().Play("Complet");
        speedLevel = 0; 
        Camera.main.GetComponent<Camera_Settings>().followPlayer = false;
    }

    public void ChangeScene(string sceneName, bool nextlevel)
    {
        SceneManager.LoadScene(sceneName);
        gameStarted = false; speedLevel = 0;
        if (nextlevel) { level++; }
    }

    /*public void ExitShopScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        gameStarted = false; speedLevel = 0;

        //coje el hijo sprite del itemframe y le cambia la imagen a la del item elejido
        FindObjectOfType<ItemFrame>().gameObject.transform.Find("itemSpriteHolder").GetComponent<UnityEngine.UI.Image>().sprite = heldObject.itemSprite;

        //asigna el item al itemframe para que el jugador lo use
        FindObjectOfType<ItemFrame>().GetComponent<ItemFrame>().holdingObject = heldObject;

        FindObjectOfType<Desk_Random>().gameObject.transform.position = new Vector2(middleDistance, FindObjectOfType<Desk_Random>().gameObject.transform.position.y);

        GameObject player = FindObjectOfType<Player_Controller>().gameObject;
        player.GetComponent<Player_Controller>().player_abletomove = false;
        player.GetComponent<Animator>().SetInteger("Control", 5);

        FindObjectOfType<StartAndDeathMenu>().gameObject.GetComponent<Animator>().Play("Aftershop");
    }*/

    public void AddCoin(int value)
    {
        coins += (uint)value;
    }
    public uint GetScore()
    {
        return coins;
    }
    public uint gm_coins
    {
        get { return coins; }
        set { coins = value; }
    }
    public uint gm_level
    {
        get { return level; }
        set { level = value; }
    }

    public float gm_gamespeed
    {
        get { return speedLevel; }
        set { speedLevel = value; }
    }

    public float MapValues(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }
}
