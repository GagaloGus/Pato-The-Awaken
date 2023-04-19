using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sebastian_Controller : Enemy
{
    int xMoveDirection;
    float sizeXRatio;
    BoxCollider2D boxCol2d;

    LayerMask groundLayerMask;
    //ignorar los otros enemigos
    void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandom(0.8f);

        //randomiza la direccion al que va
        do { xMoveDirection = Random.Range(-1, 2); } while (xMoveDirection == 0);

        boxCol2d = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        //mantiene la escala del bicho para q no se deforme
        sizeXRatio = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 17 && GameManager.instance.gameStarted)
        {
            //se da la vuelta
            transform.localScale = new Vector2(xMoveDirection * -1 * sizeXRatio, transform.localScale.y);
            //emite un raycast hacia la direccion a la que mira
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0), (boxCol2d.size.x / 2 + 0.1f) * sizeXRatio, groundLayerMask.value);

            //si encuentra el suelo se da la vuelta
            if (hit.collider != null)
            {
                xMoveDirection *= -1;
            }
        }
    }
}

