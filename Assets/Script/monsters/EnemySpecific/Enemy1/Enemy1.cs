public class Enemy1 : Entity
{
    public E1_MoveState moveState { get; private set; }
    public E1_IdleState idleState { get; private set; }
    public E1_DeadState deadState { get; private set; }
    public E1_DodgeState dodgeState { get; private set; }
    public E1_LookForPlayerrState lookForPlayerrState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set;  }
    public E1_RangeAttackState rangeAttackState { get; private set;  }
    public E1_StunState stunState { get; private set;  }

}
