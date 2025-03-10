using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractive : MonoBehaviour, Interactable
{
    private bool isInteracting = false;

    // Implementaci�n de la propiedad CouldInteract (para saber si el jugador est� cerca)
    public bool CouldInteract 
    { 
        get; 
        private set; 
    }

    // M�todo para interactuar con el objeto (ser� sobrecargado por las clases hijas)
    public virtual void Interact()
    {
        Debug.Log("Interacci�n con el objeto, Se reproducir� la animaci�n y una musiquita");
    }

    // M�todo para detectar cuando el jugador entra en el rango de interacci�n
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CouldInteract = true;
        }
    }

    // M�todo para detectar cuando el jugador sale del rango de interacci�n
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CouldInteract = false;
        }
    }
}
