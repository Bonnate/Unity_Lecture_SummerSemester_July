using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    private Vector3 mOriginPos;

    private void Awake()
    {
        mOriginPos = transform.position;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector3 newPosition = transform.position;

        // 위아래로 이동
        newPosition.y += mouseInput.y * OptionsManager.Instance.MoveSpeed;

        // 좌우로 이동
        newPosition.x += mouseInput.x * OptionsManager.Instance.MoveSpeed;

        // 이동 가능한 최대 범위 제한
        newPosition.x = Mathf.Clamp(newPosition.x, mOriginPos.x - OptionsManager.Instance.MaxMoveRange, mOriginPos.x + OptionsManager.Instance.MaxMoveRange);
        newPosition.y = Mathf.Clamp(newPosition.y, mOriginPos.y - OptionsManager.Instance.MaxMoveRange, mOriginPos.y + OptionsManager.Instance.MaxMoveRange);

        transform.position = newPosition;
    }
}
