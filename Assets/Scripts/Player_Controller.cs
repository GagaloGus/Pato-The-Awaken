using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public bool ableToMove, isGrounded, isJumping;

    public float speed, 
        jumpPower = 15, jumpTimeCounter;

    Rigidbody2D rb;
    public BoxCollider2D boxCol;

    public GameObject camera_GO;
    LayerMask groundLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); boxCol = GetComponent<BoxCollider2D>();

        camera_GO = GameObject.FindGameObjectWithTag("MainCamera");

        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        BoxCasting();

        if (ableToMove)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            Jump();
        }

        camera_GO.transform.position = new Vector3(transform.position.x, camera_GO.transform.position.y, camera_GO.transform.position.z);
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
}
