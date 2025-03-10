using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jarron : ItemInteractive
{
    public string itemFound = "Llave";  // El ítem que se encuentra al romper el jarrón
    private bool isBroken = false;  // Controla si el jarrón ya fue roto

    // Sobrescribimos el método Interact para definir lo que ocurre cuando el jugador interactúa con el jarrón
    public override void Interact()
    {
        if (!isBroken)
        {
            isBroken = true;
            Debug.Log("¡El jarrón se ha roto! Has encontrado una: " + itemFound);

            // Aquí puedes agregar más lógica, como cambiar el modelo del jarrón, agregar efectos visuales, etc.
            BreakJarEffect();
        }
        else
        {
            Debug.Log("El jarrón ya está roto.");
        }
    }

    // Método para simular el efecto visual de romper el jarrón
    private void BreakJarEffect()
    {

        Debug.Log("Efecto de romper el jarrón activado.");
    }
}
