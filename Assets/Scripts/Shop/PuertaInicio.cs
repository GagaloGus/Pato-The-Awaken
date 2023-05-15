using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaInicio : MonoBehaviour
{
    public bool clicked = false;

    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && !clicked && !GameManager.instance.gameStarted)
        {
            animator.enabled = true;
            FindObjectOfType<StartAndDeathMenu>().gameObject.GetComponent<Animator>().Play("EnterShop");
            clicked = true;
        }   
    }
}
