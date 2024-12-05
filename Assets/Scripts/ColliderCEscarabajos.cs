using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCEscarabajos : MonoBehaviour
{
    [SerializeField] public GameObject PantallaEscarabajos;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            PantallaEscarabajos.SetActive(true);
        }
    }
}
