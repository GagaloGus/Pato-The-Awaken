using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Settings : MonoBehaviour
{
    GameObject player;
    public float HeightDif, ymin;

    public bool followPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Controller>().gameObject;
        followPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Clamp(player.transform.position.y - HeightDif, ymin, Mathf.Infinity);
        if (followPlayer)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }
}
