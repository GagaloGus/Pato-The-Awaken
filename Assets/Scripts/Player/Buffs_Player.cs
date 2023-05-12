using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs_Player : MonoBehaviour
{
    protected enum ActiveBuff { idle, doubleJump, fast, invincible, magnet, balloon, random }
    protected ActiveBuff currentBuff;

    public float xPosition;

    public IEnumerator UseBuff(InventoryObject inv)
    {
        currentBuff = ActiveBuff.idle;
        float speedMult = 1;

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

                AudioManager.instance.PlayMusic("Faster Buff");
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
            }
            else if (inv.name == "Balloon")
            {
                currentBuff = ActiveBuff.balloon;
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

            if (inv.name == "FasterBuff")
            {
                GameManager.instance.gm_gamespeed /= speedMult;
                xPosition -= speedMult;
            }

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
        for (int repeat = 0; repeat <= useTime * 6; repeat++)
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
                yield return new WaitForSeconds(0.025f);
            }

            Destroy(trail);
        }
    }

    protected IEnumerator InvencibilityColorChange(float useTime)
    {
        for (int repeat = 0; repeat <= useTime; repeat++)
        {
            //for se repite 100 veces
            for (float i = 0.01f; i < 1; i += 0.01f)
            {
                GetComponent<SpriteRenderer>().color = Color.HSVToRGB(i, 0.82f, 1);
                yield return new WaitForSeconds(0.005f);
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void StartBuffCoroutine(InventoryObject inv)
    {
        StartCoroutine(UseBuff(inv));
        print(inv.name);
    }
}
