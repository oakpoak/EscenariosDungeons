using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Puerta : ItemInteractive
{
    public BoxCollider Tiger;
    public GameObject Door1;
    public GameObject Door2;
    public GameManager GameManager;
    
    // Sobrescribimos el método Interact para definir qué sucede cuando el jugador interactúa con la puerta
    public override void Interact()
    {
        _Mechanism(false);
            
    }

    private void OnTriggerExit(Collider other)
    {
        _Mechanism(true);
    }

    private void _Mechanism(bool check)
    {
        Door1.SetActive(check);
        Door2.SetActive(check);

    }
}
