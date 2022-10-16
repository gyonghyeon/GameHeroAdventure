using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FinalStateMachine stateMachine;
    public D_Entity entityData;
    public Animator anim { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirecteion { get; private set; }
    public Core core { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;

    private float currentHealth;
    private float currentStunResistance; //기절 저항
    private float lastDamageTime;

    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;
    public virtual void Start()
    {
        core = GetComponent<Core>();

        currentHealth = entityData

        anim = GetComponent<Animator>();

        stateMachine = new FinalStateMachine();
    }
    public virtual void Update()
    {
        stateMachine.currentStatae.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentStatae.PhysicUpdate();
    }
    public virtual  void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }
    public virtual bool checkWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool checkLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual void Filp()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f); //y 좌표로 180도 회전
    }
}
