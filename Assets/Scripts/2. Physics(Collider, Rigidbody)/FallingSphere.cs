using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FallingSphere 스크립트는 구체 오브젝트를 제어하여 원래 위치로 이동시키는 기능을 제공합니다.
[RequireComponent(typeof(Rigidbody))]
public class FallingSphere : MonoBehaviour
{
    public Vector3 OriginPos { private set; get; } // 원래 위치를 저장하는 벡터

    private Rigidbody mRigidbody; // Rigidbody 컴포넌트를 저장하는 변수

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옵니다.

        OriginPos = transform.position; // 현재 위치를 원래 위치로 초기화합니다.
    }

    // 구체 오브젝트를 원래 위치로 이동시키는 함수
    public void MoveToOrigin()
    {
        transform.position = OriginPos; // 구체 오브젝트의 위치를 원래 위치로 이동시킵니다.

        mRigidbody.velocity = Vector3.zero; // 구체의 속도를 0으로 초기화하여 정지시킵니다.
        mRigidbody.angularVelocity = Vector3.zero; // 구체의 각속도를 0으로 초기화하여 회전을 멈춥니다.
    }
}