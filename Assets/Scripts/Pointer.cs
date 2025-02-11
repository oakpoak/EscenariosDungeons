using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void ActivateColor(float r, float g, float b, float a)
    {
        spriteRenderer.color = new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public void DesactivateColor(float r, float g, float b)
    {
        spriteRenderer.color = new Color(r / 255f, g / 255f, b / 255f, 0 / 255f);
    }
}
