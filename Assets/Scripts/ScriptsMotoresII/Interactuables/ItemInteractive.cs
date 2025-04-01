using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractive : MonoBehaviour, Interactable
{
    private bool isInteracting = false;

    // Implementación de la propiedad CouldInteract (para saber si el jugador está cerca)
    public bool CouldInteract 
    { 
        get; 
        private set; 
    }

    // Método para interactuar con el objeto (será sobrecargado por las clases hijas)
    public virtual void Interact()
    {
        Debug.Log("Interacción con el objeto, Se reproducirá la animación y una musiquita");
    }

    // Método para detectar cuando el jugador entra en el rango de interacción
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CouldInteract = true;
        }
    }

    // Método para detectar cuando el jugador sale del rango de interacción
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CouldInteract = false;
        }
    }
}
