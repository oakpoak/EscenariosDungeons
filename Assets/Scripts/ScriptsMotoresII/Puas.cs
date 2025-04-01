using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puas : MonoBehaviour
{
    public Transform targetTransform; // Transform donde aparecerá el jugador
    public float pauseDuration = 3f; // Tiempo que el jugador estará desactivado
    public float blinkDuration = 2f; // Tiempo que el jugador estará parpadeando
    public float blinkInterval = 0.2f; // Intervalo entre parpadeos

    private Renderer playerRenderer; // Renderer del jugador
    private GameObject player; // Referencia al jugador
    private Color originalColor; // Color original del material

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra es el jugador
        if (other.CompareTag("Player")) // Cambia "Player" si usas otra etiqueta
        {
            player = other.gameObject;
            playerRenderer = player.GetComponent<Renderer>();
            if (playerRenderer != null)
            {
                originalColor = playerRenderer.material.color;
                StartCoroutine(RespawnRoutine());
            }
            else
            {
                Debug.LogError("El jugador no tiene un Renderer asignado.");
            }
        }
    }

    private IEnumerator RespawnRoutine()
    {
        // 1. Desactivar el jugador
        player.SetActive(false);

        // 2. Pausa
        yield return new WaitForSeconds(pauseDuration);

        // 3. Mover el jugador y reactivarlo
        player.transform.position = targetTransform.position;
        player.SetActive(true);

        // 4. Iniciar parpadeo
        yield return StartCoroutine(BlinkPlayer());

        // 5. Asegurarse de que el jugador esté completamente visible
        SetPlayerOpacity(1f);
    }

    private IEnumerator BlinkPlayer()
    {
        float elapsed = 0f;
        while (elapsed < blinkDuration)
        {
            // Alternar entre transparente y visible
            SetPlayerOpacity(0.5f); // Semitransparente
            yield return new WaitForSeconds(blinkInterval);

            SetPlayerOpacity(1f); // Totalmente visible
            yield return new WaitForSeconds(blinkInterval);

            elapsed += blinkInterval * 2;
        }
    }

    private void SetPlayerOpacity(float alpha)
    {
        if (playerRenderer != null)
        {
            Color color = originalColor;
            color.a = alpha; // Cambiar la opacidad
            playerRenderer.material.color = color;
        }
    }
}
