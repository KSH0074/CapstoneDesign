using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;

    PlayerStat ps;
    Data_set saveData;
    string SLdata; // save Load data
    string path; // 경로
    const string FILENAME = "SaveFile.json";
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        //다른 씬에서도 사용 가능하도록 유지, 필요없을시 삭제  
        //메인화면에서는 Load 기능만, Data_set을 static으로 안 하는 방식사용
        DontDestroyOnLoad(this.gameObject);
        
    }

    private void Start()
    {
        ps = GameObject.FindObjectOfType<PlayerStat>();
        saveData = new Data_set();
        path = Application.persistentDataPath + "/"; //유니티 기본경로 // C:\Users\----\AppData\LocalLow\DefaultCompany\... 
    }

    //test Code 입니다. 테스트 이후 삭제
  /*  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //savePoint랑 만나면 Save, Load 버튼을 누르면 Load
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
            LoadData();//이부분은 파일 읽기 + 적용 
        }
    }*/

    public void SaveData()
    {  //시간이 남으면 플레이어 스탯을 다른 클래스로 분할시켜 하드코딩요소 제거..
        ps = GameObject.FindObjectOfType<PlayerStat>();
        saveData.MaxHP = ps.MaxHP.GetStat();
        saveData.Current_HP = ps.Current_HP;
        saveData.Move_speed = ps.Move_speed;
        saveData.Attack_power = ps.Attack_power.GetStat();
        saveData.Stealth = ps.Stealth.GetStat();
        saveData.Armor = ps.Armor.GetStat();
        saveData.EXP = ps.EXP;
        saveData.Level = ps.Level;
        saveData.SceneNumber = SceneManager.GetActiveScene().buildIndex;//현재 씬 
        saveData.Pos = ps.transform.GetChild(0).position; //!!!

        SLdata = JsonUtility.ToJson(saveData);
        File.WriteAllText(path + FILENAME, SLdata);// 이부분은 파일 저장 
    }

    public void LoadData()
    {
        SLdata = File.ReadAllText(path + FILENAME);
        Data_set Load = JsonUtility.FromJson<Data_set>(SLdata); // 읽어오는 부분

        if (Load.MaxHP == 0)
            return;

        //Player 찾아서 Stat 변경 
        ps = GameObject.FindObjectOfType<PlayerStat>();
        ps.MaxHP.SetStat(Load.MaxHP);
        ps.Current_HP = Load.Current_HP;
        ps.Move_speed = Load.Move_speed;
        ps.Attack_power.SetStat(Load.Attack_power);
        ps.Stealth.SetStat(Load.Stealth);
        ps.Armor.SetStat(Load.Armor);
        ps.EXP = Load.EXP;
        ps.Level = Load.Level;
        ps.transform.GetChild(0).position = Load.Pos;//!!!
        ps.transform.GetChild(0).GetComponent<Move>().MoveStop();//이동 멈추기

        UIManager.instance.UpdateHp(ps.Current_HP); // UI 업데이트

        Debug.Log(path);
    }
    public void LoadData(int i)
    {
        SLdata = File.ReadAllText(path + FILENAME);
        Data_set Load = JsonUtility.FromJson<Data_set>(SLdata); // 읽어오는 부분

        if (Load.MaxHP == 0)
            return;

        //Player 찾아서 Stat 변경 
        ps = GameObject.FindObjectOfType<PlayerStat>();
        ps.MaxHP.SetStat(Load.MaxHP);
        ps.Current_HP = Load.Current_HP;
        ps.Move_speed = Load.Move_speed;
        ps.Attack_power.SetStat(Load.Attack_power);
        ps.Stealth.SetStat(Load.Stealth);
        ps.Armor.SetStat(Load.Armor);
        ps.EXP = Load.EXP;
        ps.Level = Load.Level;
        
        ps.transform.GetChild(0).GetComponent<Move>().MoveStop();//이동 멈추기

        UIManager.instance.UpdateHp(ps.Current_HP); // UI 업데이트

        Debug.Log(path);
    }

    public void LoadScene() //메인화면에서 이어하기 선택할 시 이 함수 사용 후 LoadData함수 호출 
    {
        SLdata = File.ReadAllText(path + FILENAME);
        Data_set Load = JsonUtility.FromJson<Data_set>(SLdata);

        SceneManager.LoadScene(Load.SceneNumber);
    }

    public void InitData() //메인에서 start시 세이브데이터 초기화
    {
        saveData = new Data_set();
        SLdata = JsonUtility.ToJson(saveData);
        File.WriteAllText(path + FILENAME, SLdata);
    }

    public void DeleteData()
    {
        System.IO.File.Delete(path + FILENAME);
    }
}
