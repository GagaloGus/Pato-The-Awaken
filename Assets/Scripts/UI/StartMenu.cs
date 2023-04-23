using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    GameObject startMenu, startLogo;
    bool clicked_start;
    // Update is called once per frame
    private void Start()
    {
        clicked_start = false;
        startMenu = transform.Find("Start Logo").gameObject;
        startLogo = FindObjectOfType<StartLogo>().gameObject;
    }
    public void StartLevel()
    {
        if (!clicked_start)
        {
            startLogo.GetComponent<Animator>().Play("startlogo_leave");
            clicked_start = true;
        }
    }
}
