using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BancoPato : MonoBehaviour
{
    Animator animator;
    public GameObject patoshoppo;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("clicked", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { animator.SetBool("clicked", true); }
    }

    public void RestartCharacter()
    {
        patoshoppo.SetActive(true); patoshoppo.transform.position = new Vector2(-4.28f, -2.06f); patoshoppo.GetComponent<ShopPlayerController>().ableToMove = true;
        animator.SetBool("clicked", false);
        gameObject.SetActive(false);
    }
}
