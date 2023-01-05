using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public Stat MaxHP;
    public float Currnet_HP { get; set; }
    public float Move_speed;
    public Stat Attack_power;
    public float EXP = 10; //사망시 플레이어에게 주는 EXP
    public Vector3 Pos;

    public Transform player;
    PlayerStat player_stat;
    // public int Level = 1;

    //스텔스 스탯 < 적 탐지 트리거 크기 영향  <  스킬레벨 올릴 시 이벤트 발생 < 레벨업 및 스킬UX 
    //  캐릭터에 스텔스 생성


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.parent;
        MaxHP.SetStat(100);
        Currnet_HP = MaxHP.GetStat();
        Attack_power.SetStat(15);
        Move_speed = 4.7f;
        Pos = this.transform.position;

        player_stat = player.GetComponent<PlayerStat>();
    }

    public void TakeDamage(int damage)
    {
        Currnet_HP -= damage;
        damageCheck();
    }

    void damageCheck()
    {
        if (Currnet_HP <= 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            //플레이어에게 경험치 줌 
            player_stat.EXP += this.EXP;
            //EXP 체크 이후 레벨상승 시 발생 이벤트 
            player_stat.EXPcheck();
            // Debug.LogError("적 사망");

            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        //사망 애니메이션 작동 후 
        GetComponent<EnemyAnimationControl>().DeathAnim();
        Destroy(this.gameObject, 2.0f);//2초뒤 제거
    }

    public void ResetPosition() // 게임 재시작시 원래 위치로 이동
    {
        this.transform.position = Pos;
    }

}
