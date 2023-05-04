using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayerController : MonoBehaviour
{
    public bool ableToMove;
    SpriteRenderer rend;
    Rigidbody2D rb;

    enum PlayerStats { IdleFront, WalkShop, up, down}
    PlayerStats controlStates;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (ableToMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 4;
        }

        float NewScale = GameManager.instance.MapValues(transform.position.y, 0.6f, -4, 0.8f, 1);
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
            controlStates = PlayerStats.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            controlStates = PlayerStats.down;
        }
        else
        {
            controlStates = PlayerStats.IdleFront;
        }
        animator.SetInteger("ControlShop", (int)controlStates);
    }
}
