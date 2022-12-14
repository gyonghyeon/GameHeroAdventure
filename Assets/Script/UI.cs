using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;
    private bool isDie = false;
    public float maxHp;
    public float curHp;
    float imsi = 100;
    //hp는 최소 0 최대 1
    //현재체력 / 최대체력 으로 update 로 지속적으로 갱신
    void Start()
    {
        
        hpbar.value = (float)curHp / maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHp(imsi);
    }

    private void HandleHp(float imsi)
    {//hp바 변화가 부드럽게 하기 위해 선형보간 이용
     //Math.Lerp(float A, float B, float t) -> A와 B사이의 t만큼 부드럽게 값 변환
        hpbar.value = Mathf.Lerp(hpbar.value, imsi, Time.deltaTime * 10);
    }

    public void Obstacled()
    {
        if (curHp > 0)
        {
            curHp -= 20;
        }
        else
        {
            curHp = 0;
            setDie();
        }
        imsi = (float)curHp / (float)maxHp;
    }
    public void HealHp()
    {
        if(curHp == 100)
        {
            curHp = 100;
        }
        else if (curHp >= 0)
        {
            if(curHp > 80)
            {
                curHp = 100;
            }
            else
            {
                curHp += 20;
            }
            
        }
        else
        {
            curHp = 0;
            
        }
        imsi = (float)curHp / (float)maxHp;
    }
    public void HpBar_hpReset()
    {
        curHp = 100;
        imsi = (float)curHp / (float)maxHp;
    }
    public void HpBar_hpsetzero()
    {
        curHp = 0;
        imsi = (float)curHp / (float)maxHp;
    }
    private void setDie()
    {
        isDie = true;
    }
    public void SetDieFalse()
    {
        isDie = false;
    }
    public bool IsDie()
    {
        return isDie;
    }
}

