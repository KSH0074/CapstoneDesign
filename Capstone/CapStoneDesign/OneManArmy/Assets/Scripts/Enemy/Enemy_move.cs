using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_move : MonoBehaviour
{

    public NavMeshAgent NVagent;
    EnemyStat stat;
    public GameObject GOplayer = null;
    public bool trace = false;

    private void Awake()
    {
        stat = GetComponent<EnemyStat>();
        GOplayer = GameObject.FindWithTag("Player");   
    }

    // Start is called before the first frame update
    void Start()
    {
        NVagent = this.GetComponent<NavMeshAgent>();
        NVagent.speed = stat.Move_speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (trace)
        {
            NVagent.SetDestination(GOplayer.transform.position);
            GetComponent<EnemyAnimationControl>().WalkAnim(true);

            // player가 탐지 됬을 때 Scream 한 후 이동
            if (GetComponent<EnemyAnimationControl>().animator.GetCurrentAnimatorStateInfo(0).fullPathHash ==
                Animator.StringToHash("Base Layer.Walk Aggro"))
            {
                NVagent.isStopped = false;
            }
            else
            {
                NVagent.isStopped = true;
            }
        }

        if(GameManager.instance.isGameOver == true)
        {
            NVagent.ResetPath();
            this.GetComponent<EnemyAnimationControl>().ReStart();
        }
    }
}
