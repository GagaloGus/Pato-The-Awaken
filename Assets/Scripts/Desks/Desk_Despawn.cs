using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk_Despawn : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -7 - GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            Destroy(gameObject);
        }
    }
}
