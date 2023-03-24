using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLogo : MonoBehaviour
{
    public GameObject pauseButton;
    public void StartLogoLeft()
    {
        GameManager.instance.gameStarted = true;
        pauseButton.GetComponent<Animator>().Play("pause_button_enter");
        transform.parent.gameObject.SetActive(false);
    }
}
