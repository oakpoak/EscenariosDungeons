using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofredos : ItemInteractive
{
        public string item;  // El ítem que contiene el cofre
        private bool isOpened = false;  // Si el cofre ya fue abierto
        public GameManager gameManager;

        public GameObject Superior;

        // Sobrescribimos el método Interact para definir qué sucede cuando el jugador interactúa con el cofre
        public override void Interact()
        {
            if (!isOpened)
            {
                isOpened = true;
                Superior.transform.rotation = Quaternion.Euler(130f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                gameManager.llaveboss = true;

            }
            else
            {
                Debug.Log("El cofre ya está abierto.");
            }
        }

}
