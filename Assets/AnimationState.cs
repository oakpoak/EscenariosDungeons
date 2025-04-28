using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class AnimationState : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public GameObject Player;              // Arrastra aquí tu GameObject “Player”

    [Header("Nombres de parámetros (bool)")]
    public string walkParam = "Walk";      // Nombre del parámetro Bool “Walk” en tu Animator
    public string runParam = "Run";       // Nombre del parámetro Bool “Run” en tu Animator

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (Player == null)
            Debug.LogError("Asignar el Player en el inspector de AnimationState.");
    }

    void Update()
    {
        if (Player == null) return;

        // Determina si el NavMeshAgent tiene de destino la posición actual del Player
        bool isChasing = agent.hasPath
                         && Vector3.Distance(agent.destination, Player.transform.position) < 0.1f;

        // Si persigue → Run = true, Walk = false
        // Si no     → Run = false, Walk = true
        animator.SetBool(runParam, isChasing);
        animator.SetBool(walkParam, !isChasing);
    }
}