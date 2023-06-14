using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // 카메라가 바라볼 대상 오브젝트
    public float distance = 5f; // 카메라와 대상 사이의 거리
    public float height = 2f; // 카메라의 높이
    public float rotationSpeed = 3f; // 카메라 이동의 속도

    private float currentX = 0f; // 카메라의 현재 x축 회전값

    private void LateUpdate()
    {
        // 마우스 이동 입력값 가져오기
        float mouseX = Input.GetAxis("Mouse X");

        // 카메라의 x축 회전값 업데이트
        currentX += mouseX * rotationSpeed;

        // 카메라 위치 계산
        Quaternion rotation = Quaternion.Euler(0f, currentX, 0f);
        Vector3 negDistance = new Vector3(0f, height, -distance);
        Vector3 position = rotation * negDistance + target.position;

        // 카메라 이동
        transform.rotation = rotation;
        transform.position = position;
    }
}
