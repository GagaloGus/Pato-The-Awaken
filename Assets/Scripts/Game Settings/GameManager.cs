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

    public void CameraEndCutscene()
    {
        speedLevel = 0;
        Camera.main.GetComponent<Camera_Settings>().followPlayer = false;
        Camera.main.GetComponent<Animator>().Play("Camera_EndLevel");
    }

    public void DiedCamera()
    {
        speedLevel = 0;
        Camera.main.GetComponent<Animator>().Play("Camera_DeathFade");
        gameStarted = false;
    }

    public void ChangeScene(string sceneName, bool nextlevel)
    {
        SceneManager.LoadScene(sceneName);
        ducks = 0;
        gameStarted = false; speedLevel = 0;
        if (nextlevel) { level++; }
    }

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

    public float MapValues(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }
}
