using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour
{

    public Animator animator;

    public void WalkAnim(bool canWalk)
    {
        animator.SetBool("canWalk", canWalk);
    }

    public void AttackAnim(bool canAttack)
    {
        animator.SetBool("canAttack", canAttack);
        if (canAttack == true)
        {
            GetComponent<Enemy_move>().NVagent.isStopped = true;
        }
        else
        {
            GetComponent<Enemy_move>().NVagent.isStopped = false;
        }

    }

    public void DeathAnim()
    {
        animator.SetTrigger("Death");
    }

    public void LookPlayer()
    {
        this.transform.LookAt(GetComponent<Enemy_move>().GOplayer.transform); // 공격시 플레이어 바라봄
    }

    public void ReStart()
    {
        animator.SetTrigger("ReStart");
    }
}
