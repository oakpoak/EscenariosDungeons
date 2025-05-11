using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public enum BattleState { PlayerTurn, EnemyTurn, Busy }
    public BattleState currentState = BattleState.PlayerTurn;

    private int currentPlayerIndex = 0;
    private CharacterController characterController;
    private EnemyController enemyController;

    [Header("Efectos temporales")]
    public bool enemigoDebilitado = false;
    public int turnosDeVeneno = 0;

    [Header("Multiplicadores de efecto")]
    public float multiplicadorDebilitamiento = 1.25f;
    public float danioPorVeneno = 5f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        characterController = FindObjectOfType<CharacterController>();
        enemyController = FindObjectOfType<EnemyController>();
    }

    // 🎲 Lanza un dado de 20 caras y devuelve el resultado
    private int RollDice()
    {
        int result = Random.Range(1, 21);
        Debug.Log("🎲 Tirada de dado: " + result);

        DiceVisualizer.Instance.PlayDiceRoll(result);
        return result;
    }


    public void PlayerAttack(int characterIndex)
    {
        if (currentState != BattleState.PlayerTurn || characterIndex != characterController.selectedIndex)
            return;

        currentState = BattleState.Busy;

        int roll = RollDice();
        characterController.PerformAttackWithRoll(characterIndex, roll, OnPlayerAttackFinished);
    }

    public void UseSkill(int characterIndex)
    {
        if (currentState != BattleState.PlayerTurn || characterIndex != characterController.selectedIndex)
            return;

        currentState = BattleState.Busy;

        int roll = RollDice();
        characterController.PerformSkillWithRoll(characterIndex, roll, OnPlayerAttackFinished);
    }


    private void OnPlayerAttackFinished()
    {
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        currentState = BattleState.EnemyTurn;
        yield return new WaitForSeconds(1f);

        if (turnosDeVeneno > 0)
        {
            Debug.Log("El enemigo sufre daño por veneno.");
            BattleUIController.Instance.bossHealthBar.value -= danioPorVeneno;
            turnosDeVeneno--;
        }

        int roll = RollDice();
        enemyController.PerformAttackBasedOnRoll(roll, OnEnemyAttackFinished);
    }


    private void OnEnemyAttackFinished()
    {
        // Buscar un personaje que no esté KO
        CharacterController cc = FindObjectOfType<CharacterController>();
        for (int i = 0; i < cc.characters.Length; i++)
        {
            int index = (cc.selectedIndex + i) % cc.characters.Length;
            var status = cc.characters[index].GetComponent<CharacterStatus>();
            if (status != null && status.IsAlive())
            {
                cc.selectedIndex = index;
                cc.UpdateCharacterPositions();
                break;
            }
        }

        currentState = BattleState.PlayerTurn;

        InventoryUI inventory = FindObjectOfType<InventoryUI>();
        if (inventory != null)
            inventory.ResetTurnItemUsage();
    }


}
