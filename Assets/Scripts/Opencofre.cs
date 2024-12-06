using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opencofre : MonoBehaviour
{
    public Animator animator;  // El Animator de la cofreabriendo
    private bool isOpened = false;  // Si la cofre  ya fue abierta

    void Start()
    {
        // Obtén el componente Animator de la cofre
        animator = GetComponent<Animator>();
    }

    // Sobrescribimos el método Interact para definir qué sucede cuando el jugador interactúa con la puerta
    public  void Interact()
    {
        if (!isOpened)
        {
            isOpened = true;
            animator.SetBool("Abrir", true);  // Activamos el parámetro "Abrir" en el Animator
        }
        else
        {
            Debug.Log("El cofre ya está abierta.");
        }
    }
}
