using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThreeD : MonoBehaviour
{
    private Interactable interactableInRange;  // Objeto interactivo en rango

    public GameObject[] characters; // Array para los personajes
    public int currentIndex = 0; // Índice del personaje actual
    private Vector3 NewPosition;
    private int num;

    // Start is called before the first frame update
    void Start()
    {
        

        // Asegúrate de que el array de personajes está inicializado y muestra solo el primero
        if (characters.Length > 0)
        {
           // ComponentsSwitch(true);
            
            ShowCharacter(currentIndex);
            
        }
    }

    void Update()
    {
        
        //Comando para intercambiar jugadores
        if (Input.GetKeyDown(KeyCode.E))
        {
            num = 1;
            NewPosition = characters[currentIndex].transform.position;
            NextChar(num);

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            num = -1;
            NewPosition = characters[currentIndex].transform.position;
            NextChar(num);
        }

        if (interactableInRange != null && interactableInRange.CouldInteract && Input.GetKeyDown(KeyCode.F))  // Si hay un objeto interactuable y el jugador presiona 'F'
        {
            interactableInRange.Interact();  // Llama al método interact de ese objeto
        }
    }

    void ShowCharacter(int index)
    {
        // Oculta todos los personajes
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Muestra el personaje seleccionado
        if (characters.Length > 0)
        {
            characters[index].SetActive(true);

        }
    }

    void NextChar(int n)
    {
        
        // Mostrar el siguiente personaje
        currentIndex = (currentIndex + n) % characters.Length;
        if(currentIndex < 0)
        {
            currentIndex = 2;
        }
        characters[currentIndex].GetComponent<Transform>().position = NewPosition;
        ShowCharacter(currentIndex);

        
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

    // Método para verificar si el jugador sale del rango de interacción
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            interactableInRange = null;
            Debug.Log("Objeto interactuable fuera de rango.");
        }
    }


}