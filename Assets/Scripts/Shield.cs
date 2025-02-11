using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shield : MonoBehaviour
{
    public bool shield = false;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Michiguerrero"))
        {
            //Cambiar el body type de la caja a kinético
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            shield = true;

        }
    }


    public bool Check()
    {
        return shield;
    }
}
