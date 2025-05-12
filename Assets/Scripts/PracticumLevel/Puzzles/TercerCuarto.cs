using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TercerCuarto : MonoBehaviour
{
    [Header("Configuraci�n del puzzle")]
    public Transform[] cameraPositions; // 5 transforms vac�os en el inspector
    public float holdDuration = 2f;    // Duraci�n en segundos que la c�mara se mantiene en cada posici�n

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
                // Guardar posici�n y rotaci�n originales
                originalPosition = mainCamera.transform.position;
                originalRotation = mainCamera.transform.rotation;
            }

            // Iniciar secuencia de c�mara
            StartCoroutine(MoveCameraSequence());
        }
    }

    private IEnumerator MoveCameraSequence()
    {
        foreach (Transform target in cameraPositions)
        {
            if (target == null) continue;
            // Mover c�mara instant�neamente
            mainCamera.transform.position = target.position;
            mainCamera.transform.rotation = target.rotation;
            // Esperar tiempo de hold
            yield return new WaitForSeconds(holdDuration);
        }

        // Restaurar posici�n original
        mainCamera.transform.position = originalPosition;
        mainCamera.transform.rotation = originalRotation;
        game.puzzle = false;

        // Reiniciar contador del puzzle
        currentIndex = 0;
    }

    /// <summary>
    /// Llamar desde Encender.Interact() para validar la interacci�n en orden.
    /// </summary>
    public void OnPuzzleObjectInteracted(GameObject obj)
    {
        if (!initialized) return;

        // Si coincide con el objeto esperado en la secuencia
        if (obj == puzzleObjects[currentIndex])
        {
            // El script Encender ya encendi� la antorcha y la luz
            currentIndex++;
            if (currentIndex >= puzzleObjects.Length)
            {

                game.puzzle = true;

                if (game.puzzle == true)
                {
                    // Guardar posici�n y rotaci�n originales
                    originalPosition = mainCamera.transform.position;
                    originalRotation = mainCamera.transform.rotation;
                }

                StartCoroutine(Win());

                // Aqu� l�gica de �xito...
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

            // Reiniciar el puzzle y la toma de c�maras
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

        // Aqu� podr�as, si quisieras, poner alg�n efecto o feedback

        Puerta.SetActive(false);
        // 2) Espera el segundo segundo
        yield return new WaitForSeconds(2f);

        // Restaurar posici�n original
        mainCamera.transform.position = originalPosition;
        mainCamera.transform.rotation = originalRotation;
        game.puzzle = false;

    }
}

