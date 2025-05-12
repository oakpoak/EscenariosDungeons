using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreTres : ItemInteractive
{
    public string item;  // El ítem que contiene el cofre
    private bool isOpened = false;  // Si el cofre ya fue abierto
    public GameManager gameManager;

    public GameObject Superior;
    public override void Interact()
    {
        if (!isOpened)
        {
            isOpened = true;
            Superior.transform.rotation = Quaternion.Euler(130f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            gameManager.llave2Amarilla = true;
        }
    }
}
