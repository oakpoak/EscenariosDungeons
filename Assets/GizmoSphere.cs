// GizmoSphere.cs
using UnityEngine;

// Esto hace que OnDrawGizmos se ejecute siempre, incluso fuera de Play
[ExecuteAlways]
public class GizmoSphere : MonoBehaviour
{
    [Header("Offset en cada eje")]
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;

    [Header("Radio de detección")]
    [Min(0.01f)]
    public float radius = 1f;

    [Header("Color (alpha controla opacidad)")]
    [ColorUsage(true, true)]
    public Color gizmoColor = new Color(1f, 1f, 0f, 0.3f);

    void OnDrawGizmos()
    {
        // Calcula posición mundial del centro
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        Vector3 worldPos = transform.TransformPoint(offset);

        // Monta la matriz de escala uniforme basada en el radio
        Matrix4x4 mat = Matrix4x4.TRS(worldPos, transform.rotation, Vector3.one * radius);
        var oldMat = Gizmos.matrix;
        Gizmos.matrix = mat;

        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(Vector3.zero, 1f);

        Gizmos.matrix = oldMat;
    }
}
