using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ReturnOriginAction : AIAction
{
    private Vector3 originalPosition; // Almacena la posición original
    private bool isReturning;
    public float speed = 1f;

    protected override void Initialization()
    {
        base.Initialization();
        // Almacena la posición original al inicializar la acción
        originalPosition = transform.position;
        isReturning = false; // Inicializa el estado de retorno
    }

    public override void PerformAction()
    {
        if (isReturning)
        {
            float step = speed * Time.deltaTime; // Velocidad de retorno
            // Mueve el personaje hacia la posición original
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);

            // Mira hacia la posición original
            if (transform.position != originalPosition)
            {
                transform.LookAt(originalPosition);
            }
        }
    }

    public void StartReturning()
    {
        isReturning = true; // Cambia el estado a "retornando"
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        StartReturning(); // Inicia el retorno al entrar al estado
    }
}

