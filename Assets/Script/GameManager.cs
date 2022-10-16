using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    protected int stage = 0;
    public GameObject menuSet;
    public GameObject ingameSet;
    public GameObject diePanel;
    public GameObject clearPanel;
    public  TextMeshProUGUI scoreText;
    public TextMeshProUGUI[] endPanelScoreText;
    public ScoreManager scManager;
    
    public Carmera cam;

    public Player player;
    public Button[] btnStart;
    public Button btnPause;
    public Button[] btnGoMain;
    public Button btnPractice;
    public bool isStartGame = false;
    public bool isPaused = false;
    float timedeltaTime;
    public Stage[] stages = { };
    
  //  public GameObject[] monsters = { };
  //  public GameObject[] apple = { };
    protected int stage_monsterCount;
    void Start()
    {
       
        
        btnStart[0].onClick.AddListener(() => BtnStartClick(0));
        btnStart[1].onClick.AddListener(() => BtnStartClick(1));


     //   monsters = GameObject.FindGameObjectsWithTag("Monster");
     //   apple = GameObject.FindGameObjectsWithTag("Heal");
        btnPractice.onClick.AddListener(BtnPracticeClick);      
        
        btnPause.onClick.AddListener(BtnPauseClick);
        for(int i=0; i < btnGoMain.Length; i++)
        {
            btnGoMain[i].onClick.AddListener(BtnMainClick);
        }
        
    }
    void Update()
    {
        timedeltaTime = Time.deltaTime;
        if(isStartGame && !isPaused && !player.isDie )
        {
            Scoresynchronization();
            if (stage == 1)
            {
                cam.Move(timedeltaTime,stage);
            }
            else if(stage == -1)
            {
                cam.SetCamPlayerPosition();
            }
            else if(stage == 2)
            {
                cam.Move(timedeltaTime,stage);
            }
            
        }
        if (Input.GetButtonDown("Cancel"))
        {
            OnPauseClick();
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
                ingameSet.SetActive(true);
            }
            else 
            {
                menuSet.SetActive(true);
                ingameSet.SetActive(false);
            }
        }
        
    }
    public bool isGameStart()
    {
        return isStartGame;
        
    }
    public void BtnPracticeClick()
    {
        stage = -1;
        player.hpBar.SetDieFalse();
        player.Restart();
        player.isDie = false;
        cam.SetCamPracticePosition();
        isStartGame = true;
        if (isPaused)
            OnPauseClick();
        player.transform.position = player.startPosition + new Vector3(0, -40, 0);
    }
  /*  public void MonsteSetActiveTrue()
    {
        
        for(int i =1;i<monsters.Length; i++)
        {
            monsters[i].SetActive(true);
           
        }
    }*/
  /*  public void AppleSetActiveTrue()
    {
        for(int i = 0; i < apple.Length; i++)
        {
            apple[i].SetActive(true);
        }
    }*/
    public void BtnStartClick(int stagenum)
    {
        stage = stagenum + 1;
        //비활성화된 사과와 몹 활성화 (스테이지별로 생성하게 최적화 O)
        //    MonsteSetActiveTrue();
        stages[stagenum].setActiveMobs();
        //    AppleSetActiveTrue();
        stages[stagenum].setActiveHeals();
        ScoreManager.score = 0;
        Scoresynchronization();
        player.hpBar.SetDieFalse();
        player.Restart();
        player.isDie = false;
        cam.SetCamStartPosition(stage);
        isStartGame = true;
        if(isPaused)
            OnPauseClick();
        player.transform.position = new Vector3(player.startPosition.x, player.startPosition.y + (stage-1)*40, player.startPosition.z);
    }
    

    private void BtnPauseClick()
    {
        OnPauseClick();
    }
    public void OnPauseClick()
    {
        isPaused = !isPaused;
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;
        
    }
    private void DestroyAttack()
    {
        GameObject[] Attack = GameObject.FindGameObjectsWithTag("Attack");
        for (int i = 0; i < Attack.Length; i++)
        {
            Destroy(Attack[i]);
        }

    }
    public void BtnMainClick()
    {
        stages[stage-1].disableMobs();
        stages[stage - 1].disableHeals();
        DestroyAttack();        
        player.Restart();
        player.PushMainBtn();
        player.hpBar.HpBar_hpReset();
        cam.ResetCamPositon();
        player.ResetPlayerPosition();
    }
    public void SetActiveEndPanal()
    {
        diePanel.SetActive(true);
    }
    public void Scoresynchronization()
    {
        string score = ScoreManager.getScore().ToString();
        scoreText.text = "Score : " + score;
        for(int i=0;i< endPanelScoreText.Length; i++)
        {
            endPanelScoreText[i].text = "점수 : " + score;
        }
       
    }
    public void GameClear()
    {
        OnPauseClick();
        clearPanel.SetActive(true);
        isStartGame = false;
    }
}
