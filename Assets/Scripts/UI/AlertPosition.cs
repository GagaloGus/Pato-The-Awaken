using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPosition : MonoBehaviour
{
    GameObject player;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Controller>().gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x < -3 && GameManager.instance.gameStarted)
        {
            animator.Play("Alert_Animation");
        }
        else
        {
            animator.Play("Alert_Iddle_Animation");
        }
    }
}
