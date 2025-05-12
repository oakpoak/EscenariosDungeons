using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TercerCuarto : MonoBehaviour
{
    [Header("Configuración del puzzle")]
    public Transform[] cameraPositions; // 5 transforms vacíos en el inspector
    public float holdDuration = 2f;    // Duración en segundos que la cámara se mantiene en cada posición

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool initialized = false;
    private Camera mainCamera;

    public GameManager game;

    public GameObject Puerta;

    [Header("Objetos del puzzle")]
    public GameObject[] puzzleObjects;  // 4 objetos con componente Encender

    public GameObject Ganar;

    private int currentIndex = 0;       // Siguiente objeto esperado en la secuencia

    void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (initialized)
        {
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!initialized && other.CompareTag("Player"))
        {
            initialized = true;
            game.puzzle = true;

            if (game.puzzle == true)
            {
                // Guardar posición y rotación originales
                originalPosition = mainCamera.transform.position;
                originalRotation = mainCamera.transform.rotation;
            }

            // Iniciar secuencia de cámara
            StartCoroutine(MoveCameraSequence());
        }
    }

    private IEnumerator MoveCameraSequence()
    {
        foreach (Transform target in cameraPositions)
        {
            if (target == null) continue;
            // Mover cámara instantáneamente
            mainCamera.transform.position = target.position;
            mainCamera.transform.rotation = target.rotation;
            // Esperar tiempo de hold
            yield return new WaitForSeconds(holdDuration);
        }

        // Restaurar posición original
        mainCamera.transform.position = originalPosition;
        mainCamera.transform.rotation = originalRotation;
        game.puzzle = false;

        // Reiniciar contador del puzzle
        currentIndex = 0;
    }

    /// <summary>
    /// Llamar desde Encender.Interact() para validar la interacción en orden.
    /// </summary>
    public void OnPuzzleObjectInteracted(GameObject obj)
    {
        if (!initialized) return;

        // Si coincide con el objeto esperado en la secuencia
        if (obj == puzzleObjects[currentIndex])
        {
            // El script Encender ya encendió la antorcha y la luz
            currentIndex++;
            if (currentIndex >= puzzleObjects.Length)
            {

                game.puzzle = true;

                if (game.puzzle == true)
                {
                    // Guardar posición y rotación originales
                    originalPosition = mainCamera.transform.position;
                    originalRotation = mainCamera.transform.rotation;
                }

                StartCoroutine(Win());

                // Aquí lógica de éxito...
            }
        }
        else
        {

            // Apagar todas las antorchas y luces
            foreach (GameObject go in puzzleObjects)
            {
                var enc = go.GetComponent<Encender>();
                if (enc != null)
                {
                    enc.Antorcha.SetActive(false);
                    enc.luz.enabled = false;
                }
            }

            // Reiniciar el puzzle y la toma de cámaras
            currentIndex = 0;
            game.puzzle = true;
            StartCoroutine(MoveCameraSequence());
        }
    }
    private IEnumerator Win()
    {
        mainCamera.transform.position = Ganar.transform.position;
        mainCamera.transform.rotation  = Ganar.transform.rotation;
        // 1) Espera el primer segundo
        yield return new WaitForSeconds(1f);

        // Aquí podrías, si quisieras, poner algún efecto o feedback

        Puerta.SetActive(false);
        // 2) Espera el segundo segundo
        yield return new WaitForSeconds(2f);

        // Restaurar posición original
        mainCamera.transform.position = originalPosition;
        mainCamera.transform.rotation = originalRotation;
        game.puzzle = false;

    }
}

