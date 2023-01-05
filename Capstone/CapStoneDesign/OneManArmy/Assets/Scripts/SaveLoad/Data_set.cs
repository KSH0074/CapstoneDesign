using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̷��� �˾����� ���ȸ� ����ΰų� �⺻�ڷ������� �߾����. ����̽�;;, Json save Data Format 
//�ϳ��� Ŭ������ �ϳ��� ��ɸ��ؾ��ϴ� ��Ģ�� �߿伺 
public class Data_set
{
    public int MaxHP;
    public float Current_HP;
    public float Move_speed;
    public int Attack_power;
    public int Stealth;
    public int Armor;
    public float EXP;
    public int Level;
    public int SceneNumber;
    public Vector3 Pos;

    public Data_set() //�ʱ�ȭ
    {
        MaxHP = 0;
        Current_HP = 0;
        Move_speed = 0;
        Attack_power = 0;
        Stealth = 0;
        Armor = 0;
        EXP = 0;
        Level = 0;
        SceneNumber = 1;
        Pos = Vector3.zero;
    }

}