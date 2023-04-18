using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Controller : Enemy
{
    public BoxCollider2D boxCol;
    public float EndPos;

    // Start is called before the first frame update
    void Start()
    {
        EndPos = Random.Range(transform.position.y-4, RayCasting() + 9);
        //transform.position = new Vector2(transform.position.x, EndPos);
        /*if(Physics2D.Raycast(transform.position, FindObjectOfType<Player_Controller>().gameObject.transform.position - transform.position).distance < 1)
        {
            StartCoroutine(MoveToPosition(transform.position.y, EndPos));
        }*/
    }
    
    IEnumerator MoveToPosition(float firstYPos,float YPos)
    {
        for(float y = firstYPos; y > YPos; y-= 0.1f)
        {
            transform.position = new Vector2(transform.position.x, y);
            yield return new WaitForSeconds(0.01f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("spider collider")) { StartCoroutine(MoveToPosition(transform.position.y, EndPos)); }
    }
    float RayCasting()
    {
        RaycastHit2D rayocast = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
        return -rayocast.distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * EndPos);
        Gizmos.color = Color.blue;
        //Gizmos.DrawRay(transform.position, FindObjectOfType<Player_Controller>().gameObject.transform.position - transform.position);
    }
}
