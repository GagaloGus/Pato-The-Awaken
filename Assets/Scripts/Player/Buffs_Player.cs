using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs_Player : MonoBehaviour
{
    protected enum ActiveBuff { idle, doubleJump, fast, invincible, magnet, balloon, random }
    protected ActiveBuff currentBuff;

    public float xPosition;

    public GameObject Magnet;

    public IEnumerator UseBuff(InventoryObject inv)
    {
        currentBuff = ActiveBuff.idle;
        float speedMult = 1;
        float OGgamespeed = GameManager.instance.gm_gamespeed, OGXposition = xPosition;

        if (inv != null)
        {
            if (inv.name == "DoubJumpBuff")
            {
                currentBuff = ActiveBuff.doubleJump;
            }
            else if (inv.name == "FasterBuff")
            {
                currentBuff = ActiveBuff.fast; speedMult = 1.5f;
                GameManager.instance.gm_gamespeed *= speedMult;
                xPosition += speedMult;

                StopCoroutine(nameof(SpeedBoostTrail));
                StartCoroutine(SpeedBoostTrail(inv.itemActiveTime));

                AudioManager.instance.PlayMusic("Fast Buff");
            }
            else if (inv.name == "InvenciBuff")
            {
                currentBuff = ActiveBuff.invincible;
                StopCoroutine(nameof(InvencibilityColorChange));
                StartCoroutine(InvencibilityColorChange(inv.itemActiveTime));

                AudioManager.instance.PlayMusic("Invincible");
            }
            else if (inv.name == "Magnet")
            {
                currentBuff = ActiveBuff.magnet;
                StopCoroutine(nameof(MagnetCircleCast));
                StartCoroutine(MagnetCircleCast(inv.itemActiveTime));
                Magnet.SetActive(true);

                AudioManager.instance.PlaySFX("Magnet");
            }
            else if (inv.name == "Balloon")
            {
                currentBuff = ActiveBuff.balloon;
                AudioManager.instance.PlaySFX("Ballon");
            }
            else if (inv.name == "RandomItem")
            {
                InventoryObject rnd = InventoryManager.instance.items[Random.Range(0, InventoryManager.instance.items.Length - 2)];
                StartBuffCoroutine(rnd);
                AudioManager.instance.PlaySFX("Random");
                yield break;
            }

            yield return new WaitForSeconds(inv.itemActiveTime);
            print("back to normnal :(");
            currentBuff = ActiveBuff.idle;

            GameManager.instance.gm_gamespeed = OGgamespeed; 
            xPosition = OGXposition;

            Magnet.SetActive(false);

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

    protected IEnumerator SpeedBoostTrail(float useTime)
    {
        for (float repeat = 0; repeat <= useTime; repeat+= useTime/100)
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
                yield return new WaitForSeconds(useTime/100);
            }

            Destroy(trail);
        }
    }

    protected IEnumerator InvencibilityColorChange(float useTime)
    {
        for (float repeat = 0; repeat < useTime; repeat+= useTime/100)
        {
            //for se repite 100 veces
            for (float i = 0.01f; i < 1; i += 0.01f)
            {
                GetComponent<SpriteRenderer>().color = Color.HSVToRGB(i, 0.82f, 0.1f);
                yield return new WaitForSeconds(useTime/100);
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected IEnumerator MagnetCircleCast(float useTime)
    {
        for (float repeat = 0; repeat < useTime; repeat+= useTime/100)
        {
            RaycastHit2D[] coinsInRange = Physics2D.CircleCastAll(transform.position, 14, Vector2.up);

            foreach(RaycastHit2D obj in coinsInRange)
            {
                GameObject coin = obj.transform.gameObject;
                if (coin.CompareTag("coin")) { coin.transform.position = Vector2.MoveTowards(coin.transform.position, transform.position, 1); }
            }

            yield return new WaitForSeconds(useTime/100);
        }
    }

    public void StartBuffCoroutine(InventoryObject inv)
    {
        StartCoroutine(UseBuff(inv));
        print(inv.name);
    }
}
