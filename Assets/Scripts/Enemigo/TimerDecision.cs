using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDecision : AIDecision
{
    public float maxTime;
    public override bool Decide()
    {
        return _brain.TimeInThisState >= maxTime;
    }
    
}
