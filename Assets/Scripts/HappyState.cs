using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyState : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}
