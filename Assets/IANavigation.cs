// IANavigation.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent), typeof(GizmoSphere))]
public class IANavigation : Freeze
{
    [Header("Puntos de patrulla")]
    public Transform pointA;
    public Transform pointB;
    public float arriveThreshold = 0.5f;

    [Header("Detección (GizmoSphere)")]
    public GizmoSphere detectionGizmo;     // No modificar GizmoSphere.cs

    [Header("Jugador a seguir")]
    public GameObject playerObject;        // Asignar en Inspector

    [Header("Hold Settings")]
    [Tooltip("Tiempo en segundos que permanece detenido al salir del área")]
    public float HoldTime = 2f;

    [Header("Animación")]
    public AnimationState State;           // Referencia al script con los nombres de parámetros
    public Animator animator;              // Arrastra aquí tu Animator

    private NavMeshAgent agent;
    private Vector3[] patrolPoints;
    private int patrolIndex = 0;
    private Vector3 lastPatrolPos;

    private bool isChasing = false;
    private bool isReturning = false;

    // Para Hold
    public bool isHolding = false;
    private Coroutine holdCoroutine;
    private bool wasPlayerInside = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (detectionGizmo == null)
            detectionGizmo = GetComponent<GizmoSphere>();
        if (playerObject == null)
            Debug.LogError("Asignar playerObject en el Inspector.");
        if (State == null)
            Debug.LogError("Asignar referencia a AnimationState en el Inspector.");

        patrolPoints = new Vector3[] { pointA.position, pointB.position };
        agent.destination = patrolPoints[patrolIndex];

        // Al inicio estamos patrullando
        animator.SetBool(State.walkParam, true);
        animator.SetBool(State.runParam, false);
    }

    void Update()
    {
        if (playerObject == null || detectionGizmo == null || isHolding)
            return;

        Vector3 playerPos = playerObject.transform.position;
        Vector3 center = transform.TransformPoint(
            new Vector3(
                detectionGizmo.offsetX,
                detectionGizmo.offsetY,
                detectionGizmo.offsetZ
            )
        );
        bool playerInside = Vector3.Distance(playerPos, center) <= detectionGizmo.radius;

        Debug.DrawLine(transform.position, playerPos,
                       playerInside ? Color.red : Color.green);

        // — Detectamos salida SOLO si antes estaba dentro y estamos persiguiendo —
        if (wasPlayerInside && !playerInside && isChasing)
        {
            isChasing = false;
            // Hold: ambos parámetros false → Idle
            animator.SetBool(State.runParam, false);
            animator.SetBool(State.walkParam, false);
            holdCoroutine = StartCoroutine(HoldThenReturn());
        }

        // — Detectamos entrada al área —
        if (!wasPlayerInside && playerInside)
        {
            lastPatrolPos = transform.position;
            isChasing = true;
            // Cambio a Run
            animator.SetBool(State.runParam, true);
            animator.SetBool(State.walkParam, false);
        }

        wasPlayerInside = playerInside;

        // —— Patrullaje —— (solo cuando no chase ni returning)
        if (!isChasing && !isReturning)
        {
            // Aseguramos animación Walk
            animator.SetBool(State.walkParam, true);
            animator.SetBool(State.runParam, false);

            if (!agent.pathPending && agent.remainingDistance < arriveThreshold)
            {
                patrolIndex = 1 - patrolIndex;
                agent.destination = patrolPoints[patrolIndex];
            }
        }

        // —— Persecución continua ——
        if (isChasing)
        {
            agent.destination = playerPos;
        }

        // —— Regreso —— 
        if (isReturning)
        {
            // Aseguramos animación Walk
            animator.SetBool(State.walkParam, true);
            animator.SetBool(State.runParam, false);

            if (!agent.pathPending && agent.remainingDistance < arriveThreshold)
            {
                isReturning = false;
                agent.destination = patrolPoints[patrolIndex];
            }
        }
    }

    private IEnumerator HoldThenReturn()
    {
        isHolding = true;
        agent.isStopped = true;

        // Espera el tiempo de Hold en Idle
        yield return new WaitForSeconds(HoldTime);

        agent.isStopped = false;
        isReturning = true;
        agent.destination = lastPatrolPos;

        isHolding = false;
    }
}


