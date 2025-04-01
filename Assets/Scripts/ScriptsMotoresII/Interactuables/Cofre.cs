using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : ItemInteractive
{
    public string item;  // El �tem que contiene el cofre
    private bool isOpened = false;  // Si el cofre ya fue abierto

    // Sobrescribimos el m�todo Interact para definir qu� sucede cuando el jugador interact�a con el cofre
    public override void Interact()
    {
        if (!isOpened)
        {
            isOpened = true;
            Debug.Log("�Cofre abierto! Has obtenido: " + item);
        }
        else
        {
            Debug.Log("El cofre ya est� abierto.");
        }
    }
}
