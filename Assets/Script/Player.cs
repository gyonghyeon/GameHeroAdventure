using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;


    public SoundManager soundManager;
    public GameManager gameManager;
    private Ground_Sensor m_groundSensor;//센서스크립트 가져오기'
    public UI hpBar;
    public GameObject attack1;
    public float attackPower;
    public float m_dodgeForce;
    public float m_MaxSpeed;
    public float m_JumpPower;
    public float attackCoolDown;
    public float invincibilityTime;
    protected int JumpCount = 0;
    public int MaxJumpCount = 1;
    private bool m_dodging = false;
    private bool m_attacking1 = false;
    private bool m_moving = false;
    private bool m_damaged = false;
    private float m_disableMovementTimer = 0.0f;
    private int m_facingDirection = 1;
    private bool m_grounded = true; //땅에 닿았는지 여부
    public Vector3 startPosition;

    public bool isDie = false;

    public PlayerAudio PA;

    private void Awake()
    {
        
    }
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //다른 오브젝트의 컴포넌스를 사용할때 transform을 이용해 찾는다
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Ground_Sensor>();
        startPosition = transform.position;

    }

    void Update()
    {
        if (isDie == false && gameManager.isGameStart())
        {
            Move();
            MoveSfx();
            if (!m_damaged)
            {
                Jump();
            }

            Dodge();
           
        }
        GroundedChecked();
    }
    void FixedUpdate()
    {
       
    }

    void LandingSound()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Layer1.Landing"))
        {

            soundManager.StartLandingSound();
        }
    }
    void Move()
    {
        //Time.deltaTime = 전 프레임이 완료되기까지 시간
        m_disableMovementTimer -= Time.deltaTime;
        float inputX = 0.0f;
        if (m_disableMovementTimer < 0.0f)
        {
            inputX = Input.GetAxis("Horizontal");//연속적인 입력값
        }

        float inputRaw = Input.GetAxisRaw("Horizontal");//불연속적인 0,1,-1 입력값

        //현재 캐릭터 움직임 입력이 0 보다 크고 캐릭터가 바라보는 방향과 움직임 입력 방향이 같은지 체크
        //Sing =부호 반환 , Epsilon = 0 에 가까운 float형

        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
        {
            
            m_moving = true;
        }
        else
        {
            m_moving = false;
        }
        //바라보는 방향 선정
        FlipPlayer(inputRaw);

        //캐릭터가 멈출때 속도 줄이기 
        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;

        //set movement
        //1.동일속도 velocity
        if (!m_dodging)
        {
            rigidbody2d.velocity = new Vector2(inputX * m_MaxSpeed * SlowDownSpeed, rigidbody2d.velocity.y);

        }

        //리지드 바디 y축 속도를 animator 파라미터변수인 AirSpeedY에 넣어 0일때 낙하모션으로 변하게함
        animator.SetFloat("AirSpeedY", rigidbody2d.velocity.y);


    }
    void GroundedChecked()//땅에 닿았는지 여부 체크
    {
        if (!m_grounded && m_groundSensor.State())
        {
            LandingSound();
            Debug.Log("바닥");
            
            m_grounded = true;
            animator.SetBool("Grounded", m_grounded);
            
        }
        //공중에서 시작할때
        if (m_grounded && !m_groundSensor.State())
        {
            Debug.Log("공중");
            m_grounded = false;
            animator.SetBool("Grounded", m_grounded);
        }
    }
    void Jump()
    {
        if (m_grounded == true)
        {
            animator.SetInteger("JumpCount", 0);
        }
        m_disableMovementTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && m_grounded && m_disableMovementTimer < 0.0f
            || Input.GetButtonDown("Jump") && animator.GetInteger("JumpCount") < MaxJumpCount && m_disableMovementTimer < 0.0f)
        {
            if (animator.GetInteger("JumpCount") < MaxJumpCount)
            {
                PA.PlaySound("JUMP");
                animator.SetInteger("JumpCount", animator.GetInteger("JumpCount") + 1);

                animator.SetTrigger("Jump");
                m_grounded = false;
                animator.SetBool("Grounded", m_grounded);
                rigidbody2d.AddForce(Vector3.up * m_JumpPower, ForceMode2D.Impulse);
                m_groundSensor.Disable(0);
            }
            else if (animator.GetInteger("JumpCount") >= MaxJumpCount)
            {

            }
            else if (m_grounded == true)
            {
                animator.SetInteger("JumpCount", 0);
            }

            /*
            JumpCount++;        
            if(true)//바닥에 닿는 모션 나올때
            {
                JumpCount = 0;
            }
            */
        }
        //Run
        else if (m_moving)
            animator.SetInteger("AnimState", 1);

        //Idle
        else
            animator.SetInteger("AnimState", 0);
    }

    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && m_grounded && !m_dodging)
        {
            PA.PlaySound("DODGE");
            m_dodging = true;
            animator.SetTrigger("Dodge");
            rigidbody2d.velocity = (new Vector2(m_facingDirection * m_dodgeForce, rigidbody2d.velocity.y));

            Invoke("OffDodging", 1.2f);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !m_dodging && !m_attacking1)
        {
            PA.PlaySound("ATTACK");
            m_attacking1 = true;
            animator.SetTrigger("Attack1");
            GameObject attack = Instantiate(attack1, transform.position, transform.rotation);
            if (m_facingDirection < 0)
            {
                attack.GetComponent<SpriteRenderer>().flipX = true;
            }
            attack.GetComponent<Rigidbody2D>().AddForce(new Vector2(m_facingDirection, 0) * attackPower);
            Destroy(attack, 2.5f);
            Invoke("OffAttacking1", attackCoolDown);

        }
    }

    void OffAttacking1()
    {
        m_attacking1 = false;

    }
    void OffDodging()
    {
        m_dodging = false;
    }
    void FlipPlayer(float inputRaw)
    {
        if (inputRaw > 0)
        {
            spriteRenderer.flipX = false;
            m_facingDirection = 1;
        }
        else if (inputRaw < 0)
        {
            spriteRenderer.flipX = true;
            m_facingDirection = -1;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌");
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Monster")
        {
            PA.PlaySound("DAMAGED");
            m_damaged = true;
            Invoke("CanJump", 0.7f);
            hpBar.Obstacled();
            OnDameged(collision.transform.position);
            if (hpBar.IsDie())
            {
                Die();
            }
        }
    }
    public void Player_DieTrigger()
    {
        animator.SetTrigger("Die");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Heal")
        {
            PA.PlaySound("HEAL");
            hpBar.HealHp();
            //게임 리셋시 살릴수 있도록 비활성화로 변경
            //      Destroy(collision.gameObject, 0.1f);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "wall")
        {

            Die();
            hpBar.HpBar_hpsetzero();
        }
        if (collision.gameObject.tag == "box")
        {
            gameManager.GameClear();
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
            soundManager.StopMusicSound();
        }
    }
    void Die()
    {
        soundManager.StopMusicSound();
        PA.PlaySound("DIE");
        if (isDie == false)
        {
            gameManager.SetActiveEndPanal();
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
            isDie = true;
            Debug.Log("사망");

            animator.SetTrigger("Die");
        }


    }


    void OnDameged(Vector2 targetPositon)
    {
        gameObject.layer = 12;
        PA.PlaySound("DAMAGED");
        //피격시 투명해지게
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //피격시 피격한 방향과 반대방향으로 밀려나가기 안됨 위로만됨
        int dirc = spriteRenderer.flipX ? 1 : -1;
        rigidbody2d.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);

        //Animation
        // animator.SetTrigger("DoDamaged");


        //함수로 무적시간 결정 Invoke(초후 나올 메서드, 지속시간)
        Invoke("OffDamaged", invincibilityTime);

    }
    public void Restart()
    {
        rigidbody2d.AddForce(new Vector2(0, 1) * 3, ForceMode2D.Impulse);
        rigidbody2d.gravityScale = 1.6f;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    void CanJump()
    {
        m_damaged = false;
    }
    void OffDamaged()
    {
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    public void ResetPlayerPosition()
    {
        transform.position = startPosition;
    }

    public void PushMainBtn()
    {
        rigidbody2d.position = startPosition;
        animator.SetTrigger("PushMainBtn");
        rigidbody2d.constraints = RigidbodyConstraints2D.None;
    }

    float moveX;
    bool isRunnig = false;
    void MoveSfx()
    {
        moveX = Input.GetAxis("Horizontal");
        
        if(rigidbody2d.velocity.x != 0 && rigidbody2d.velocity.y == 0)
        {
            isRunnig = true;
        }
        else
        {
            isRunnig = false;
        }
        if (isRunnig)
        {
            if (!PA.audioSource.isPlaying)
            {
                PA.PlaySound("RUN");
            }
        }

    }
}
