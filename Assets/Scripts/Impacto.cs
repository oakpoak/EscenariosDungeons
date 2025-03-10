using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impacto : MonoBehaviour
{
    public float fuerzaRetroceso = 10f;        // Fuerza del retroceso
    public float velocidadMaximaRetroceso = 5f; // Velocidad máxima del retroceso
    public float friccion = 0.2f;              // Fricción para reducir el retroceso con el tiempo
    public float gravedad = -9.8f;             // Gravedad aplicada en el retroceso

    private Rigidbody rb;                      // Rigidbody del jugador
    private Vector3 velocidad = Vector3.zero;  // Velocidad del jugador

    private void Start()
    {
        // Obtener el Rigidbody del jugador
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody en el objeto.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Impacto detectado con el jugador");

            // Obtener el Rigidbody del jugador
            Rigidbody rbJugador = other.GetComponent<Rigidbody>();

            if (rbJugador != null)
            {
                // Calcular la dirección de retroceso
                Vector3 direccionRetroceso = (other.transform.position - transform.position).normalized;

                // Aplicar la fuerza de retroceso directamente al Rigidbody
                Vector3 fuerzaRetrocesoFinal = direccionRetroceso * fuerzaRetroceso;
                rbJugador.AddForce(fuerzaRetrocesoFinal, ForceMode.Impulse);

                // Limitar la velocidad máxima
                if (rbJugador.velocity.magnitude > velocidadMaximaRetroceso)
                {
                    rbJugador.velocity = rbJugador.velocity.normalized * velocidadMaximaRetroceso;
                }
            }
            else
            {
                Debug.LogWarning("El jugador no tiene un Rigidbody.");
            }
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            // Aplicar fricción para reducir la velocidad con el tiempo
            velocidad *= (1 - friccion * Time.fixedDeltaTime);

            // Simular la gravedad manualmente
            velocidad.y += gravedad * Time.fixedDeltaTime;

            // Mover al jugador aplicando la velocidad al Rigidbody
            rb.velocity = velocidad;
        }
    }
}