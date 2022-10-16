using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Sensor : MonoBehaviour
{
    private int m_ColCount = 0; // 충돌시 올라감

    private float m_DisableTimer;


    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()//바닥 센서에 닿았는지 여부 리턴
    {
        if (m_DisableTimer > 0)
        {
            return false;
        }
        return m_ColCount > 0;
    }

    public void OnTriggerEnter2D(Collider2D other)//다른 오브젝트간 충돌이 일어날때 처음 한번만 호출되는 함수.
    {
        m_ColCount++;
    }

    void OnTriggerExit2D(Collider2D other)//충돌에서 벋어날때 호출
    {
        m_ColCount--;
    }


    void Update()
    {
        m_DisableTimer -= Time.deltaTime; //전프레임
    }


    public void Disable(float duration) //공중에서 지속시간
    {
        m_DisableTimer = duration;
    }
}
