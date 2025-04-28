// IANavigation.cs
using UnityEngine;
using UnityEngine.AI;

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

    private NavMeshAgent agent;
    private Vector3[] patrolPoints;
    private int patrolIndex = 0;
    private Vector3 lastPatrolPos;
    private bool isChasing = false;
    private bool isReturning = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (detectionGizmo == null)
            detectionGizmo = GetComponent<GizmoSphere>();
        if (playerObject == null)
            Debug.LogError("Asignar playerObject en el Inspector.");

        patrolPoints = new Vector3[] { pointA.position, pointB.position };
        agent.destination = patrolPoints[patrolIndex];
    }

    void Update()
    {
        if (playerObject == null || detectionGizmo == null) return;

        Vector3 playerPos = playerObject.transform.position;
        // calcula el centro del gizmo en world space
        Vector3 offset = new Vector3(
            detectionGizmo.offsetX,
            detectionGizmo.offsetY,
            detectionGizmo.offsetZ
        );
        Vector3 center = transform.TransformPoint(offset);

        // detección por distancia al radio
        bool playerInside = Vector3.Distance(playerPos, center) <= detectionGizmo.radius;

        // opcional: línea roja dentro, verde fuera
        Debug.DrawLine(transform.position, playerPos,
                       playerInside ? Color.red : Color.green);

        // —— Patrullaje ——
        if (!isChasing && !isReturning)
        {
            if (!agent.pathPending && agent.remainingDistance < arriveThreshold)
            {
                patrolIndex = 1 - patrolIndex;
                agent.destination = patrolPoints[patrolIndex];
            }
            if (playerInside)
            {
                lastPatrolPos = transform.position;
                isChasing = true;
            }
        }

        // —— Persecución ——
        if (isChasing)
        {
            if (playerInside)
            {
                agent.destination = playerPos;
            }
            else
            {
                isChasing = false;
                isReturning = true;
                agent.destination = lastPatrolPos;
            }
        }

        // —— Regreso ——
        if (isReturning)
        {
            if (!agent.pathPending && agent.remainingDistance < arriveThreshold)
            {
                isReturning = false;
                agent.destination = patrolPoints[patrolIndex];
            }
        }
    }
}

