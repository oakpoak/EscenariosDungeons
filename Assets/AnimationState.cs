// AnimationState.cs
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class AnimationState : MonoBehaviour
{ 
    
    [Header("Nombres de parámetros (bool)")]
    public string walkParam = "Walk";     
    public string runParam = "Run";
}



