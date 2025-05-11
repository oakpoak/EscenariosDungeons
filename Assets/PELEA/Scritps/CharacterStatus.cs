using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [Header("Estado del personaje")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("KO")]
    public bool isKO = false;
    public string deathTrigger = "Death"; // Nombre del trigger en el Animator

    private Animator animator;
    private int characterIndex = -1;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterIndex = GetCharacterIndex();

        // Inicializar solo si no hay valor aún
        if (CharacterData.currentHealth[characterIndex] < 0)
        {
            Debug.Log($"[INIT] Usando currentHealth del Inspector para {name}: {currentHealth}");
            CharacterData.currentHealth[characterIndex] = currentHealth;
        }
        else
        {
            currentHealth = CharacterData.currentHealth[characterIndex];
            Debug.Log($"[INIT] Usando valor persistente para {name}: {currentHealth}");
        }

        // Si ya estaba muerto, activamos KO directamente
        if (currentHealth <= 0 && !isKO)
        {
            HandleKO();
        }
    }

    public void UpdateHealthBar()
    {
        var ui = BattleUIController.Instance;
        var bar = ui.GetHealthBarForCharacter(gameObject);
        if (bar != null)
            bar.value = currentHealth;
    }




    public void TakeDamage(float amount)
    {
        if (isKO) return;

        currentHealth -= amount;
        CharacterData.currentHealth[characterIndex] = currentHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HandleKO();
        }

        UpdateHealthBar();
    }

    public void Heal(float amount)
    {
        if (isKO) return;

        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        CharacterData.currentHealth[characterIndex] = currentHealth;
        UpdateHealthBar();
    }


    void HandleKO()
    {
        isKO = true;
        currentHealth = 0;
        CharacterData.currentHealth[characterIndex] = 0;

        Debug.Log($"{name} ha quedado KO");

        if (animator != null && !string.IsNullOrEmpty(deathTrigger))
        {
            animator.SetTrigger(deathTrigger);
        }
    }

    public bool IsAlive()
    {
        return !isKO;
    }

    int GetCharacterIndex()
    {
        if (gameObject.name.Contains("1")) return 0;
        if (gameObject.name.Contains("2")) return 1;
        if (gameObject.name.Contains("3")) return 2;
        return 0; // predeterminado
    }
}

