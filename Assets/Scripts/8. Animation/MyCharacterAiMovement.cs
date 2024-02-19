using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterAiMovement : CharacterAiMovement
{
    // Awake() 메서드를 재정의합니다.
    private new void Awake()
    {
        base.Awake(); // 부모 클래스의 Awake() 메서드를 호출합니다.
    }

    private void Start()
    {
        // 아무런 동작이 없는 Start() 메서드입니다.
    }

    private void Update()
    {
        // 현재 이동 속도에 따라 애니메이션 파라미터 "MovementSpeed"를 설정합니다.
        mAnimator.SetFloat("MovementSpeed", mNavMeshAgent.velocity.magnitude);

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼이 눌렸는지 확인합니다.
        {
            // 마우스 클릭 위치로부터 레이를 발사합니다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // 레이캐스트를 통해 오브젝트와 충돌이 있는지 확인합니다.
            {
                Vector3 hitPoint = hit.point; // 레이가 충돌한 지점을 저장합니다.

                mNavMeshAgent.isStopped = false; // 네비게이션 에이전트의 이동을 재개합니다.

                mNavMeshAgent.SetDestination(hitPoint); // 네비게이션 에이전트의 목적지를 설정합니다.
            }
        }
    }
}
