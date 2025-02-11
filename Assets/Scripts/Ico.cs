using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ico : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void ActivateColor()
    {
        spriteRenderer.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
    }

    public void DesactivateColor()
    {
        spriteRenderer.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 50 / 255f);
    }

    public void Transparent()
    {
        spriteRenderer.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
    }

}
