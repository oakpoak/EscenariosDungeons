using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndSwitchCamera : MonoBehaviour
{
    
    [Header("Referencias")]
    public Transform target;          // Tu personaje

    [Header("Offset inicial (se calcula al Start)")]
    private Vector3 offset;           // distancia fija y altura fija

    public GameManager GM;
    public Transform Normal;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("No hay target asignado en FollowAndSwitchCamera");
            return;
        }
        // Calculamos el desplazamiento inicial entre cámara y jugador
        offset = transform.position - target.position;
    }
     
    void LateUpdate()
    { 

        if (GM.Fight == false && GM.puzzle == false)
        {
            if (target == null) return;

            // Sólo actualizamos la posición: target + offset
            transform.position = target.position + offset;
        }
        else if (GM.puzzle == true)
        {
            Normal = GetComponent<Transform>();
            transform.position = Normal.position;
        }
    }
}



