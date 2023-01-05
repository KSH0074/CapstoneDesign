using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator animator;

    public void WalkAnim(bool canWalk)
    {
        animator.SetBool("isWalk", canWalk);
    }

    public void DefaultAttackAnim()
    {
        animator.SetTrigger("defaultAttack");
    }

    public void DodgeAnim()
    {
        animator.SetTrigger("Dodge");
    }

    public void DeathAnim()
    {
        animator.SetTrigger("Death");
    }

    public void StopAnim()
    {
        animator.speed = 1.0f;
    }

    public void RestartAnim()
    {
        //animator.speed = 1.0f;
        animator.SetTrigger("Restart");
    }
}
