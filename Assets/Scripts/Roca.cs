using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roca : MonoBehaviour
{
    public float rollForce = 10f; // Fuerza con la que la roca rodará
    public Transform initialPosition; //Posicion inicial de la roca

    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetRock();
    }

    public void ActivateRock()
    {
        rb.isKinematic = false; //activa la fisica
        rb.AddForce(Vector3.forward*rollForce, ForceMode.Impulse); //Aplica fuerza a la roca


    }
    public void ResetRock()
    {
        transform.position = initialPosition.position;
        rb.isKinematic = true; //desactiva la fisica
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    
}
