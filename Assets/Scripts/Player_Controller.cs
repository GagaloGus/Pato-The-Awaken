using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public bool ableToMove, isGrounded, isJumping, isGliding;

    public float jumpPower = 15, jumpTimeCounter;
    public float xPosition;

    Rigidbody2D rb;
    public BoxCollider2D boxCol;
    LayerMask groundLayerMask;

    enum PlayerStates { idle, run, up, down, glide}
    PlayerStates controlStates;

    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); boxCol = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        animator = GetComponent<Animator>();
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

        if (!GameManager.instance.gameStarted) // Cuando el juego est� en el inicio El pato est� con la animaci�n Idle
        {
            controlStates = PlayerStates.idle;
        } 

        if(transform.position.y < -2)
        {
            GameManager.instance.ChangeScene("main", false);
        }

        if (transform.position.x < -9)
        {
            DeathCamera();
        }
    }

    void DeathCamera()
    {
        GameManager.instance.DiedCamera();
        ableToMove = false;
        rb.gravityScale = 0; rb.velocity = Vector2.zero;
        animator.SetInteger("Control", 1);
        animator.Play("DeathNokas");
    }
    float MaintainVelocity()
    {
        float newVel;

        newVel = -transform.position.x / 2 + xPosition;

        return newVel;
    }
    void BoxCasting()
    {
        RaycastHit2D boxcasteo = Physics2D.BoxCast(transform.position + new Vector3(boxCol.offset.x, boxCol.offset.y - 0.25f, 0), 
            boxCol.size / 1.25f, 0, Vector2.down, 0.1f, groundLayerMask);

         isGrounded = boxcasteo.collider;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(boxCol.offset.x, boxCol.offset.y - 0.25f, 0), boxCol.size / 1.25f);
    }
    void Ground()
    {
        rb.gravityScale = 7; rb.drag = 0;
        isGliding = false;
        controlStates = PlayerStates.run; //El pato hace la animaci�n de correr cuando toca el suelo
    }
    void Jump()
    {
        //salta si esta en el suelo y le doy al espacio
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpTimeCounter = 0.21f;
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
        if (Input.GetKeyDown(KeyCode.Space)) { isGliding = !isGliding; }

        if (isGliding) 
        { 
            rb.gravityScale = 2; rb.drag = 3;
            controlStates = PlayerStates.glide; // El pato hace la animaci�n de Planeo
        }
        else 
        { 
            rb.gravityScale = 7; rb.drag = 0;

            if(rb.velocity.y > 0.1)
            {
                controlStates = PlayerStates.up; // El pato hace la animaci�n Up
            }
            else if(rb.velocity.y < -0.1)
            {
                controlStates = PlayerStates.down; // El pato hacia la animaci�n Down
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            GameManager.instance.gm_coins++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("duck"))
        {
            GameManager.instance.gm_ducks++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("End level"))
        {
            GameManager.instance.ChangeScene("main", true);
        }
        if (collision.CompareTag("enemyBonkBox"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            Destroy(collision.transform.parent.gameObject);
            isGliding = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("enemy"))
        {
            StopAllCoroutines();
            StartCoroutine(EnemyStun(collision.gameObject));
            Destroy(collision.gameObject);
        }
    }

    IEnumerator EnemyStun(GameObject enemyGameObj)
    {
        {
            ableToMove = false; isGliding = false;
            animator.SetInteger("Control", 3);
            rb.velocity = Vector2.left*4;
            rb.gravityScale = 0;
            yield return new WaitForSeconds(enemyGameObj.GetComponent<Enemy>().stunTime);
            ableToMove = true;
        }
        
    }
}
