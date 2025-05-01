using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject[] characters;
    public Transform[] positions;
    public int selectedIndex = 0;

    private Animator[] animators;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAttack(selectedIndex);
        }
    }

    void UpdateCharacterPositions()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            int positionIndex = (i - selectedIndex + 2 + characters.Length) % characters.Length;
            characters[i].transform.position = positions[positionIndex].position;
        }
    }

    public void PerformAttack(int characterIndex)
    {
        Debug.Log($"Iniciando ataque del personaje {characterIndex}");
        float attackDuration = animators[characterIndex].GetCurrentAnimatorStateInfo(0).length;

        // Cambiar a la cámara de ataque del personaje
        CameraController.Instance.RequestCameraChange(CameraController.Instance.characterAttackCamera, attackDuration);

        // Reproducir la animación de ataque
        animators[characterIndex].SetTrigger("AttackTrigger");
        Debug.Log($"Animación de ataque activada para el personaje {characterIndex}");

        StartCoroutine(HandleAttackSequence(characterIndex));
    }

    System.Collections.IEnumerator HandleAttackSequence(int characterIndex)
    {
        Debug.Log($"Comenzando secuencia de ataque para el personaje {characterIndex}");
        float attackDuration = animators[characterIndex].GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(attackDuration);

        Debug.Log($"Finalizando secuencia de ataque para el personaje {characterIndex}");
        CameraController.Instance.RequestCameraChange(CameraController.Instance.defaultCamera, 0.1f);
        EnemyController.Instance.PlayDamageAnimation();
    }
}