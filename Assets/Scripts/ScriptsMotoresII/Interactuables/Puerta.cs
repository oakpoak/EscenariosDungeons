using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Puerta : ItemInteractive
{
    public Animator animator;  // El Animator de la puerta
    private bool isOpened = false;  // Si la puerta ya fue abierta

    void Start()
    {
        // Obtén el componente Animator de la puerta
        animator = GetComponent<Animator>();
    }

    // Sobrescribimos el método Interact para definir qué sucede cuando el jugador interactúa con la puerta
    public override void Interact()
    {
        if (!isOpened)
        {
            isOpened = true;
            animator.SetBool("Abrir", true);  // Activamos el parámetro "Abrir" en el Animator
        }
        else
        {
            Debug.Log("La puerta ya está abierta.");
        }
    }
}
