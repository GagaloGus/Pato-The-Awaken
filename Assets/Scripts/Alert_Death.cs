using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_Death : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Alert")
        {
            collision.GetComponent<Animator>().Play("Alert_Animation");
        }

        if (collision.gameObject.tag == "Camera_Death")
        {
            FindObjectOfType<Camera_Movement>().CanMove = false;
            //collision.GetComponent<Animator>().Play("Death_Animation");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Alert")
        {
            collision.GetComponent<Animator>().Play("Alert_Iddle_Animation");
        }
    }

}

