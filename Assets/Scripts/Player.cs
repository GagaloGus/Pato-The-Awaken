using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 20;
    public float rayDistance = 1.5f;
    private Rigidbody2D rb;
    private Animator animator;
    SpriteRenderer rend;
    public bool Dead;
    public float TotalClicks = 10;
    public float CurrentClicks = 0;
    public bool Trap = false;
    public GameObject Enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Trapped();
    }

    void Movement()
    {
        if (Dead == false)
        {
            if (Input.GetKey(KeyCode.A))
            {

               // animator.SetBool("IsWalking", true); //Hago que cuando presione A, le diga al animator que "IsWalking" es true y que el sprite del jugador se gire.
                rend.flipX = true;
                rb.velocity = new Vector2(-velocity, rb.velocity.y);
            }
            else if (Input.GetKey(KeyCode.D))
            {

                //animator.SetBool("IsWalking", true); //Hago que cuando presione D, le diga al animator que "IsWalking" es true y el sprite del jugador no se gire.
                rend.flipX = false;
                rb.velocity = new Vector2(velocity, rb.velocity.y);
            }
            else
            {
                //animator.SetBool("IsWalking", false);
                //animator.SetBool("IsGrounded", true);
                rb.velocity = new Vector2(rb.velocity.x / 1.12f, rb.velocity.y);

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //AudioManager.instance.PlayAudio(JumpAudio, volumemusic);
                //animator.Play("JumpAnimation"); //Hago que cuando presione la tecla espacio, le diga al animator que "IsWalking" es true.
                rb.AddForce(new Vector2(0, 200));
            }
        }
    }

    void Trapped()
    {
        if (Trap == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CurrentClicks++;
            }
            if (CurrentClicks == TotalClicks)
            {
                GetComponent<Player>().Dead = false;
                Enemy.GetComponent<SpriteRenderer>().color = Color.red;
                Destroy(Enemy.GetComponent<Collider2D>());
            }
            //collision.GetComponent<Animator>().Play("Stun_Animation");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrapEnemy")
        {
            rb.velocity = Vector2.zero;
            Dead = true;
            Trap = true;
        }
        //if (collision.gameObject.tag == "Alert")
        //{
        //    collision.GetComponent<Animator>().Play("Alert_Animation");
        //}
        //if (collision.gameObject.tag == "Camera_Death")
        //{
        //    FindObjectOfType<Camera>().CanMove = false;
        //    Dead = true;
        //    rb.velocity = Vector2.zero;
        //    //collision.GetComponent<Animator>().Play("Death_Animation");
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Alert")
        //{
        //    collision.GetComponent<Animator>().Play("Alert_Iddle_Animation");
        //}
    }

}
