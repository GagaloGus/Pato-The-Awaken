using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs_Player : MonoBehaviour
{
    protected enum ActiveBuff { idle, doubleJump, fast, invincible, magnet, balloon, random }
    protected ActiveBuff currentBuff;

    protected bool ableToMove = true;

    public float xPosition;

    protected GameObject Magnet, Balloons;

    public IEnumerator UseBuff(InventoryObject inv)
    {
        currentBuff = ActiveBuff.idle;
        float OGgamespeed = GameManager.instance.gm_gamespeed, OGXposition = xPosition;

        if (inv != null)
        {
            if (inv.name == "DoubJumpBuff")
            {
                currentBuff = ActiveBuff.doubleJump;
                AudioManager.instance.PlaySFX("Use boioing");
            }
            else if (inv.name == "FasterBuff")
            {
                currentBuff = ActiveBuff.fast;
                GameManager.instance.gm_gamespeed *= 1.5f;
                xPosition += 1.5f;
                StartCoroutine(SpeedBoostTrail());

                AudioManager.instance.PlaySFX("Use fast");
                AudioManager.instance.PlayMusic("Fast Buff");
            }
            else if (inv.name == "InvenciBuff")
            {
                currentBuff = ActiveBuff.invincible;
                StartCoroutine(InvencibilityColorChange());
                GameManager.instance.gm_gamespeed *= 1.2f;
                xPosition += 1.2f;

                AudioManager.instance.PlaySFX("Use star");
                AudioManager.instance.PlayMusic("Invincible");
            }
            else if (inv.name == "Magnet")
            {
                currentBuff = ActiveBuff.magnet;
                StartCoroutine(MagnetCircleCast());

                AudioManager.instance.PlaySFX("Magnet");
            }
            else if (inv.name == "Balloon")
            {
                currentBuff = ActiveBuff.balloon;
                StartCoroutine(BalloonHold());

                AudioManager.instance.PlaySFX("Balloon");
            }
            else if (inv.name == "RandomItem")
            {
                InventoryObject rnd = InventoryManager.instance.items[Random.Range(0, InventoryManager.instance.items.Length - 2)];
                StartBuffCoroutine(rnd);
                yield break;
            }

            yield return new WaitForSeconds(inv.itemActiveTime);
            print("back to normnal :(");
            currentBuff = ActiveBuff.idle;

            GameManager.instance.gm_gamespeed = OGgamespeed;
            xPosition = OGXposition;

        }
        else
        {
            //Si se quiere usar un buff que no existe se reinicia la escena
            GameManager.instance.ChangeScene("main", false);
        }
    }

    protected IEnumerator DoubleJumpEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        for (float i = 0; i <= 1; i += 0.1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(i, 1, i);
            yield return new WaitForSeconds(0.05f);
        }
    }

    protected IEnumerator SpeedBoostTrail()
    {
        for (int repeat = 0; repeat < Mathf.Infinity; repeat++)
        {
            GameObject trail = new GameObject();
            SpriteRenderer sprtRend = trail.AddComponent<SpriteRenderer>();
            sprtRend.sprite = GetComponent<SpriteRenderer>().sprite;
            sprtRend.sortingLayerName = "Player Effect";
            trail.transform.position = transform.position;
            trail.transform.parent = FindObjectOfType<Desk_Random>().gameObject.transform;

            //for se repite 6 veces
            for (float i = 0.5f; i >= 0; i -= 0.1f)
            {
                sprtRend.color = new Color(0, 0, 0, i);
                yield return new WaitForSeconds(Time.deltaTime*2);   
            }
            Destroy(trail);

            if (currentBuff == ActiveBuff.idle) { yield break; }
        }
    }

    protected IEnumerator InvencibilityColorChange()
    {
        for (int repeat = 0; repeat < Mathf.Infinity; repeat++)
        {
            //for se repite 100 veces
            for (float i = 0.01f; i < 1; i += 0.01f)
            {
                GetComponent<SpriteRenderer>().color = Color.HSVToRGB(i, 0.82f, 0.9f);
                yield return new WaitForSeconds(Time.deltaTime);

                if (currentBuff == ActiveBuff.idle) { GetComponent<SpriteRenderer>().color = Color.white; yield break; }
            }
        }

    }

    protected IEnumerator MagnetCircleCast()
    {
        Magnet.SetActive(true);
        for (int repeat = 0; repeat < Mathf.Infinity; repeat++)
        {
            RaycastHit2D[] coinsInRange = Physics2D.CircleCastAll(transform.position, 9, Vector2.up);

            foreach(RaycastHit2D obj in coinsInRange)
            {
                GameObject coin = obj.transform.gameObject;
                if (coin.CompareTag("coin")) { coin.transform.position = Vector2.MoveTowards(coin.transform.position, transform.position, GameManager.instance.gm_gamespeed/50); }
            }

            yield return new WaitForSeconds(Time.deltaTime);

            if (currentBuff == ActiveBuff.idle) { Magnet.SetActive(false); yield break; }
        }
    }

    protected IEnumerator BalloonHold()
    {
        Balloons.SetActive(true); 
        GetComponent<BoxCollider2D>().enabled = false; 
        ableToMove = false;
        GetComponent<Rigidbody2D>().gravityScale = 0; GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        GetComponent<Animator>().SetInteger("Control", 3);

        Vector2 destination = new Vector2(xPosition, 14);

        for (int repeat = 0; repeat < Mathf.Infinity; repeat++)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, Vector2.Distance(transform.position, destination)/50 );
            yield return new WaitForSeconds(Time.deltaTime);

            if (currentBuff == ActiveBuff.idle) { 
                Balloons.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = true;
                ableToMove = true;

                yield break; 
            }
        }
    }

    public void StartBuffCoroutine(InventoryObject inv)
    {
        StartCoroutine(UseBuff(inv));
        print(inv.name);
    }
}
