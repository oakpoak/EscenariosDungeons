using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaActivador : MonoBehaviour
{
    public Roca rockRoller; // Asigna el objeto roca con el script RockRoller

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que entra es el personaje
        {
            rockRoller.ActivateRock(); // Activa la roca
        }
    }
}
