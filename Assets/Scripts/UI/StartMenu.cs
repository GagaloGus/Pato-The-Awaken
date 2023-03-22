using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject pauseButton, startMenu;
    public static bool gameStarted;
    void Start()
    {
        gameStarted = false;
        pauseButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        startMenu.SetActive(!gameStarted);
        print(gameStarted + " " + PauseMenu.isPaused);
    }

    public void StartLevel()
    {
        gameStarted = true;
        pauseButton.SetActive(true);
    }
}
