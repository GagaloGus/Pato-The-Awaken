using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk_Random : MonoBehaviour
{
    public GameObject[] allDesks, shuffledDesks;
    public int amountDesksGenerated;
    // Start is called before the first frame update
    void Start()
    {
        //hace que el tamaño del array de Shuffle se cambie al que queremos automaticamente
        GameObject[] resizeArray = new GameObject[amountDesksGenerated];
        shuffledDesks.CopyTo(resizeArray, 0); shuffledDesks = resizeArray;

        //randomiza las mesas generadas
        for(int i = 0; i < amountDesksGenerated; i++)
        {
            int rnd = Random.Range(0, allDesks.Length);
            shuffledDesks[i] = allDesks[rnd];
        }

        //instancia las mesas generadas para que esten en fila
        for(int i = 0; i < shuffledDesks.Length; i++)
        {
            GameObject desk = Instantiate(shuffledDesks[i], Vector2.zero, Quaternion.identity);

            float nextXPosition = 0;
            for (int count = 0; count < i ; count++)
            {
                if (count == 0) { nextXPosition += shuffledDesks[count].GetComponent<SpriteRenderer>().bounds.size.x / 2; }
                else { nextXPosition += shuffledDesks[count].GetComponent<SpriteRenderer>().bounds.size.x; }
            }

            if (i > 0)
            {
                desk.transform.position = new Vector2(nextXPosition + desk.GetComponent<SpriteRenderer>().bounds.size.x / 2, 0);
            }
        }
    }

}
