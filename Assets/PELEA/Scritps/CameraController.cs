using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Cameras")]
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera enemyAttackCamera;
    public CinemachineVirtualCamera characterAttackCamera;

    private bool isCameraLocked = false; // Evita cambios de c�mara conflictivos

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RequestCameraChange(CinemachineVirtualCamera targetCamera, float duration)
    {
        if (isCameraLocked)
        {
            Debug.Log($"C�mara bloqueada, no se puede cambiar a {targetCamera.name}");
            return;
        }

        Debug.Log($"Cambiando a la c�mara: {targetCamera.name} por {duration} segundos.");
        isCameraLocked = true;
        ResetCameraPriorities();
        targetCamera.Priority = 10;

        // Desbloquea la c�mara despu�s de la duraci�n especificada
        Invoke(nameof(UnlockCamera), duration);
    }

    private void UnlockCamera()
    {
        Debug.Log("C�mara desbloqueada");
        isCameraLocked = false;
    }

    private void ResetCameraPriorities()
    {
        defaultCamera.Priority = 0;
        enemyAttackCamera.Priority = 0;
        characterAttackCamera.Priority = 0;
    }
}