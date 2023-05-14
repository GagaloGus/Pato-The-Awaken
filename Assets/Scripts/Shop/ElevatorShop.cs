using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorShop : MonoBehaviour
{
    public void EnableColider()
    {
        FindObjectOfType<ShopPlayerController>().GetComponent<Collider2D>().enabled = true;
        FindObjectOfType<ShopPlayerController>().GetComponent<ShopPlayerController>().ableToMove = true;
    }
}
