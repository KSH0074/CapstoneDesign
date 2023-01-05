using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Attack: MonoBehaviour
{
    Transform parent;
   

    void Start()
    {
        parent = this.transform.parent;   
    }

    private void OnTriggerEnter(Collider other) 
    {
      
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("Play Attack motion & stand");// 이동 멈추고 공격모션 나와야함 
            //공격 애니메이션 재생
            parent.GetComponent<EnemyAnimationControl>().AttackAnim(true);
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            parent.GetComponent<EnemyAnimationControl>().AttackAnim(false); // 공격 범위 벗어났을 때 공격 취소
        }
    }
}
