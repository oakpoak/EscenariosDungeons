using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    private Vector3 originalPosition; // Almacena la posici�n original
    public float distanceThreshold = 0.001f; // Umbral de distancia para considerar que se ha alcanzado la posici�n

    public override void Initialization()
    {
        base.Initialization();
        // Almacena la posici�n original al inicializar la decisi�n
        originalPosition = transform.position;
    }

    public override bool Decide()
    {
        // Verifica la distancia entre la posici�n actual y la posici�n original
        return Vector3.Distance(transform.position, originalPosition) <= distanceThreshold;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        // Almacena la posici�n original al entrar al estado
        originalPosition = transform.position;
    }
}
