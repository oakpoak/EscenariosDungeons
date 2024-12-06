using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opencofre : ItemInteractive
{
    public Animator animator;  // El Animator de la cofreabriendo
    private bool isOpened = false;  // Si la cofre  ya fue abierta

    void Start()
    {
        // Obt�n el componente Animator de la cofre
        animator = GetComponent<Animator>();
    }

    // Sobrescribimos el m�todo Interact para definir qu� sucede cuando el jugador interact�a con la puerta
    public override void Interact()
    {
        if (!isOpened)
        {
            isOpened = true;
            animator.SetBool("Abrir", true);  // Activamos el par�metro "Abrir" en el Animator
        }
        else
        {
            Debug.Log("El cofre ya est� abierta.");
        }
    }
}