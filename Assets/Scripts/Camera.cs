using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public int MoveX;
    public bool CanMove;
    void Start()
    {

    }

    void Update()
    {
        CameraStop();
    }
    private void CameraStop()
    {
        if (CanMove == true)
        {
            transform.Translate(new Vector2(MoveX, 0) * Time.deltaTime);
        }
    }
}
