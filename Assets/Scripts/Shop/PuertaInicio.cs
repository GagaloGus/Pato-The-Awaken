using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaInicio : MonoBehaviour
{
    public bool clicked = false;
    public GameObject TKey;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;

        TKey = transform.Find("Key T").gameObject;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && !clicked && !GameManager.instance.gameStarted && TKey.activeInHierarchy)
        {
            animator.enabled = true;
            FindObjectOfType<StartAndDeathMenu>().gameObject.GetComponent<Animator>().Play("EnterShop");
            clicked = true;
        }   
    }
}
