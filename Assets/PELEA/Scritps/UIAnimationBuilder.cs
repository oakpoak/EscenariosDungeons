using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections.Generic;

public class UIAnimationBuilder : MonoBehaviour
{
    [Header("Configura aquí")]
    public Image targetImage;
    public Sprite[] frames;
    public float frameRate = 12f;

    [ContextMenu("Crear animación UI")]
    public void CrearAnimacion()
    {
#if UNITY_EDITOR
        if (targetImage == null || frames.Length == 0)
        {
            Debug.LogWarning("Falta asignar targetImage o sprites.");
            return;
        }

        AnimationClip clip = new AnimationClip();
        clip.frameRate = frameRate;

        EditorCurveBinding binding = new EditorCurveBinding();
        binding.type = typeof(Image);
        binding.path = "";
        binding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[frames.Length];

        for (int i = 0; i < frames.Length; i++)
        {
            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = i / frameRate;
            keyFrames[i].value = frames[i];
        }

        AnimationUtility.SetObjectReferenceCurve(clip, binding, keyFrames);

        string path = "Assets/Dice_UI_Animation.anim";
        AssetDatabase.CreateAsset(clip, path);
        AssetDatabase.SaveAssets();

        Debug.Log("Animación UI creada: " + path);
#endif
    }
}
