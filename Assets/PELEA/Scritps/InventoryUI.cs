using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject overlay;
    public GameObject healthButton;
    public TextMeshProUGUI healthCountText;

    [Header("Configuración")]
    public int healthItemCount = 3;
    public float healingAmount = 20f;

    private bool isOpen = false;
    private bool isSelectingTarget = false;
    private bool itemUsedThisTurn = false;

    void Start()
    {
        UpdateHealthUI();
    }

    public void ToggleInventory()
    {
        if (itemUsedThisTurn) return;

        isOpen = !isOpen;
        overlay.SetActive(isOpen);
    }

    public void OnHealthItemClicked()
    {
        if (healthItemCount <= 0 || itemUsedThisTurn) return;

        isSelectingTarget = true;
        Debug.Log("Selecciona un personaje para curar.");
    }

    public void TryUseOnCharacter(GameObject targetCharacter)
    {
        if (!isSelectingTarget) return;

        isSelectingTarget = false;
        isOpen = false;
        overlay.SetActive(false);

        var healthBar = BattleUIController.Instance.GetHealthBarForCharacter(targetCharacter);
        if (healthBar != null)
        {
            healthBar.value += healingAmount;
            healthItemCount--;
            itemUsedThisTurn = true;
            UpdateHealthUI();

            Debug.Log($"Curación usada en {targetCharacter.name}. Quedan {healthItemCount}.");

            // 🔁 TERMINAR TURNO
            BattleManager.Instance.StartCoroutine("EnemyTurn"); // Llama el turno del enemigo directamente
        }
    }


    public void ResetTurnItemUsage()
    {
        itemUsedThisTurn = false;
    }

    void UpdateHealthUI()
    {
        if (healthCountText != null)
            healthCountText.text = healthItemCount.ToString();

        if (healthButton != null)
        {
            var btn = healthButton.GetComponent<Button>();
            if (btn != null)
                btn.interactable = healthItemCount > 0;
        }
    }

}

