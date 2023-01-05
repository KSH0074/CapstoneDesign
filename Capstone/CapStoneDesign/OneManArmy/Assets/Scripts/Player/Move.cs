using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Move : MonoBehaviour
{

    public NavMeshAgent agent;
    private Vector3 movePoint; // 이동 위치 저장
    public Camera mainCamera; // 메인 카메라
    public Ray ray;

    public ParticleSystem ps;

    void Start()
    {
        //기본설정 초기화


        mainCamera = Camera.main;
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = this.transform.parent.GetComponent<PlayerStat>().Move_speed;
        agent.angularSpeed = 7600.0f;
        agent.stoppingDistance = 0;
        agent.autoBraking = false;
    }

    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            //우클릭 이동 
            if (Input.GetMouseButton(1))
            {
                Vector3 movePoint = MovePointReturn(ray);
                Move_to(movePoint);
            }
            //이동 장소에 이펙트 추가
            if (Input.GetMouseButtonUp(1) && Time.timeScale != 0)//애니메이션 끝일때만 나오도록 수정해야함 
            {
                Instantiate(ps, movePoint, Quaternion.identity);
            }

            if (DestinationArrived())
            {
                // Debug.Log("Arrived");
                GetComponent<AnimationControl>().WalkAnim(false);
            }
        }
    }

    // 추후 기능관리, 유지보수 용이 하도록 변경
    void Move_to(Vector3 movePoint)
    {
        if (GetComponent<AnimationControl>().animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.DefaultAttack") &&
            GetComponent<AnimationControl>().animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.Dodge"))
        {
            //Debug.Log("set destination");
            agent.SetDestination(movePoint);
            GetComponent<AnimationControl>().WalkAnim(true);
            MoveGo();
        }
        else
        {
           MoveStop(); // 공격 모션중 이동 방지
        }

    }

    public Vector3 MovePointReturn(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            movePoint = raycastHit.point;
            //Debug.Log("movePoint : " + movePoint.ToString());
            //Debug.Log("맞은 객체 : " + raycastHit.transform.name);

        }
        return movePoint;
    }

    public Ray getRay()
    {
        return ray;
    }

    private bool DestinationArrived()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void MoveStop()
    {
        agent.isStopped = true;
        GetComponent<AnimationControl>().WalkAnim(false);
       
    }
    public void MoveGo() 
    {
        agent.isStopped = false;
    }
}

