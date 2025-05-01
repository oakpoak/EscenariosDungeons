using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Cameras")]
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera enemyAttackCamera;
    public CinemachineVirtualCamera characterAttackCamera;

    private bool isCameraLocked = false; // Evita cambios de cámara conflictivos

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
            Debug.Log($"Cámara bloqueada, no se puede cambiar a {targetCamera.name}");
            return;
        }

        Debug.Log($"Cambiando a la cámara: {targetCamera.name} por {duration} segundos.");
        isCameraLocked = true;
        ResetCameraPriorities();
        targetCamera.Priority = 10;

        // Desbloquea la cámara después de la duración especificada
        Invoke(nameof(UnlockCamera), duration);
    }

    private void UnlockCamera()
    {
        Debug.Log("Cámara desbloqueada");
        isCameraLocked = false;
    }

    private void ResetCameraPriorities()
    {
        defaultCamera.Priority = 0;
        enemyAttackCamera.Priority = 0;
        characterAttackCamera.Priority = 0;
    }
}