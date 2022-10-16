using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FinalStateMachine stateMachine;
    protected Entity entity;
    protected Core core;
    protected float startTime;
    
    public float startTime { get; protected set; }

    protected string animBoolName;

    public State(Entity entity, FinalStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core = entity.Core;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
    }
    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}
