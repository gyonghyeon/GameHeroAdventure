using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : State
{
    protected D_IdelState stateData;
    protected bool flipAfteriIdle;
    protected bool isIdlteTimeOver;

    protected float idleTime;
    public IdelState(Entity entity, FinalStateMachine stateMachine, string animBoolName, D_IdelState statData)
        :base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);
        isIdlteTimeOver = false;
        setRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfteriIdle)
        {
            entity.Filp();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Time.time 게임시작후 흐른 시간
        if(Time.time >= startTime + idleTime)
        {
            isIdlteTimeOver = true;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public void setFlipAfterIdle(bool flip)
    {

        flipAfteriIdle = flip;
    }
    private void setRandomIdleTime() //최소와 최대범위에서 랜덤 시간
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
