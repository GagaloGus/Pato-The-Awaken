using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : Buffs_Player
{
    bool isGrounded, isJumping, isGliding;

    float jumpPower = 15, jumpTimeCounter;
    public int jumpsAvaliable;

    Rigidbody2D rb;
    BoxCollider2D boxCol;
    LayerMask groundLayerMask;

    enum PlayerStates { idle, run, up, down, glide}
    PlayerStates controlStates;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); boxCol = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        animator = GetComponent<Animator>();

        Magnet = transform.Find("Magnet").gameObject;
        Magnet.SetActive(false);

        Balloons = transform.Find("Balloon").gameObject;
        Balloons.SetActive(false);
    }
   
    void Update()
    {
        BoxCasting();

        if (ableToMove && !PauseMenu.isPaused && GameManager.instance.gameStarted)
        {
            rb.velocity = new Vector2(MaintainVelocity(), rb.velocity.y);
            if (isGrounded) { Ground(); }
            else { Air(); }
            Jump();
            animator.SetInteger("Control", (int)controlStates);
        }

        if (!GameManager.instance.gameStarted) // Cuando el juego está en el inicio El pato está con la animación Idle
        {
            controlStates = PlayerStates.idle;
        } 

        if(transform.position.y < -2 || transform.position.x < -9)
        {
            GameManager.instance.DeathÑokas();
        }
    }

    float MaintainVelocity()
    {
        return -transform.position.x / 2 + xPosition;
    }
    void BoxCasting()
    {
        RaycastHit2D boxcasteo = Physics2D.BoxCast(transform.position + new Vector3(boxCol.offset.x, boxCol.offset.y - 0.25f, 0), 
            boxCol.size / 1.25f, 0, Vector2.down, 0.1f, groundLayerMask);

         isGrounded = boxcasteo.collider;
    }

    void Ground()
    {
        rb.gravityScale = 7; rb.drag = 0;
        isGliding = false;
        controlStates = PlayerStates.run; //El pato hace la animación de correr cuando toca el suelo

        if (currentBuff == ActiveBuff.doubleJump) { jumpsAvaliable = 1; } else { jumpsAvaliable = 0; }
    }
    void Jump()
    {
        //salta si esta en el suelo y le doy al espacio
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpsAvaliable > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpTimeCounter = 0.21f;

            if (!isGrounded && currentBuff == ActiveBuff.doubleJump)
            {
                StopCoroutine(nameof(DoubleJumpEffect));
                StartCoroutine(DoubleJumpEffect());
                AudioManager.instance.PlaySFX("Double Jump");
            }
            else
            {
                AudioManager.instance.PlaySFX("Jump");
            }
            jumpsAvaliable--;
            
        }

        //permite que salte mas si sigo presionando espacio
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpTimeCounter -= Time.deltaTime;
            }
            else { isJumping = false; }
        }

        //si suelto el espacio no estoy saltando
        if (Input.GetKeyUp(KeyCode.Space)) { isJumping = false; }

    }
    void Air()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsAvaliable == 0) { isGliding = !isGliding; }

        if (isGliding) 
        {
            rb.gravityScale = 2; rb.drag = 3;
            controlStates = PlayerStates.glide; // El pato hace la animación de Planeo
        }
        else 
        { 
            rb.gravityScale = 7; rb.drag = 0;

            if(rb.velocity.y > 0.1)
            {
                controlStates = PlayerStates.up; // El pato hace la animación Up
            }
            else if(rb.velocity.y < -0.1)
            {
                controlStates = PlayerStates.down; // El pato hacia la animación Down
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            GameManager.instance.gm_coins++;
            Destroy(collision.gameObject);

            AudioManager.instance.PlaySFX("Coin");
        }
        if (collision.CompareTag("End level"))
        {
            StopCoroutine(nameof(EnemyStun));
            rb.velocity = new Vector2(8, 3); rb.gravityScale = 0; rb.drag = 0;
            ableToMove = false;
            animator.SetInteger("Control", (int)PlayerStates.glide);


            GameManager.instance.LevelComplete();
        }
        if (collision.CompareTag("enemyBonkBox"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            Destroy(collision.transform.parent.gameObject);
            isGliding = false;

            AudioManager.instance.PlaySFX("Stun");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            if(currentBuff != ActiveBuff.invincible)
            {
                StopCoroutine(nameof(EnemyStun));
                StartCoroutine(EnemyStun(collision.gameObject));

                AudioManager.instance.PlaySFX("Stun");
            }
            else
            {
                AudioManager.instance.PlaySFX("Invincible kill");
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator EnemyStun(GameObject enemyGameObj)
    {
        ableToMove = false; isGliding = false;
        animator.SetInteger("Control", 3);
        rb.velocity = Vector2.left*4;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(enemyGameObj.GetComponent<Enemy>().stunTime);
        ableToMove = true;
    }


}
