using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private bool canDodge = true;
    public CapsuleCollider hit_zone;
    private Vector3 dodgeVector;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDodge && !GameManager.instance.isGameOver)
        {
            dodging();
        }
    }
    void dodging()
    {
        canDodge = false;
        GetComponent<Move>().agent.isStopped = true; //이동 중지
        GetComponent<Move>().agent.ResetPath();
        hit_zone.enabled = false; //데미지 무시

        dodgeVector = GetComponent<Move>().MovePointReturn(Camera.main.ScreenPointToRay(Input.mousePosition));
        this.transform.LookAt(dodgeVector);

        GetComponent<AnimationControl>().DodgeAnim(); //회피 모션 재생

        StartCoroutine(CheckDodgeAnim());
    }


    IEnumerator CheckDodgeAnim()
    {
        while (true)
        {
            yield return null;
            if (GetComponent<AnimationControl>().animator.GetCurrentAnimatorStateInfo(0).fullPathHash !=
            Animator.StringToHash("Base Layer.Dodge"))
            {
                //this.transform.position = GetComponent<AnimationControl>().animator.rootPosition;
                hit_zone.GetComponent<CapsuleCollider>().enabled = true; //데미지 적용
                GetComponent<Move>().agent.isStopped = false; //이동 가능
                canDodge = true;
                break;
            }
        }
       
    }
    
}
