using UnityEngine;

// CameraController 스크립트는 대상 오브젝트를 중심으로 카메라를 회전하여 따라다니는 역할을 수행합니다.
public class CameraController : MonoBehaviour
{
    public Transform mTarget; // 카메라가 바라볼 대상 오브젝트
    public float mDistance = 5f; // 카메라와 대상 사이의 거리
    public float mHeight = 2f; // 카메라의 높이
    public float mRotationSpeed = 3f; // 카메라 이동의 속도

    private float mCurrentX = 0f; // 카메라의 현재 x축 회전값

    // 매 프레임의 마지막에 호출되며, 대상 오브젝트를 중심으로 카메라 위치와 회전을 업데이트합니다.
    private void LateUpdate()
    {
        // 마우스 이동 입력값 가져오기
        float mouseX = Input.GetAxis("Mouse X");

        // 카메라의 x축 회전값 업데이트
        mCurrentX += mouseX * mRotationSpeed;

        // 카메라 위치 계산
        Quaternion rotation = Quaternion.Euler(0f, mCurrentX, 0f); // 현재 x축 회전값을 쿼터니언으로 변환
        Vector3 negDistance = new Vector3(0f, mHeight, -mDistance); // 카메라 위치를 지정하기 위한 벡터 계산
        Vector3 position = rotation * negDistance + mTarget.position; // 카메라의 위치 계산

        // 카메라 이동
        transform.rotation = rotation; // 카메라의 회전값 설정
        transform.position = position; // 카메라의 위치 설정
    }
}
