using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpSlime : MonoBehaviour
{
    public GameObject This;


    public float JumpPower;
    public float JumpCoolDown;
    protected Rigidbody2D JumpSlimeRB2D;
    private bool isDisableBefore;
    
    public void Start()
    {
        JumpSlimeRB2D = GetComponent<Rigidbody2D>();
        StartCoroutine("Jump");
        isDisableBefore = false;
        
    }
    //StopCoroutine(""); 으로 멈추기 가능
    IEnumerator Jump()
    {
        JumpSlimeRB2D.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(JumpCoolDown);
        StartCoroutine(Jump());
    }
    private void OnEnable()
    {
        if (isDisableBefore)
        {
            Start();
        }
        

    }
    public void OnDisable()
    {
        isDisableBefore = true;   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //플레이어와 충돌시
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            
            ScoreManager.setScore(10);
            StopCoroutine("Jump");
            isDisableBefore = true;
            This.SetActive(false);
        }
    }
    
}
