using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MatchPlaneToCamera 스크립트는 평면 오브젝트를 카메라의 크기에 맞게 조정합니다.
public class MatchPlaneToCamera : MonoBehaviour
{
    private Camera mCamera; // 카메라 변수

    private void Awake()
    {
        mCamera = Camera.main; // 또는 원하는 카메라를 지정할 수 있습니다.
    }

    private void Start()
    {
        AdjustPlaneSize(); // 평면의 크기 조정
    }

    private void Update()
    {
        if (HasCameraSizeChanged())
        {
            AdjustPlaneSize(); // 카메라 크기가 변경되었을 때 평면 크기 조정
        }
    }

    // 평면의 크기를 조정합니다.
    private void AdjustPlaneSize()
    {
        float distance = Mathf.Abs(mCamera.transform.position.z - transform.position.z); // 카메라와 평면 사이의 거리
        float height = 2f * distance * Mathf.Tan(mCamera.fieldOfView * 0.5f * Mathf.Deg2Rad); // 평면의 높이
        float width = height * mCamera.aspect; // 평면의 너비

        transform.localScale = new Vector3(width, height, 1f); // 평면의 스케일 조정
    }

    // 카메라의 크기가 변경되었는지 확인합니다.
    private bool HasCameraSizeChanged()
    {
        float currentAspect = mCamera.aspect; // 현재 카메라의 가로세로 비율
        float targetAspect = transform.localScale.x / transform.localScale.z; // 평면의 가로세로 비율

        return !Mathf.Approximately(currentAspect, targetAspect); // 가로세로 비율이 다른지 확인하여 변경 여부 반환
    }
}
