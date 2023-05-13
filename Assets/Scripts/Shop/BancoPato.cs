using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BancoPato : MonoBehaviour
{
    Animator animator;
    GameObject patoshoppo;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("clicked", false);

        patoshoppo = FindObjectOfType<ShopPlayerController>().gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { animator.SetBool("clicked", true); }
    }

    public void RestartCharacter()
    {
        patoshoppo.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        animator.SetBool("clicked", false);
        animator.Play("idle");
    }
}
