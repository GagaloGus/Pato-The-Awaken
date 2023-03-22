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
    private void OnTriggerStay2D(Collider2D Collider)
    {
        if (Collider.CompareTag("Player"))
        {
            animator.Play("Alert_Animation");
        }
    }
}
