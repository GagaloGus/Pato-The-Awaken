using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    void Awake()
    {
        if (!instance) //comprueba que instance no tenga informacion
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //si tiene info, ya existe un GM
        {
            Destroy(gameObject);
        }
    }
}
