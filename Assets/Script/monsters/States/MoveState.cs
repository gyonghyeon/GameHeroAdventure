using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;


    public MoveState(Entity entity, FinalStateMachine stateMachine, string animBoolName
        , D_MoveState stateData)
        : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);

        isDetectingLedge = entity.checkLedge();
        isDetectingWall = entity.checkWall();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        isDetectingLedge = entity.checkLedge();
        isDetectingWall = entity.checkWall();
    }
   
}
