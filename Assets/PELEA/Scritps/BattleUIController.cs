using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    public static BattleUIController Instance;

    public GameObject[] characterUIElements;
    public Slider[] characterHealthBars;
    public Button[] attackButtons;
    public Button[] skillButtons;
    public GameObject[] characters;
    public Slider bossHealthBar;

    public Transform[] uiPositions;
    public Vector3 selectedScale = new Vector3(0.15f, 0.15f, 1.0f);
    public Vector3 defaultScale = Vector3.one;

    private CharacterController characterController;

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
        characterController = FindObjectOfType<CharacterController>();

        for (int i = 0; i < attackButtons.Length; i++)
        {
            int index = i;
            attackButtons[i].onClick.AddListener(() => OnAttackButtonPressed(index));
            skillButtons[i].onClick.AddListener(() => OnSkillButtonPressed(index));
        }

        UpdateUIPositions();
    }

    void Update()
    {
        UpdateUIPositions();
    }

    private void UpdateUIPositions()
    {
        for (int i = 0; i < characterUIElements.Length; i++)
        {
            int positionIndex = (i - characterController.selectedIndex + uiPositions.Length) % uiPositions.Length;

            characterUIElements[i].transform.position = uiPositions[positionIndex].position;

            if (positionIndex == 0)
            {
                characterUIElements[i].transform.localScale = selectedScale;
                EnableButtons(i, true);
            }
            else
            {
                characterUIElements[i].transform.localScale = defaultScale;
                EnableButtons(i, false);
            }
        }
    }

    private void EnableButtons(int elementIndex, bool enable)
    {
        attackButtons[elementIndex].interactable = enable;
        skillButtons[elementIndex].interactable = enable;
    }

    public void OnAttackButtonPressed(int characterIndex)
    {
        Debug.Log($"Botón de ataque presionado para el personaje {characterIndex}");
        if (characterIndex == characterController.selectedIndex)
        {
            float attackDuration = characterController.characters[characterIndex]
                .GetComponent<Animator>()
                .GetCurrentAnimatorStateInfo(0).length;

            CameraController.Instance.RequestCameraChange(CameraController.Instance.characterAttackCamera, attackDuration);

            characterController.PerformAttack(characterIndex);

            bossHealthBar.value -= 10;

            if (bossHealthBar.value <= 0)
            {
                Debug.Log("¡El jefe ha sido derrotado!");
            }
        }
    }

    public void OnSkillButtonPressed(int characterIndex)
    {
        Debug.Log($"Botón de habilidad presionado para el personaje {characterIndex}");
        if (characterIndex == characterController.selectedIndex)
        {
            float skillDuration = 1.5f;

            CameraController.Instance.RequestCameraChange(CameraController.Instance.characterAttackCamera, skillDuration);

            Debug.Log($"Habilidad especial del personaje {characterIndex}");
            bossHealthBar.value -= 20;

            if (bossHealthBar.value <= 0)
            {
                Debug.Log("¡El jefe ha sido derrotado!");
            }
        }
    }

    public void ReduceCharacterHealth(int characterIndex, float damage)
    {
        characterHealthBars[characterIndex].value -= damage;

        if (characterHealthBars[characterIndex].value <= 0)
        {
            Debug.Log($"El personaje {characterIndex} ha sido derrotado.");
        }
    }
}