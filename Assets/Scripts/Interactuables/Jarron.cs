using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jarron : ItemInteractive
{
    public string itemFound = "Llave";  // El �tem que se encuentra al romper el jarr�n
    private bool isBroken = false;  // Controla si el jarr�n ya fue roto

    // Sobrescribimos el m�todo Interact para definir lo que ocurre cuando el jugador interact�a con el jarr�n
    public override void Interact()
    {
        if (!isBroken)
        {
            isBroken = true;
            Debug.Log("�El jarr�n se ha roto! Has encontrado una: " + itemFound);

            // Aqu� puedes agregar m�s l�gica, como cambiar el modelo del jarr�n, agregar efectos visuales, etc.
            BreakJarEffect();
        }
        else
        {
            Debug.Log("El jarr�n ya est� roto.");
        }
    }

    // M�todo para simular el efecto visual de romper el jarr�n
    private void BreakJarEffect()
    {

        Debug.Log("Efecto de romper el jarr�n activado.");
    }
}
