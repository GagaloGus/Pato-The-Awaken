using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayerController : MonoBehaviour
{
    public bool InShop, ableToMove, InElevator, InBanco;
    SpriteRenderer rend;
    Rigidbody2D rb;

    GameObject bancoPato;

    enum PlayerStats { IdleFront, WalkShop, WalkFront, WalkUpFront, Elevetor}
    PlayerStats controlStates;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        ableToMove = false;

        bancoPato = FindObjectOfType<BancoPato>().gameObject;
        bancoPato.GetComponent<BancoPato>().patoshoppo = this.gameObject;
        bancoPato.SetActive(false);
    }

    void Update()
    {
        if (ableToMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 4;
        }

        float NewScale = GameManager.instance.MapValues(transform.position.y - transform.parent.transform.position.y, 0.6f, -4, 0.7f, 1);
        transform.localScale = Vector3.one * NewScale;

        AnimationControl();
        GameObject shopcanvas = FindObjectOfType<Canvas>().gameObject;

        if (Input.GetKeyDown(KeyCode.E) && (InShop || InElevator || InBanco))
        {
            ableToMove = false;
            rb.velocity = Vector2.zero;

            if (InShop)
            {
                shopcanvas.transform.Find("ShopUI").gameObject.SetActive(true);
            }
            else if (InElevator)
            {
                transform.parent.gameObject.GetComponent<Animator>().Play("ElevatorUp");
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (InBanco)
            {
                bancoPato.SetActive(true); bancoPato.GetComponent<Animator>().Play("Sentao");
                shopcanvas.transform.Find("Key Sign Banco").gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        shopcanvas.transform.Find("Key Sign Tienda").gameObject.SetActive(InShop);
        shopcanvas.transform.Find("Key Sign Elevator").gameObject.SetActive(InElevator);
        shopcanvas.transform.Find("Key Sign Banco").gameObject.SetActive(InBanco);
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shop"))
        {
            InShop = true;
        }
        if (collision.CompareTag("Elevator"))
        {
            InElevator = true;
            print("Estoy au ascensor");
        }
        if (collision.CompareTag("Banco"))
        {
            InBanco = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shop"))
        {
            InShop = false;
        }
        if (collision.CompareTag("Elevator"))
        {
            InElevator = false;
        }
        if (collision.CompareTag("Banco"))
        {
            InBanco = false;
        }
    }
    public void Move(bool active)
    {
        ableToMove = active;
    }

    public void PlayWalkSFX()
    {
        AudioManager.instance.PlaySFX("Player walk shop");
    }

    void AnimationControl()
    {
        if (Input.GetKey(KeyCode.A))
        {
            controlStates = PlayerStats.WalkShop;
            rend.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            controlStates = PlayerStats.WalkShop;
            rend.flipX = false;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            controlStates = PlayerStats.WalkUpFront;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            controlStates = PlayerStats.WalkFront;
        }
        else
        {
            controlStates = PlayerStats.IdleFront;
        }

        animator.SetInteger("ControlShop", (int)controlStates);
    }
}
