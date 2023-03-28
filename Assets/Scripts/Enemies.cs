using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float rayDistance = 2.5f;


    void Start()
    {
        
    }

    void Update()
    {
        
    }


    void OnDrawGizmosSelected() //Asigno al raycast un color y una direccion hacia donde apuntar.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.left * rayDistance);
    }
}
