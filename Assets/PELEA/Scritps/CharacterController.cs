using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public GameObject[] characters;
    public Transform[] positions;
    public int selectedIndex = 0;

    private Animator[] animators;

    [Header("Daño base del jugador (por tipo de resultado de dado)")]
    public float damageLeve = 5f;
    public float damageNormal = 10f;
    public float damageCritico = 15f;

    [Header("Habilidad - Caballero")]
    public float habilidadCaballeroDanioNormal = 15f;
    public float habilidadCaballeroDanioCritico = 25f;
    public float habilidadCaballeroAutodanio = 5f;

    [Header("Habilidad - Mago")]
    public float habilidadMagoCuracion = 10f;
    public float habilidadMagoAutodaño = 5f;
    public float habilidadMagoRoboVida = 5f;

    [Header("Habilidad - Arquero")]
    public float habilidadArqueroAutodanio = 5f;

    void Start()
    {
        animators = new Animator[characters.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            animators[i] = characters[i].GetComponent<Animator>();
        }

        UpdateCharacterPositions();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex = (selectedIndex + 1) % characters.Length;
            UpdateCharacterPositions();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex = (selectedIndex - 1 + characters.Length) % characters.Length;
            UpdateCharacterPositions();
        }
    }

    public void UpdateCharacterPositions()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            int positionIndex = (i - selectedIndex + 2 + characters.Length) % characters.Length;
            characters[i].transform.position = positions[positionIndex].position;
        }
        // Al final de UpdateCharacterPositions()
        var status = characters[selectedIndex].GetComponent<CharacterStatus>();
        if (status != null && status.isKO)
        {
            BattleUIController.Instance.SetButtonsInteractable(false);
        }
        else
        {
            BattleUIController.Instance.SetButtonsInteractable(true);
        }

    }

    public void PerformAttackWithRoll(int characterIndex, int roll, System.Action onAttackFinished)
    {
        var status = characters[characterIndex].GetComponent<CharacterStatus>();
        if (status != null && status.isKO)
        {
            Debug.Log($"{characters[characterIndex].name} está KO y no puede atacar.");
            onAttackFinished?.Invoke(); // ← esto es lo que faltaba
            return;
        }


        float attackDuration = animators[characterIndex].GetCurrentAnimatorStateInfo(0).length;
        CameraController.Instance.RequestCameraChange(CameraController.Instance.characterAttackCamera, attackDuration);
        animators[characterIndex].SetTrigger("AttackTrigger");
        StartCoroutine(HandleAttackSequenceWithRoll(characterIndex, roll, onAttackFinished));
    }

    IEnumerator HandleAttackSequenceWithRoll(int characterIndex, int roll, System.Action onAttackFinished)
    {
        float attackDuration = animators[characterIndex].GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(attackDuration);

        CameraController.Instance.RequestCameraChange(CameraController.Instance.defaultCamera, 0.1f);

        float finalDamage = 0f;

        if (roll <= 3)
        {
            Debug.Log("El ataque del jugador falló.");
        }
        else if (roll <= 11)
        {
            Debug.Log("Daño leve infligido al enemigo.");
            finalDamage = damageLeve;
        }
        else if (roll <= 19)
        {
            Debug.Log("Daño normal infligido al enemigo.");
            finalDamage = damageNormal;
        }
        else if (roll == 20)
        {
            Debug.Log("¡Crítico! Daño alto infligido al enemigo.");
            finalDamage = damageCritico;
        }

        if (finalDamage > 0)
        {
            EnemyController.Instance.PlayDamageAnimation();
        }

        BattleUIController.Instance.bossHealthBar.value -= finalDamage;

        if (BattleUIController.Instance.bossHealthBar.value <= 0)
            Debug.Log("¡El jefe ha sido derrotado!");

        onAttackFinished?.Invoke();
    }

    public void PerformSkillWithRoll(int characterIndex, int roll, System.Action onSkillFinished)
    {
        var status = characters[characterIndex].GetComponent<CharacterStatus>();
        if (status != null && status.isKO)
        {
            Debug.Log($"{characters[characterIndex].name} está KO y no puede usar habilidades.");
            onSkillFinished?.Invoke(); // ← importante
            return;
        }


        float duration = animators[characterIndex].GetCurrentAnimatorStateInfo(0).length;
        CameraController.Instance.RequestCameraChange(CameraController.Instance.characterAttackCamera, duration);
        animators[characterIndex].SetTrigger("SkillTrigger");

        StartCoroutine(HandleSkillSequence(characterIndex, roll, onSkillFinished));
    }

    IEnumerator HandleSkillSequence(int characterIndex, int roll, System.Action onSkillFinished)
    {
        float duration = animators[characterIndex].GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        CameraController.Instance.RequestCameraChange(CameraController.Instance.defaultCamera, 0.1f);

        var ui = BattleUIController.Instance;
        float damage = 0f;

        if (characterIndex == 0) // Caballero
        {
            if (roll == 1)
            {
                Debug.Log("El caballero se hace daño a sí mismo.");
                ui.characters[0].GetComponent<CharacterStatus>()?.TakeDamage(habilidadCaballeroAutodanio);
            }
            else if (roll <= 4)
            {
                Debug.Log("El gran espadazo falló.");
            }
            else if (roll <= 19)
            {
                Debug.Log("El caballero inflige daño normal al jefe.");
                damage = habilidadCaballeroDanioNormal;
            }
            else if (roll == 20)
            {
                Debug.Log("¡Golpe devastador del caballero!");
                damage = habilidadCaballeroDanioCritico;
            }

            if (damage > 0)
            {
                EnemyController.Instance.PlayDamageAnimation();
            }

            ui.bossHealthBar.value -= damage;
        }

        else if (characterIndex == 1) // Mago
        {
            if (roll == 1)
            {
                Debug.Log("¡El mago se equivocó y dañó a todos los aliados!");
                for (int i = 0; i < ui.characters.Length; i++)
                {
                    ui.characters[i].GetComponent<CharacterStatus>()?.TakeDamage(habilidadMagoAutodaño);
                }
            }
            else if (roll <= 4)
            {
                Debug.Log("El hechizo de curación falló.");
            }
            else if (roll <= 19)
            {
                Debug.Log("El mago cura a todos los aliados.");
                for (int i = 0; i < ui.characters.Length; i++)
                {
                    ui.characters[i].GetComponent<CharacterStatus>()?.Heal(habilidadMagoCuracion);
                }
            }
            else if (roll == 20)
            {
                Debug.Log("Curación crítica del mago + absorción de vida.");
                for (int i = 0; i < ui.characters.Length; i++)
                {
                    ui.characters[i].GetComponent<CharacterStatus>()?.Heal(habilidadMagoCuracion);
                }

                EnemyController.Instance.PlayDamageAnimation();
                ui.bossHealthBar.value -= habilidadMagoRoboVida;
            }
        }

        else if (characterIndex == 2) // Arquero
        {
            if (roll == 1)
            {
                Debug.Log("El arquero se hiere con su propia flecha.");
                ui.characters[2].GetComponent<CharacterStatus>()?.TakeDamage(habilidadArqueroAutodanio);
            }
            else if (roll <= 4)
            {
                Debug.Log("La flecha de fuego falló.");
            }
            else if (roll <= 19)
            {
                Debug.Log("El arquero debilitó al enemigo: recibirá más daño el siguiente turno.");
                BattleManager.Instance.enemigoDebilitado = true;
            }
            else if (roll == 20)
            {
                Debug.Log("¡Flecha de fuego crítica! Debilitamiento + veneno.");
                BattleManager.Instance.enemigoDebilitado = true;
                BattleManager.Instance.turnosDeVeneno = 2;
            }
        }

        onSkillFinished?.Invoke();
    }
}

