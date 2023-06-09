using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.gameStarted)
        {
            if (isPaused) { Resume(); }
            else { Pause(); }
        }
    }

    public void Pause()
    {
        AudioManager.instance.PlaySFX("Button press");
        if (GameManager.instance.gameStarted)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }
    public void Resume()
    {
        AudioManager.instance.PlaySFX("Button press");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Restart()
    {
        AudioManager.instance.PlaySFX("Button press");
        Time.timeScale = 1;
        GameManager.instance.ChangeScene("Main", false);
    }

}
