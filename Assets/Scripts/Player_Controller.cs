using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public bool ableToMove, isGrounded, isJumping;

    public float jumpPower = 15, jumpTimeCounter;

    Rigidbody2D rb;
    public BoxCollider2D boxCol;
    LayerMask groundLayerMask;

    GameObject maincamera;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); boxCol = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        BoxCasting();

        if (ableToMove && !PauseMenu.isPaused && GameManager.instance.gameStarted)
        {
            rb.velocity = new Vector2(MaintainVelocity(), rb.velocity.y);
            Jump();
        }

        if(transform.position.y < -2)
        {
            GameManager.instance.ChangeScene("main", false);
        }
    }
    float MaintainVelocity()
    {
        float newVel;

        newVel = -transform.position.x / 2;

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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Sebastian_Controller>())
        {
            
        }
    }

    void EnemyStun()
    {
        
    }
}
