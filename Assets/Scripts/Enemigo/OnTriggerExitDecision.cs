using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerExitDecision : AIDecision
{
    private bool hasPlayerExit;
   

    public override void Initialization()
    {
        base.Initialization();
        hasPlayerExit = false;
    }


    public override bool Decide()
    {
        return hasPlayerExit;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _brain.Target != null)
        {
            _brain.Target = null;
            hasPlayerExit = true;

            //GetComponentInChildren<Animator>().SetBool("IsFollowing", false);
        }


    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        hasPlayerExit = false;

    }
}
