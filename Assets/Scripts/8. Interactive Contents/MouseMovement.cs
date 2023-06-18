using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MouseMovement 스크립트는 마우스 입력에 따라 카메라를 이동시킵니다.
public class MouseMovement : MonoBehaviour
{
    private Vector3 mOriginPos; // 초기 위치를 저장하는 변수

    private void Awake()
    {
        mOriginPos = transform.position; // 초기 위치를 현재 위치로 설정합니다.
    }

    private void Update()
    {
        MoveCamera(); // 카메라 이동 함수 호출
    }

    // 마우스 입력에 따라 카메라를 이동시킵니다.
    private void MoveCamera()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // 마우스 입력값을 가져옵니다.

        Vector3 newPosition = transform.position; // 현재 위치를 저장하는 변수

        // 위아래로 이동
        newPosition.y += mouseInput.y * OptionsManager.Instance.MoveSpeed;

        // 좌우로 이동
        newPosition.x += mouseInput.x * OptionsManager.Instance.MoveSpeed;

        // 이동 가능한 최대 범위 제한
        newPosition.x = Mathf.Clamp(newPosition.x, mOriginPos.x - OptionsManager.Instance.MaxMoveRange, mOriginPos.x + OptionsManager.Instance.MaxMoveRange);
        newPosition.y = Mathf.Clamp(newPosition.y, mOriginPos.y - OptionsManager.Instance.MaxMoveRange, mOriginPos.y + OptionsManager.Instance.MaxMoveRange);

        transform.position = newPosition; // 새로운 위치로 카메라를 이동시킵니다.
    }
}
