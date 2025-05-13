#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [Header("Escena Addressable")]
    public SceneAsset Pelea;
    public float fadeDuration = 1f; // ← antes se llamaba "Time", causaba conflicto
    public Camera Cam;

    [Header("Fade UI")]
    public Image fadeImage;

    public GameManager GM;

    private string sceneName;
    private bool alreadyTriggered = false;

    private void Start()
    {
#if UNITY_EDITOR
        if (Pelea != null)
        {
            string path = AssetDatabase.GetAssetPath(Pelea);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
        }
#else
        sceneName = Pelea != null ? Pelea.name : "";
#endif

        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered && other.CompareTag("Player"))
        {
            alreadyTriggered = true;

            // Guardar ID del enemigo y posición del jugador
            EnemyMapID id = GetComponent<EnemyMapID>();
            if (id != null)
            {
                WorldState.enemigoActualID = id.enemyID;
            }

            WorldState.posicionJugador = other.transform.position;
            GM.Fight = true;

            StartCoroutine(TransitionRoutine());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        // 1. Pausar el tiempo
        Time.timeScale = 0f;

        // 2. Efecto de fade (en tiempo real)
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += UnityEngine.Time.unscaledDeltaTime;
            float a = Mathf.Clamp01(elapsed / fadeDuration);
            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }

        // 3. Cargar escena y esperar que termine
        if (!string.IsNullOrEmpty(sceneName))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
                yield return null;

            // 4. Reactivar el tiempo dentro de la nueva escena
            Time.timeScale = 1f;
        }
    }
}



