using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이럴줄 알았으면 스탯만 떼어두거나 기본자료형으로 했어야함. 설계미스;;, Json save Data Format 
//하나의 클래스는 하나의 기능만해야하는 원칙의 중요성 
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

    public Data_set() //초기화
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