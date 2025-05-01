using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    private Animator animator;
    public float attackDamage = 15f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformAttack();
        }
    }

    public void PerformAttack()
    {
        Debug.Log("El enemigo inicia el ataque");
        float attackDuration = animator.GetCurrentAnimatorStateInfo(0).length;

        // Cambiar a la cámara de ataque del enemigo
        CameraController.Instance.RequestCameraChange(CameraController.Instance.enemyAttackCamera, attackDuration);

        // Reproducir la animación de ataque del enemigo
        animator.SetTrigger("AttackTrigger");
        Debug.Log("Animación de ataque del enemigo activada");

        StartCoroutine(HandleAttackSequence());
    }

    public void PlayDamageAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("DamageTrigger");
        }
        else
        {
            Debug.LogWarning("El Animator del enemigo no está asignado.");
        }
    }

    System.Collections.IEnumerator HandleAttackSequence()
    {
        Debug.Log("Comenzando secuencia de ataque del enemigo");
        float attackDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(attackDuration);

        Debug.Log("Finalizando secuencia de ataque del enemigo");
        CameraController.Instance.RequestCameraChange(CameraController.Instance.defaultCamera, 0.1f);

        var battleUI = BattleUIController.Instance;
        for (int i = 0; i < battleUI.characters.Length; i++)
        {
            var character = battleUI.characters[i];
            var characterAnimator = character.GetComponent<Animator>();

            if (characterAnimator != null)
            {
                characterAnimator.SetTrigger("DamageTrigger");
            }

            battleUI.ReduceCharacterHealth(i, attackDamage);
        }
    }
}