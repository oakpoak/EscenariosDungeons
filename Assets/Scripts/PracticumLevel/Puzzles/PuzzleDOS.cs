using UnityEngine;

/// <summary>
/// Controla un puzzle de antorchas en el que el jugador
/// debe encender una cantidad determinada para abrir un cofre.
/// </summary>
public class PuzzleDOS : MonoBehaviour
{
    [Header("Configuración del puzzle")]
    [Tooltip("Número de antorchas que el jugador debe encender para completar el puzzle")]
    public int requiredCount = 7;

    [Tooltip("Referencia al cofre que se abrirá al completar el puzzle")]
    public GameObject chest;

    private int currentCount = 0;   // Contador de antorchas encendidas
    private bool isCompleted = false;

    /// <summary>
    /// Llamar desde cada antorcha al encenderse (por ejemplo, desde Encender.Interact()).
    /// </summary>
    public void AddTorch()
    {
        if (isCompleted)
            return;

        currentCount++;
        Debug.Log($"Antorchas encendidas: {currentCount}/{requiredCount}");

        if (currentCount >= requiredCount)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        isCompleted = true;
        Debug.Log("Puzzle DOS completado: abriendo cofre...");

        if (chest != null)
        {
            chest.transform.rotation = Quaternion.Euler(130f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
           
        }
    }
}
