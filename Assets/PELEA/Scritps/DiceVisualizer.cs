using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class DiceVisualizer : MonoBehaviour
{
    public static DiceVisualizer Instance;

    public Animator diceAnimator;
    public TextMeshProUGUI diceNumberText;
    public float animationDuration = 1.5f; // Cambia según tu animación

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayDiceRoll(int result)
    {
        StartCoroutine(PlayAndShowNumber(result));
    }

    private IEnumerator PlayAndShowNumber(int result)
    {
        // Oculta número
        diceNumberText.gameObject.SetActive(false);
        diceNumberText.text = "";

        // Reproduce animación
        diceAnimator.Play("Dice_UI_Animation", -1, 0f); // Asegura que se reinicie desde el inicio

        // Espera duración de la animación
        yield return new WaitForSeconds(animationDuration);

        // Muestra el número
        diceNumberText.text = result.ToString();
        diceNumberText.gameObject.SetActive(true);
    }
}
