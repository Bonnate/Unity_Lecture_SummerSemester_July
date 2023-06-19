using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractiveProps : MonoBehaviour
{
    private Rigidbody mRigidbody; // Rigidbody 컴포넌트를 저장할 변수
    private Vector3 mOriginPosition; // 초기 위치를 저장할 변수
    [SerializeField] private float mMoveSpeed = 0.1f; // 이동 속도

    private float mHitDuration = 0f; // 히트 지속 시간

    private void Awake()
    {
        mHitDuration = 1.0f; // 히트 지속 시간 초기화

        mRigidbody = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        mRigidbody.useGravity = false; // 중력 사용 안 함

        mRigidbody.angularDrag = mRigidbody.drag = 0.25f; // 회전 및 이동 저항 설정

        if (TryGetComponent<Collider>(out _) == false)
        {
            gameObject.AddComponent<BoxCollider>(); // Collider 컴포넌트 추가
        }

        mOriginPosition = transform.position; // 초기 위치 저장

        // 무작위 방향과 힘으로 물체에 충격을 가합니다.
        mRigidbody.AddForce(new Vector3(1 - 2 * Random.value, 1 - 2 * Random.value, 1 - 2 * Random.value) * Random.Range(0f, 1f), ForceMode.Impulse);
        mRigidbody.AddTorque(new Vector3(1 - 2 * Random.value, 1 - 2 * Random.value, 1 - 2 * Random.value) * Random.Range(0f, 0.25f), ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoMoveToCenter()); // 중심으로 이동하는 코루틴 실행
    }

    // 중심으로 이동하는 코루틴
    private IEnumerator CoMoveToCenter()
    {
        while (true)
        {
            mHitDuration -= Time.deltaTime;
            if (mHitDuration > 0f)
            {
                yield return null;
                continue;
            }

            float distanceToOrigin = (mOriginPosition - transform.position).magnitude;

            if (distanceToOrigin < 0.5f)
            {
                // 남은 거리에 기반하여 감속 계수 계산
                float deceleration = Mathf.Clamp01(distanceToOrigin / 0.1f);

                // 물체의 속도를 점차 감소시킴
                mRigidbody.velocity *= deceleration;
            }
            else
            {
                // 중심으로 향하는 정상적인 힘을 가합니다.
                mRigidbody.AddForce((mOriginPosition - transform.position).normalized * mMoveSpeed);
            }

            yield return null;
        }
    }

    // 외부에서 물체에 힘을 가하는 함수
    public void Force(Vector3 hitPointPos, float power)
    {
        // 충격 위치에서 물체로 향하는 방향에 일정한 힘을 가합니다.
        mRigidbody.AddForce((transform.position - hitPointPos).normalized * power);
        mRigidbody.AddTorque((transform.position - hitPointPos).normalized * power * 0.1f);

        mHitDuration = 1.0f; // 히트 지속 시간 초기화
    }
}