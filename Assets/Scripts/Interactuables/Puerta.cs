using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Puerta : ItemInteractive
{
    public Animator animator;  // El Animator de la puerta
    private bool isOpened = false;  // Si la puerta ya fue abierta

    void Start()
    {
        // Obt�n el componente Animator de la puerta
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
            Debug.Log("La puerta ya est� abierta.");
        }
    }
}
