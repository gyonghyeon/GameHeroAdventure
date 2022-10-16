using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmera : MonoBehaviour
{
    public Transform PlayerTrans;
    public float speed;
    Vector3 startPosition = new Vector3(0,0,-1);
    Vector3 practicePosition = new Vector3(0, -40, -1);
   

    public void Move(float timedeltaTime, float stage)
    {
    //    Vector3 curVector = new Vector3(transform.position.x, transform.position.y+40*stage, transform.position.z);
        transform.Translate(Vector3.right * speed * timedeltaTime);
    }
    public void SetCamStartPosition(int stagenum) //stage 별로 카메라 시작지점 설정 0부터 40씩 y값 상승
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 40*(stagenum-1), -1);
    }
    public void SetCamPracticePosition()
    {
        transform.position = practicePosition;
    }
    public void ResetCamPositon()
    {
        transform.position = startPosition;
    }
    public void SetCamPlayerPosition()
    {
        transform.position =  new Vector3(PlayerTrans.position.x,transform.position.y,transform.position.z);

}
}
