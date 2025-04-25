using UnityEngine;

// Esto hace que OnDrawGizmos se ejecute siempre, incluso fuera de Play
[ExecuteAlways]
public class SphereGizmo : MonoBehaviour
{
    [Header("Offset en cada eje")]
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;

    [Header("Escala (X=ancho, Y=alto, Z=profundidad)")]
    public Vector3 scale = new Vector3(1f, 2f, 1f);

    [Header("Color (alpha controla opacidad)")]
    [ColorUsage(true, true)]
    public Color gizmoColor = new Color(1f, 1f, 0f, 0.3f);

    void OnDrawGizmos()
    {
        // Construye el offset desde los tres floats
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        Vector3 worldPos = transform.TransformPoint(offset);

        Matrix4x4 mat = Matrix4x4.TRS(worldPos, transform.rotation, scale);
        var oldMat = Gizmos.matrix;
        Gizmos.matrix = mat;

        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(Vector3.zero, 1f);

        Gizmos.matrix = oldMat;
    }
}