using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// CameraMovement 스크립트는 카메라의 이동과 커서 설정을 제어합니다.
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mStateTextLabel; // 상태 텍스트를 표시할 TextMeshProUGUI 컴포넌트

    private bool mIsMoveEnable = true; // 카메라 이동을 활성화하는 플래그

    private void Start()
    {
        ToggleCursor(); // 커서 설정 초기화
        UpdateStateText(); // 상태 텍스트 업데이트
    }

    // 매 프레임마다 호출되며, 입력값에 따라 카메라 이동과 회전을 수행합니다.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            mIsMoveEnable = !mIsMoveEnable; // 이동 플래그 반전

            ToggleCursor(); // 커서 상태 업데이트
            UpdateStateText(); // 상태 텍스트 업데이트
        }

        if (mIsMoveEnable == false)
            return;

        Vector2 keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // 키보드 입력값
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // 마우스 입력값

        transform.Translate(keyboardInput.x, 0f, keyboardInput.y); // 키보드 입력값에 따라 이동
        transform.Rotate(0f, mouseInput.x, 0f, Space.World); // 마우스 X축 입력값에 따라 회전
        transform.Rotate(-mouseInput.y, 0f, 0f); // 마우스 Y축 입력값에 따라 회전
    }

    // 커서의 상태를 설정하여 보이지 않거나 잠금 상태로 변경합니다.
    private void ToggleCursor()
    {
        if (mIsMoveEnable)
        {
            Cursor.lockState = CursorLockMode.Locked; // 커서를 잠금 모드로 설정하여 숨김
            Cursor.visible = false; // 커서를 보이지 않도록 설정
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // 커서 잠금 모드 해제하여 표시
            Cursor.visible = true; // 커서를 보이도록 설정
        }
    }

    // 상태 텍스트를 업데이트하여 현재 카메라 이동 상태를 표시합니다.
    private void UpdateStateText()
    {
        mStateTextLabel.text = $"카메라 움직임 {(mIsMoveEnable ? "<color=green>활성화</color>" : "<color=orange>비활성화</color>")}";
        // 상태 텍스트 업데이트. 이동 활성화 여부에 따라 적절한 색상과 메시지 표시
    }
}
