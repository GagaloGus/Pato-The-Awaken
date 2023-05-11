using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    bool ableToMove = true, isGrounded, isJumping, isGliding;

    float jumpPower = 15, jumpTimeCounter;
    public int jumpsAvaliable;
    public float xPosition, yPosition;

    Rigidbody2D rb;
    public BoxCollider2D boxCol;
    LayerMask groundLayerMask;

    enum PlayerStates { idle, run, up, down, glide}
    PlayerStates controlStates;

    enum ActiveBuff { idle, doubleJump, fast, invincible, magnet, balloon, random}
    ActiveBuff currentBuff;

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

        if (!GameManager.instance.gameStarted) // Cuando el juego está en el inicio El pato está con la animación Idle
        {
            controlStates = PlayerStates.idle;
        } 

        if(transform.position.y < -2)
        {
            DeathCamera();
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
                AudioManager.instance.PlaySFX("Jump 2");
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
            AudioManager.instance.PlaySFX("Glide");
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
        }
        if (collision.CompareTag("duck"))
        {
            GameManager.instance.gm_ducks++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("End level"))
        {
            StopCoroutine(nameof(EnemyStun));
            rb.velocity = new Vector2(8, 3); rb.gravityScale = 0; rb.drag = 0;
            ableToMove = false;
            animator.SetInteger("Control", (int)PlayerStates.glide);


            GameManager.instance.CameraEndCutscene();
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
        if (collision.gameObject.CompareTag("enemy"))
        {
            if(currentBuff != ActiveBuff.invincible)
            {
                StopCoroutine(nameof(EnemyStun));
                StartCoroutine(EnemyStun(collision.gameObject));
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

    public IEnumerator UseBuff(InventoryObject inv)
    {
        currentBuff = ActiveBuff.idle;
        float speedMult = 1;

        if(inv != null)
        {
            if (inv.name == "DoubJumpBuff")
            {
                currentBuff = ActiveBuff.doubleJump;
            }
            else if (inv.name == "FasterBuff")
            {
                currentBuff = ActiveBuff.fast; speedMult = 1.5f;
                GameManager.instance.gm_gamespeed *= speedMult;
                xPosition += speedMult;

                StopCoroutine(nameof(SpeedBoostTrail));
                StartCoroutine(SpeedBoostTrail(inv.itemActiveTime));

                AudioManager.instance.PlayMusic("Faster Buff");
            }
            else if (inv.name == "InvenciBuff")
            {
                currentBuff = ActiveBuff.invincible;
                StopCoroutine(nameof(InvencibilityColorChange));
                StartCoroutine(InvencibilityColorChange(inv.itemActiveTime));

                AudioManager.instance.PlayMusic("Invincible");
            }
            else if (inv.name == "Magnet")
            {
                currentBuff = ActiveBuff.magnet;

                StartCoroutine(InvencibilityColorChange(inv.itemActiveTime));
            }
            else if (inv.name == "Balloon")
            {
                currentBuff = ActiveBuff.balloon;
            }

            yield return new WaitForSeconds(inv.itemActiveTime);
            print("back to normnal :(");
            currentBuff = ActiveBuff.idle;
            

            if (inv.name == "FasterBuff")
            {
                GameManager.instance.gm_gamespeed /= speedMult;
                xPosition -= speedMult;
            }

        }
        else
        {
            //Si se quiere usar un buff que no existe se reinicia la escena
            GameManager.instance.ChangeScene("main", false);
        }
    }

    IEnumerator DoubleJumpEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        for (float i = 0; i <= 1; i+= 0.1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(i, 1, i);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator SpeedBoostTrail(float useTime)
    {
        for (int repeat = 0; repeat <= useTime * 6; repeat++)
        {
            GameObject trail = new GameObject();
            SpriteRenderer sprtRend = trail.AddComponent<SpriteRenderer>();
            sprtRend.sprite = GetComponent<SpriteRenderer>().sprite;
            sprtRend.sortingLayerName = "Player Effect";
            trail.transform.position = transform.position;
            trail.transform.parent = FindObjectOfType<Desk_Random>().gameObject.transform;

            //for se repite 6 veces
            for (float i = 0.5f; i >= 0; i-= 0.1f)
            {
                sprtRend.color = new Color(0, 0, 0, i);
                yield return new WaitForSeconds(0.025f);
            }
            
            Destroy(trail);
        }
    }

    IEnumerator InvencibilityColorChange(float useTime)
    {
        for (int repeat = 0; repeat <= useTime; repeat++)
        {
            //for se repite 100 veces
            for (float i = 0.01f; i < 1; i+= 0.01f)
            {
                GetComponent<SpriteRenderer>().color = Color.HSVToRGB(i, 0.82f, 1);
                yield return new WaitForSeconds(0.005f);
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void StartBuffCoroutine(InventoryObject inv)
    {
        StartCoroutine(UseBuff(inv));
        print(inv.name);
    }
}
