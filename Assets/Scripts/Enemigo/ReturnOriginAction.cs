using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ReturnOriginAction : AIAction
{
    private Vector3 originalPosition; // Almacena la posici�n original
    private bool isReturning;
    public float speed = 1f;

    protected override void Initialization()
    {
        base.Initialization();
        // Almacena la posici�n original al inicializar la acci�n
        originalPosition = transform.position;
        isReturning = false; // Inicializa el estado de retorno
    }

    public override void PerformAction()
    {
        if (isReturning)
        {
            float step = speed * Time.deltaTime; // Velocidad de retorno
            // Mueve el personaje hacia la posici�n original
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);

            // Mira hacia la posici�n original
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

