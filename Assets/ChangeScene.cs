#if UNITY_EDITOR
using UnityEditor;  // para extraer el nombre de tu SceneAsset en el Editor
#endif
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [Header("Escena Addressable")]
    public SceneAsset Pelea;      // tu SceneAsset de la escena “Pelea”
    public float Time;            // duración del fade‐out en segundos (tiempo real)
    public Camera Cam;            // tu cámara, por si quieres usarla luego

    [Header("Fade UI")]
    public Image fadeImage;       // arrastra aquí la Image negra que cubra toda la pantalla

    public GameManager GM;

    private string sceneName;

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
        // Inicializar la imagen completamente transparente
        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GM.Fight = true;
            StartCoroutine(TransitionRoutine());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        // 1. Congelar el juego
        UnityEngine.Time.timeScale = 0f;

        // 2. Fade‐out a negro en “Time” segundos (usa unscaledDeltaTime)
        float elapsed = 0f;
        while (elapsed < Time)
        {
            elapsed += UnityEngine.Time.unscaledDeltaTime;
            float a = Mathf.Clamp01(elapsed / Time);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }

        // 3. Restaurar timeScale y cargar la escena
        UnityEngine.Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadSceneAsync(sceneName);
    }
}

