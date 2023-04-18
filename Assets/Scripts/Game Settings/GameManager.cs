using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    uint coins, ducks, level = 1;
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

    public void DiedCamera()
    {
        speedLevel = 0;
        Camera.main.GetComponent<Animator>().Play("Camera_Death");
        gameStarted = false;
    }

    public void ChangeScene(string sceneName, bool nextlevel)
    {
        SceneManager.LoadScene(sceneName);
        ducks = 0;
        gameStarted = false; speedLevel = 0;
        if (nextlevel) { level++; }
    }

    IEnumerator FadeOut()
    {
        for(float i = 0; i <= 1; i += 0.01f)
        {
            
        }
    }
    public uint gm_coins
    {
        get { return coins; }
        set { coins = value; }
    }
    public uint gm_ducks
    {
        get { return ducks; }
        set { ducks = value; }
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
}
