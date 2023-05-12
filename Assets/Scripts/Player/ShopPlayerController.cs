using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayerController : MonoBehaviour
{
    public bool InShop, ableToMove, InElevator;
    SpriteRenderer rend;
    Rigidbody2D rb;

    enum PlayerStats { IdleFront, WalkShop, WalkFront, WalkUpFront, Elevetor}
    PlayerStats controlStates;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        ableToMove = false;
    }

    void Update()
    {
        if (ableToMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 4;
        }

        float NewScale = GameManager.instance.MapValues(transform.position.y - transform.parent.transform.position.y, 0.6f, -4, 0.7f, 1);
        transform.localScale = Vector3.one * NewScale;

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

        if (InShop == true && Input.GetKey(KeyCode.E))
        {
            ableToMove = false;
            FindObjectOfType<ShopUI>().Shop.SetActive(true);
        }
        FindObjectOfType<ShopUI>().KeySign.SetActive(InShop);
        if (InElevator == true && Input.GetKey(KeyCode.E))
        {
            ableToMove = false;
            transform.parent.gameObject.GetComponent<Animator>().Play("ElevatorUp");
            GetComponent<BoxCollider2D>().enabled = false;
            rb.velocity = Vector2.zero;
        }
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
    }
    public void Move(bool active)
    {
        ableToMove = active;
    }
}
