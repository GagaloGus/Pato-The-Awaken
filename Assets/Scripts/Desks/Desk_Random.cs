using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desk_Random : MonoBehaviour
{
    public GameObject[] allDesks, shuffledDesks;
    public GameObject mesaInicio, mesaFinal, mesaTienda, fondo;
    public int amountDesksGenerated;
    float EndPos, StartPos;

    public Transform deskParent;
    public Slider LevelSlider;

    public Sprite[] bookSprites;
    // Start is called before the first frame update
    void Start()
    {

        StartPos = transform.position.x;
        //hace que el tamaño del array de Shuffle se cambie al que queremos automaticamente
        GameObject[] resizeArray = new GameObject[amountDesksGenerated + 2];
        shuffledDesks.CopyTo(resizeArray, 0); shuffledDesks = resizeArray;

        shuffledDesks[0] = mesaInicio; shuffledDesks[shuffledDesks.Length - 1] = mesaFinal; 

        int rnd_aux = 0;
        //randomiza las mesas generadas
        for(int i = 1; i < amountDesksGenerated + 1; i++)
        {
                int rnd = rnd_aux;
                do
                {
                    rnd = Random.Range(0, allDesks.Length);
                } while (rnd == rnd_aux);
                rnd_aux = rnd;
                shuffledDesks[i] = allDesks[rnd];
        }
        shuffledDesks[(int)Mathf.Round(shuffledDesks.Length / 2)] = mesaTienda;
        //instancia las mesas generadas para que esten en fila
        for (int i = 0; i < shuffledDesks.Length; i++)
        {
            GameObject desk = Instantiate(shuffledDesks[i], Vector3.up*5 + deskParent.position, Quaternion.identity, deskParent);

            float nextXPosition = 0;
            for (int count = 0; count < i ; count++)
            {
                if (count == 0) { nextXPosition += shuffledDesks[count].GetComponent<SpriteRenderer>().bounds.size.x / 2; }
                else { nextXPosition += shuffledDesks[count].GetComponent<SpriteRenderer>().bounds.size.x + 1; }
            }

            if (i > 0)
            {
                desk.transform.position = new Vector3(nextXPosition + desk.GetComponent<SpriteRenderer>().bounds.size.x / 2 , 5, 0) + deskParent.position;
                EndPos = nextXPosition + desk.GetComponent<SpriteRenderer>().bounds.size.x / 3 + 1;
            }
        }

        GameObject[] books = GameObject.FindGameObjectsWithTag("libro");
        foreach(GameObject booj in books)
        {
            booj.GetComponent<SpriteRenderer>().sprite = bookSprites[Random.Range(0, bookSprites.Length - 1)];
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * GameManager.instance.gm_gamespeed * Time.deltaTime);
        LevelSlider.value = GameManager.instance.MapValues(transform.position.x, StartPos, -EndPos, 0, 1);

        if(transform.position.x > -EndPos)
        fondo.transform.position = new Vector2(GameManager.instance.MapValues(transform.position.x, StartPos, -EndPos, 12.3f, -7.7f), fondo.transform.position.y);
    }
}
