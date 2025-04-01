using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FollowTargetAction : AIAction
{
    public float speed;
    // Start is called before the first frame update
    public override void PerformAction()
    {
        if(_brain.Target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _brain.Target.position, step);
            transform.LookAt(_brain.Target.position);
        }
    }
}
