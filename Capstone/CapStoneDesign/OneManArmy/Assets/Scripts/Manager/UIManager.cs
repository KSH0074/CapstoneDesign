using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance;

    public GameObject player;

    [Header("Hp UI")]
    public Text hpText;
    public Image hpBar;
    public Image curHpBar;
    public Image hitBloodScreen;

    [Header("Stat UI")]
    public GameObject statUI;
    public Text statText;
    public Text statPointText;

    [Header("Stat Icon")]
    public GameObject[] Health;
    public GameObject[] Damage;
    public GameObject[] Stealth;
    public GameObject[] Armor;
    private int[] statPointUsed = new int[] { -1, -1, -1, -1 };
    private int[] tmpStatPointUsed = new int[4];
    private Color yellow = new Color32(240, 255, 16, 255);
    private Color Green = new Color32(65, 248, 105, 255);
    private Color Origin = new Color32(255, 255, 255, 255);


    private int statPoint = 3;
    private int curStatPoint = 0;
    private string statType;
    private int[,] tmpStatAmount = new int[,] { { 0, 10 }, { 0, 2 }, { 0, 5 }, { 0, 2 } };
    // {{health,health증가율}, {damage,damage증가율},{stealth,stealth증가율},{armor,armor증가율}}

    [Header("GameOver UI")]
    public GameObject gameoverUI;

    [Header("Scene Change Panel")]
    public Image scenePanel;
    private float time = 0.0f;
    private float F_time = 2f;

    [Header("Tutorial")]
    public GameObject tutorial;
    public GameObject tutorialText;

    public void ActiveStatUI(bool active)
    {
        if (active == true)
        {
            Time.timeScale = 0;
            ResetStatPoint();
            UpdatePlayerStatText();
            statUI.SetActive(active);
        }
        else
        {
            if (curStatPoint == 0)
            {
                player.GetComponent<PlayerStat>().MaxHP.SetStat(player.GetComponent<PlayerStat>().MaxHP.GetStat() + tmpStatAmount[0, 0]);
                player.GetComponent<PlayerStat>().Current_HP += tmpStatAmount[0, 0];
                UpdateHp(player.GetComponent<PlayerStat>().Current_HP);

                player.GetComponent<PlayerStat>().Attack_power.SetStat(player.GetComponent<PlayerStat>().Attack_power.GetStat() + tmpStatAmount[1, 0]);

                player.GetComponent<PlayerStat>().Stealth.SetStat(player.GetComponent<PlayerStat>().Stealth.GetStat() - tmpStatAmount[2, 0]);
                player.GetComponent<PlayerStat>().TraceTriggerUpdate();

                player.GetComponent<PlayerStat>().Armor.SetStat(player.GetComponent<PlayerStat>().Armor.GetStat() + tmpStatAmount[3, 0]);

                for (int i = 0; i < 4; i++)
                {
                    statPointUsed[i] = tmpStatPointUsed[i];
                }

                Time.timeScale = 1.0f;
                UpdateStatIcon(Green);
                ResetStatPoint();

                statUI.SetActive(active);

                SaveLoad.instance.SaveData();  // 스탯 분배후 세이브
            }
            else
            {
                Debug.LogWarning("StatPoint remain");
            }
        }
    }

    public void StatName(string name)
    {
        statType = name;
    }

    public void UpdateStatLevel()
    {
        if (curStatPoint != 0 && tmpStatPointUsed[0] != 5 && tmpStatPointUsed[1] != 5 && tmpStatPointUsed[2] != 5 && tmpStatPointUsed[3] != 5)
        {
            curStatPoint -= 1;
            switch (statType)
            {
                case "Health":
                    tmpStatAmount[0, 0] += tmpStatAmount[0, 1];
                    tmpStatPointUsed[0] += 1;
                    Health[tmpStatPointUsed[0]].transform.GetChild(0).GetComponent<Image>().color = yellow;
                    break;
                case "Damage":
                    tmpStatAmount[1, 0] += tmpStatAmount[1, 1];
                    tmpStatPointUsed[1] += 1;
                    Damage[tmpStatPointUsed[1]].transform.GetChild(0).GetComponent<Image>().color = yellow;
                    break;
                case "Stealth":
                    tmpStatAmount[2, 0] += tmpStatAmount[2, 1];
                    tmpStatPointUsed[2] += 1;
                    Stealth[tmpStatPointUsed[2]].transform.GetChild(0).GetComponent<Image>().color = yellow;
                    break;
                case "Armor":
                    tmpStatAmount[3, 0] += tmpStatAmount[3, 1];
                    tmpStatPointUsed[3] += 1;
                    Armor[tmpStatPointUsed[3]].transform.GetChild(0).GetComponent<Image>().color = yellow;
                    break;
            }

            UpdatePlayerStatText();
        }
    }

    public void ResetStatPoint()
    {
        curStatPoint = statPoint;
        for (int i = 0; i < 4; i++)
        {
            tmpStatAmount[i, 0] = 0;
            tmpStatPointUsed[i] = statPointUsed[i];
        }
        UpdateStatIcon(Origin);
        UpdatePlayerStatText();
    }

    public void UpdateStatIcon(Color c)
    {
        for (int i = 0; i < 6; i++)
        {
            if (Health[i].transform.GetChild(0).GetComponent<Image>().color == yellow)
                Health[i].transform.GetChild(0).GetComponent<Image>().color = c;
            if (Damage[i].transform.GetChild(0).GetComponent<Image>().color == yellow)
                Damage[i].transform.GetChild(0).GetComponent<Image>().color = c;
            if (Stealth[i].transform.GetChild(0).GetComponent<Image>().color == yellow)
                Stealth[i].transform.GetChild(0).GetComponent<Image>().color = c;
            if (Armor[i].transform.GetChild(0).GetComponent<Image>().color == yellow)
                Armor[i].transform.GetChild(0).GetComponent<Image>().color = c;
        }
    }

    public void UpdateHp(float hp)
    {
        hpText.text = player.GetComponent<PlayerStat>().Current_HP.ToString();

        curHpBar.fillAmount = hp / player.GetComponent<PlayerStat>().MaxHP.GetStat();
        StartCoroutine(UpdateHpBar());
        UpdateHitScreen();
    }

    IEnumerator UpdateHpBar()
    {
        while (true)
        {
            yield return null;
            if (hpBar.fillAmount != curHpBar.fillAmount)
                hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, curHpBar.fillAmount, Time.deltaTime * 2.0f);
        }
    }

    public void UpdateHitScreen()
    {
        // 피격시 screen blood 효과
        Color color = hitBloodScreen.color;
        float colorPercent = (player.GetComponent<PlayerStat>().MaxHP.GetStat() - player.GetComponent<PlayerStat>().Current_HP) /
            player.GetComponent<PlayerStat>().MaxHP.GetStat();

        if(colorPercent < 0.3f)
        {
            color.a = 0f;
        }
        else if(colorPercent < 0.6f)
        {
            color.a = 0.3f;
        }
        else
        {
            color.a = 0.6f;
        }

        hitBloodScreen.color = color;
    }

    public void UpdatePlayerStatText()
    {
        statPointText.text = curStatPoint.ToString();
        //Stat_Board text
        statText.text = "Health : " + player.GetComponent<PlayerStat>().MaxHP.GetStat() + "(+" + tmpStatAmount[0, 0] + ")\n"
            + "Damage : " + player.GetComponent<PlayerStat>().Attack_power.GetStat() + "(+" + tmpStatAmount[1, 0] + ")\n"
            + "Stealth : " + player.GetComponent<PlayerStat>().Stealth.GetStat() + "(+" + tmpStatAmount[2, 0] + ")\n"
            + "Armor : " + player.GetComponent<PlayerStat>().Armor.GetStat() + "(+" + tmpStatAmount[3, 0] + ")";
    }

    public void UpdateGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void UpdateGameWinUI()
    {
        StartCoroutine(FadeOut());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        UpdateGameoverUI(false);
        GameManager.instance.ReStart();

        player.transform.GetChild(0).GetComponent<AnimationControl>().RestartAnim();
    }

    public void StartGame()
    {
        StartCoroutine(FadeOut());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeIn()
    {
        Color alpha = scenePanel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            scenePanel.color = alpha;
            yield return null;
        }
        time = 0.0f;
    }

    IEnumerator FadeOut()
    {
        Scene scene = SceneManager.GetActiveScene();
        Color alpha = scenePanel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            scenePanel.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        time = 0.0f;

        if (scene.name == "Main Menu")
        {
            tutorial.SetActive(true);
            tutorialText.SetActive(true);
            yield return new WaitForSeconds(5.0f);

            SceneManager.LoadScene("SubWay");
        }
        else if(scene.name == "Maze")
        {
            SceneManager.LoadScene("Ending");
        }
    }
}