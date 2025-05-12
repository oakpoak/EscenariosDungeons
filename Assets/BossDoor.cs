using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Puerta del jefe que, al tener la llave, realiza un fade-out y carga la escena de pelea.
/// </summary>
public class BossDoor : ItemInteractive
{
    [Header("Escena Addressable")]
    public SceneAsset Pelea;      // Tu SceneAsset de la escena "Pelea"
    public float fadeTime = 1f;   // Duración del fade-out en segundos
    public Camera Cam;            // La cámara principal

    [Header("Fade UI")]
    public Image fadeImage;       // Image negra que cubre toda la pantalla

    public GameManager GameManager;
    public BoxCollider Tiger;     // Collider de la puerta para deshabilitar tras iniciar transición

    private string sceneName;
    private bool isTransitioning = false;

    void Start()
    {
        // Obtener el nombre de la escena desde el SceneAsset
#if UNITY_EDITOR
        if (Pelea != null)
        {
            string path = AssetDatabase.GetAssetPath(Pelea);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
        }
#else
        sceneName = Pelea != null ? Pelea.name : "";
#endif

        // Inicializar la imagen completamente transparente
        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0);
    }

    public override void Interact()
    {
        if (isTransitioning)
            return;

        if (GameManager.llaveboss)
        {
            isTransitioning = true;
            // Deshabilitar el collider para no volver a interactuar
            if (Tiger != null)
                Tiger.enabled = false;

            StartCoroutine(TransitionRoutine());
        }
        else
        {
            Debug.Log("Necesitas la llave del jefe para entrar.");
        }
    }

    private IEnumerator TransitionRoutine()
    {
        // 1. Congelar el juego
        Time.timeScale = 0f;

        // 2. Fade-out a negro en 'fadeTime' segundos usando unscaledDeltaTime
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.unscaledDeltaTime;
            float a = Mathf.Clamp01(elapsed / fadeTime);
            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }

        // 3. Restaurar timeScale y cargar la escena
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadSceneAsync(sceneName);
    }
}
