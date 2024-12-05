using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody rb;

    void Start()
    {
        // Obtener el Rigidbody adjunto al objeto
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento en el espacio del mundo (con fuerzas)
        float movimientoX = Input.GetAxis("Horizontal") * velocidad;
        float movimientoZ = Input.GetAxis("Vertical") * velocidad;

        // Vector de movimiento
        Vector3 movimiento = new Vector3(movimientoX, 0, movimientoZ);

        // Usamos el Rigidbody para mover el objeto
        rb.MovePosition(transform.position + movimiento * Time.deltaTime);
    }
}
