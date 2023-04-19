using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float stunTime;
    public void SpawnRandom(float chance)
    {
        float rnd = Random.Range(0, 1f);
        if (!(rnd <= chance)) { Destroy(gameObject); }
    }
}
