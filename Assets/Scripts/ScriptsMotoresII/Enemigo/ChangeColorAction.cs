using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorAction : AIAction
{
    public Color alertColor;
    private Color _originalColor;
    private MeshRenderer _meshRenderer;
    public override void PerformAction()
    { }

    protected override void Initialization()
    {
        base.Initialization();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        _originalColor = _meshRenderer.material.color;
        _meshRenderer.material.color = alertColor;

    }

    public override void OnExitState()
    {
        base.OnExitState(); 
        _meshRenderer.material.color= _originalColor;
    }
}
