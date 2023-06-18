using System.Collections;
using UnityEngine;

using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class CharacterAiMovement : MonoBehaviour
{
    protected NavMeshAgent mNavMeshAgent;  // NavMeshAgent 컴포넌트를 저장하기 위한 변수
    protected Animator mAnimator;  // Animator 컴포넌트를 저장하기 위한 변수
    protected BoxCollider mBoxCollider;  // BoxCollider 컴포넌트를 저장하기 위한 변수

    private float mDestinationInterval = 10f;  // 목적지 변경 간격을 나타내는 변수
    private float mWanderRadius = 10f;  // 돌아다니는 범위를 나타내는 변수

    protected void Awake()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();  // NavMeshAgent 컴포넌트를 가져옴
        mAnimator = GetComponent<Animator>();  // Animator 컴포넌트를 가져옴
        mBoxCollider = GetComponent<BoxCollider>();  // BoxCollider 컴포넌트를 가져옴
        
        mBoxCollider.size = Vector3.one * 1.5f;  // BoxCollider의 크기를 설정
    }

    private void Start()
    {
        StartCoroutine(CoAiMovement());  // CoAiMovement 코루틴을 시작
    }

    private void Update()
    {
        mAnimator.SetFloat("MovementSpeed", mNavMeshAgent.velocity.magnitude);  // Animator의 MovementSpeed 매개변수를 업데이트
    }

    private IEnumerator CoAiMovement()
    {
        while (true)
        {
            SetRandomDestination();  // 랜덤한 목적지 설정
            yield return new WaitForSeconds(mDestinationInterval);  // 일정 시간 대기 후 반복
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * mWanderRadius;  // 랜덤한 방향 벡터 생성
        randomDirection += transform.position;  // 현재 위치에 더하여 목적지 좌표 생성
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, mWanderRadius, 1);  // 주어진 반경 내에서 유효한 위치를 샘플링
        Vector3 finalPosition = hit.position;  // 최종 목적지 좌표

        mNavMeshAgent.isStopped = false;  // NavMeshAgent 정지 상태 해제
        mNavMeshAgent.SetDestination(finalPosition);  // NavMeshAgent의 목적지 설정
    }

    public void GreetingEach(Vector3 targetPosition)
    {
        mNavMeshAgent.isStopped = true;  // NavMeshAgent 정지 상태 설정

        Vector3 targetDirection = targetPosition - transform.position;  // 대상 위치와의 방향 벡터 계산
        transform.rotation = Quaternion.LookRotation(targetDirection);  // 대상 위치를 향하도록 회전

        mAnimator.SetTrigger("StartGreeting");  // StartGreeting 트리거 활성화
        mAnimator.SetFloat("GreetingsIndex", Random.Range(0, 2));  // GreetingsIndex 매개변수 설정 (0부터 2 사이의 랜덤한 값)
    }

    public void PlayerExit()
    {
        mAnimator.SetTrigger("StopGreeting"); // StopGreeting 트리거 활성화

        SetRandomDestination(); // 랜덤한 목적지 설정
    }
}
