using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStateMachine
{
    public State currentStatae{get; private set; }

    public void Initalize(State startingState)
    {
        currentStatae = startingState;
        currentStatae.Enter();
    }

    public void ChangeState(State newState)
    {
        currentStatae.Exit();
        currentStatae= newState;
        currentStatae.Enter();
    }
}
