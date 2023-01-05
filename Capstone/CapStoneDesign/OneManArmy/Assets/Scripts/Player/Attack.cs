using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BoxCollider Hit_zone;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            defaultAttack();

        }
        if (GetComponent<AnimationControl>().animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DefaultAttack") &&
            GetComponent<AnimationControl>().animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Hit_zone.enabled = false;//애니메이션 끝나면 공격판정 삭제
        }

    }

    void defaultAttack() 
    {
        if (GetComponent<AnimationControl>().animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.DefaultAttack") &&
            GetComponent<AnimationControl>().animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.Dodge") &&
            GetComponent<AnimationControl>().animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.Death"))
        {
            this.transform.LookAt(AttackLookatPoint());//마우스 포인터가 있는곳을 향한다
            Hit_zone.enabled = true;//공격판정 on 
            GetComponent<AnimationControl>().DefaultAttackAnim(); //공격 애니메이션 재생

            GetComponent<Move>().agent.ResetPath();  
        }

        GetComponent<Move>().agent.isStopped = true; //이동 중지
        GetComponent<Move>().agent.velocity = Vector3.zero; //미끄러짐 방지

    }

    /*
     * 이런 방식으로 공격(스킬)마다 함수 생성
     * 나중에는 함수 포인터화 하여 관리 
     */
    Vector3 AttackLookatPoint()
    {
        
        return GetComponent<Move>().MovePointReturn(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
  
}
