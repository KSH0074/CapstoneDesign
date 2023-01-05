using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            //싱글턴 오브젝트 반환
            return m_instance;
        }
    }

    private static GameManager m_instance;
    public GameObject player;
    public GameObject[] enemies;

    public bool isGameOver { get; set; }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        isGameOver = false;
    }

    public void LevelUP()
    {
        UIManager.instance.ActiveStatUI(true);
    }

    public void GameWin()
    {
        UIManager.instance.UpdateGameWinUI();
        SaveLoad.instance.DeleteData();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.transform.GetComponent<EnemyStat>().EnemyDeath();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        UIManager.instance.UpdateGameoverUI(true);
    }

    public void ReStart()
    {
        isGameOver = false;
        SaveLoad.instance.LoadData();
        player.transform.GetComponent<CapsuleCollider>().enabled = true;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.transform.GetComponent<EnemyStat>().ResetPosition();
        }
    }
}
