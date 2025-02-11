using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicializar : MonoBehaviour
{
    public GameObject []Ataques= new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        Ataques[0].SetActive(false);
        Ataques[1].SetActive(false);
        Ataques[2].SetActive(false);
        Ataques[3].SetActive(false);
        Ataques[4].SetActive(false);
    }

}
