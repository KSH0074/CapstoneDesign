using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
     
   
    PlayerStat ps;
    Data_set saveData;
    private void Start()
    {
        ps = GameObject.FindObjectOfType<PlayerStat>();
        saveData = new Data_set();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            DataSave();
            string json = JsonUtility.ToJson(saveData);
            Debug.Log(json);
            Data_set load = JsonUtility.FromJson<Data_set>(json);
            LoadData(load);
        }
    }
    private void DataSave() 
    {
        //시간이 남으면 플레이어 스탯을 다른 클래스로 분할시켜 하드코딩요소 제거..
        saveData.MaxHP = ps.MaxHP.GetStat();
        saveData.Current_HP = ps.Current_HP;
        saveData.Move_speed = ps.Move_speed;
        saveData.Attack_power = ps.Attack_power.GetStat();
        saveData.Stealth = ps.Stealth.GetStat();
        saveData.Armor = ps.Armor.GetStat();
        saveData.EXP = ps.EXP;
        saveData.Level = ps.Level;
    }
    private void LoadData(Data_set data_Set) 
    {
        print(data_Set.MaxHP);
        print(data_Set.Current_HP);
        print(data_Set.Move_speed);
        print(data_Set.Attack_power);
        print(data_Set.Stealth);
        print(data_Set.Armor);
        print(data_Set.EXP);
        print(data_Set.Level);
    }
}
