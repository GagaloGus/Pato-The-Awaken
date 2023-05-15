using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAndDeathMenu : MonoBehaviour
{
    GameObject startLogo, player, fadeIn;
    bool clicked_start;
    // Update is called once per frame
    private void Start()
    {
        clicked_start = false;
        startLogo = FindObjectOfType<StartLogo>().gameObject;
        player = FindObjectOfType<Player_Controller>().gameObject;
        fadeIn = transform.Find("Fade Screen").gameObject;

        fadeIn.transform.Find("Continue").GetComponent<Button>().onClick.AddListener(() => { NextLevel(); }) ;
        fadeIn.transform.Find("Restart").GetComponent<Button>().onClick.AddListener(() => { RestartLevel(); });
        fadeIn.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { ExitGame(); });


        fadeIn.SetActive(false);
    }

    private void Update()
    {
        if(player.transform.position.x < -9)
        {
            fadeIn.SetActive(true);
            FindObjectOfType<StartAndDeathMenu>().gameObject.GetComponent<Animator>().Play("DeathScreen");
        }
    }

    public void DeathSound()
    {
        AudioManager.instance.PlaySFX("Dead Player");
    }
    public void GameOverSound()
    {
        AudioManager.instance.PlaySFX("Game Over sfx");
    }
    public void IntroMusic()
    {
        AudioManager.instance.PlayMusic("Start Theme");
    }

    public void CompletMusic()
    {
        AudioManager.instance.PlayMusic("Final Theme");
    }
    public void FinalMusicStop()
    {
        AudioManager.instance.musicSource.Stop();
    }
    public void LevelMusicStop()
    {
        AudioManager.instance.musicSource.Stop();
    }

    public void StartLevel()
    {
        if (!clicked_start)
        {
            AudioManager.instance.PlaySFX("Button press");
            startLogo.GetComponent<Animator>().Play("startlogo_leave");
            clicked_start = true;
        }
    }

    public void NextLevel()
    {
        GameManager.instance.ChangeScene("main", true);
    }

    public void RestartLevel()
    {
        AudioManager.instance.PlaySFX("Button press");
        GameManager.instance.ChangeScene("main", false);
    }

    public void EnterShop()
    {
        GameManager.instance.ChangeScene("Shop", false);
    }

    public void ExitGame() { Application.Quit(); }
}
