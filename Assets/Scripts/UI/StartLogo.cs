using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLogo : MonoBehaviour
{
    public GameObject pauseButton;
    public void StartLogoLeft()
    {
        GameManager.instance.GameStarted();
        pauseButton.GetComponent<Animator>().Play("pause_button_enter");
        transform.parent.gameObject.SetActive(false);
    }
    public void IntroMusicStop()
    {
        AudioManager.instance.musicSource.Stop();
    }
    public void LevelMusic()
    {
        AudioManager.instance.PlayMusic("Level Theme");
    }
    public void LevelMusicStop()
    {
        AudioManager.instance.musicSource.Stop();
    }
}
