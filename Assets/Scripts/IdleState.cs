using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public HappyState happyState;
    public bool victimIsKind;

    public AngerState angerState;
    public bool victimIsBad;

    public override State RunCurrentState()
    {
        if (victimIsKind)
        {

        }
        if (victimIsBad)
        {

        }
        return this;
    }
}
