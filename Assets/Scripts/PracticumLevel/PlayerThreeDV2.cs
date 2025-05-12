using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerThreeDV2 : MonoBehaviour
{
    private Interactable interactableInRange; // Objeto interactivo en rango
    public GameObject catito; // Referencia al objeto "Catito" asignable en el Inspector
    public Texture[] albedoTextures; // Array de texturas para el albedo
    private int currentAlbedoIndex = 0; // Índice de la textura de albedo actual
    public GameObject GameManager;

    void Update()
    {
        if ( GameManager.GetComponent<GameManager>().Fight==false && GameManager.GetComponent<GameManager>().puzzle==false)
        { 
            // Cambiar albedo al siguiente con "E"
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeAlbedo(1); // Avanza al siguiente albedo
            }
            // Cambiar albedo al anterior con "Q"
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeAlbedo(-1); // Retrocede al albedo anterior
            }

            // Interacción con el objeto en rango al presionar "F"

            if (interactableInRange != null && interactableInRange.CouldInteract && Input.GetKeyDown(KeyCode.F))
            {
                interactableInRange.Interact(); // Llama al método interact
            }
        }
        
    }

    void ChangeAlbedo(int step)
    {
        // Cambiar el índice del albedo actual
        currentAlbedoIndex = (currentAlbedoIndex + step) % albedoTextures.Length;

        // Ajustar el índice si es negativo
        if (currentAlbedoIndex < 0)
        {
            currentAlbedoIndex = 2;
        }

        // Cambiar el albedo del material del objeto "Catito"
        if (catito != null)
        {
            Renderer renderer = catito.GetComponent<Renderer>();
            if (renderer != null && albedoTextures.Length > 0)
            {
                renderer.material.SetTexture("_BaseMap", albedoTextures[currentAlbedoIndex]);
            }
            else
            {
                Debug.LogError("El objeto 'Catito' no tiene un Renderer o no se asignaron texturas al albedo.");
            }
        }
        else
        {
            Debug.LogError("El objeto 'Catito' no está asignado en el Inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable itemInteractive = other.GetComponent<Interactable>();
        if (itemInteractive != null)
        {
            interactableInRange = itemInteractive;
            Debug.Log("Objeto interactuable llamado: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            interactableInRange = null;
            Debug.Log("Objeto interactuable fuera de rango.");
        }
    }
}
