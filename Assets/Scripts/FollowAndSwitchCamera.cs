using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndSwitchCamera : MonoBehaviour
{
    public Transform target; // El objeto que la cámara debe seguir (el personaje)
    public float rotationSpeed = 5f; // Velocidad de rotación de la cámara
    public float distance = 10f; // Distancia deseada desde el personaje
    public float height = 5f; // Altura desde el personaje

    private float currentRotationX = 0f; // Ángulo de rotación en el eje X
    private float currentRotationY = 0f; // Ángulo de rotación en el eje Y

    void Update()
    {
        if (target != null)
        {
            // Captura del movimiento del mouse
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Actualizar las rotaciones en el eje Y (horizontal) y X (vertical)
            currentRotationY += mouseX; // Rotación alrededor del eje Y
            currentRotationX -= mouseY; // Rotación alrededor del eje X

            // Limitar la rotación vertical (eje X) para evitar que la cámara pase por encima o por debajo del personaje
            currentRotationX = Mathf.Clamp(currentRotationX, -30f, 80f);
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcular la rotación de la cámara en los ejes X e Y
            Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);

            // Posicionar la cámara a una distancia fija del objetivo, manteniendo la altura y la distancia
            Vector3 offset = new Vector3(0, height, -distance); // La cámara está detrás del personaje a una distancia fija

            // Aplicar la rotación y posicionamiento de la cámara
            transform.position = target.position + rotation * offset;

            // Asegurarse que la cámara siempre mire al objetivo
            transform.LookAt(target);
        }
    }
}



