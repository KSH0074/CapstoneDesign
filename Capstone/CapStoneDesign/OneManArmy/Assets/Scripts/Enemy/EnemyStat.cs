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
    public float EXP = 10; //����� �÷��̾�� �ִ� EXP
    public Vector3 Pos;

    public Transform player;
    PlayerStat player_stat;
    // public int Level = 1;

    //���ڽ� ���� < �� Ž�� Ʈ���� ũ�� ����  <  ��ų���� �ø� �� �̺�Ʈ �߻� < ������ �� ��ųUX 
    //  ĳ���Ϳ� ���ڽ� ����


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
            //�÷��̾�� ����ġ �� 
            player_stat.EXP += this.EXP;
            //EXP üũ ���� ������� �� �߻� �̺�Ʈ 
            player_stat.EXPcheck();
            // Debug.LogError("�� ���");

            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        //��� �ִϸ��̼� �۵� �� 
        GetComponent<EnemyAnimationControl>().DeathAnim();
        Destroy(this.gameObject, 2.0f);//2�ʵ� ����
    }

    public void ResetPosition() // ���� ����۽� ���� ��ġ�� �̵�
    {
        this.transform.position = Pos;
    }

}
