using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    private Animator animator;

    [Header("Configuración de enemigo")]
    public float maxHealth = 100f;
    private float currentHealth;

    public float attackDamage = 15f;
    public float fixedAttackDuration = 1.5f; // Ajustar según la duración real de la animación

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        // Inicializar salud
        currentHealth = maxHealth;

        // Configurar barra de vida visual
        BattleUIController.Instance.bossHealthBar.maxValue = maxHealth;
        BattleUIController.Instance.bossHealthBar.value = currentHealth;
    }

    public void PerformAttack(System.Action onAttackFinished)
    {
        animator.SetTrigger("AttackTrigger");
        CameraController.Instance.RequestCameraChange(CameraController.Instance.enemyAttackCamera, fixedAttackDuration);
        StartCoroutine(HandleAttackWithRoll(Random.Range(1, 21), onAttackFinished));
    }

    public void PerformAttackBasedOnRoll(int roll, System.Action onAttackFinished)
    {
        animator.SetTrigger("AttackTrigger");
        CameraController.Instance.RequestCameraChange(CameraController.Instance.enemyAttackCamera, fixedAttackDuration);
        StartCoroutine(HandleAttackWithRoll(roll, onAttackFinished));
    }

    IEnumerator HandleAttackWithRoll(int roll, System.Action onAttackFinished)
    {
        yield return new WaitForSeconds(fixedAttackDuration);

        CameraController.Instance.RequestCameraChange(CameraController.Instance.defaultCamera, 0.1f);

        var ui = BattleUIController.Instance;

        if (roll == 1)
        {
            Debug.Log("El enemigo falló el ataque.");
        }
        else if (roll >= 2 && roll <= 8)
        {
            var vivos = new System.Collections.Generic.List<GameObject>();
            for (int i = 0; i < ui.characters.Length; i++)
            {
                var status = ui.characters[i].GetComponent<CharacterStatus>();
                if (status != null && status.IsAlive())
                    vivos.Add(ui.characters[i]);
            }

            if (vivos.Count == 0)
            {
                Debug.Log("No hay personajes vivos para atacar.");
            }
            else
            {
                GameObject targetCharacter = vivos[Random.Range(0, vivos.Count)];
                int targetIndex = System.Array.IndexOf(ui.characters, targetCharacter);

                Debug.Log($"Daño leve a un solo personaje vivo: personaje {targetIndex}");

                var anim = targetCharacter.GetComponent<Animator>();
                if (anim) anim.SetTrigger("DamageTrigger");

                var status = targetCharacter.GetComponent<CharacterStatus>();
                if (status != null)
                    status.TakeDamage(attackDamage * 0.5f);
            }
        }
        else if (roll >= 9 && roll <= 14)
        {
            Debug.Log("Daño leve a todos los personajes");
            for (int i = 0; i < ui.characters.Length; i++)
            {
                var character = ui.characters[i];
                var anim = character.GetComponent<Animator>();
                if (anim) anim.SetTrigger("DamageTrigger");

                var status = character.GetComponent<CharacterStatus>();
                if (status != null)
                    status.TakeDamage(attackDamage * 0.5f);
            }
        }
        else if (roll >= 15 && roll <= 19)
        {
            Debug.Log("Daño normal a todos los personajes");
            for (int i = 0; i < ui.characters.Length; i++)
            {
                var character = ui.characters[i];
                var anim = character.GetComponent<Animator>();
                if (anim) anim.SetTrigger("DamageTrigger");

                var status = character.GetComponent<CharacterStatus>();
                if (status != null)
                    status.TakeDamage(attackDamage);
            }
        }
        else if (roll == 20)
        {
            Debug.Log("¡Golpe crítico! Daño alto a todos los personajes");
            for (int i = 0; i < ui.characters.Length; i++)
            {
                var character = ui.characters[i];
                var anim = character.GetComponent<Animator>();
                if (anim) anim.SetTrigger("DamageTrigger");

                var status = character.GetComponent<CharacterStatus>();
                if (status != null)
                    status.TakeDamage(attackDamage * 1.5f);
            }
        }

        onAttackFinished?.Invoke();
    }

    public void PlayDamageAnimation()
    {
        if (animator != null)
            animator.SetTrigger("DamageTrigger");
    }

    // (Opcional) método para recibir daño directo si lo necesitas desde otro script
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        BattleUIController.Instance.bossHealthBar.value = currentHealth;

        if (currentHealth <= 0f)
        {
            Debug.Log("El enemigo ha sido derrotado.");
        }
    }
}





