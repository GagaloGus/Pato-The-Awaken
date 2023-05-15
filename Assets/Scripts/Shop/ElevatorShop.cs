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
    
    public void EnterShop()
    {
        FindObjectOfType<Canvas>().GetComponentInChildren<Animator>().Play("FadeInShop");
    }

    public void OutShop()
    {
        FindObjectOfType<Canvas>().GetComponentInChildren<Animator>().Play("FadeOutShop");
    }
    public void ShopMusic()
    {
        AudioManager.instance.PlayMusic("Shop Music");
    }
    public void ShopMusicStop()
    {
        AudioManager.instance.musicSource.Stop();
    }
}
