using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    uint coins, ducks, level = 1;
    public bool gameStarted;
    public float speedLevel;
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
        speedLevel = 15;
    }

    public void ChangeScene(string sceneName, bool nextlevel)
    {
        SceneManager.LoadScene(sceneName);
        ducks = 0;
        gameStarted = false; speedLevel = 0;
        if (nextlevel) { level++; }
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
}
