using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    uint coins, ducks, level = 1;
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
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ducks = 0;
    }
    public void NextLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ducks = 0; level++;
    }

    public uint gm_coins
    {
        get { return coins; }
        //suma los valores directamente al score
        set { coins = value; }
    }
    public uint gm_ducks
    {
        get { return ducks; }
        //suma los valores directamente al score
        set { ducks = value; }
    }
    public uint gm_level
    {
        get { return level; }
        //suma los valores directamente al score
        set { level = value; }
    }
}
