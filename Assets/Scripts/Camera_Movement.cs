using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    private Animator animator;
    public int MoveX;
    public bool CanMove;
    public GameObject Player;
    public GameObject AlertPrefab;
    void Start()
    {

    }

    void Update()
    {
        CameraStop();
        Death_Alert();
    }
    private void CameraStop()
    {
        if (CanMove == true)
        {
            transform.Translate(new Vector2(MoveX, 0) * Time.deltaTime);
        }
    }
    private void Death_Alert()
    {
        if(Player.transform.position.x < transform.position.x - 8)
        {
            animator.Play("Alert_Animation");
        }
    }
}
