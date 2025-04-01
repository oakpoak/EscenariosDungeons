using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    private Vector3 originalPosition; // Almacena la posición original
    public float distanceThreshold = 0.001f; // Umbral de distancia para considerar que se ha alcanzado la posición

    public override void Initialization()
    {
        base.Initialization();
        // Almacena la posición original al inicializar la decisión
        originalPosition = transform.position;
    }

    public override bool Decide()
    {
        // Verifica la distancia entre la posición actual y la posición original
        return Vector3.Distance(transform.position, originalPosition) <= distanceThreshold;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        // Almacena la posición original al entrar al estado
        originalPosition = transform.position;
    }
}
