using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InteractiveCamera : MonoBehaviour
{
    private Camera mMainCamera;

    [SerializeField] private float moveSpeed = 0.01f; // 카메라 이동 속도
    [SerializeField] private float maxMoveRange = 0.25f; // 이동 가능한 최대 범위

    private Vector3 mOriginPos;

    private void Awake()
    {
        mOriginPos = transform.position;
        mMainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        MoveCamera();
        Raycast();
    }

    private void MoveCamera()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector3 newPosition = transform.position;

        // 위아래로 이동
        newPosition.y += mouseInput.y * moveSpeed;

        // 좌우로 이동
        newPosition.x += mouseInput.x * moveSpeed;

        // 이동 가능한 최대 범위 제한
        newPosition.x = Mathf.Clamp(newPosition.x, mOriginPos.x - maxMoveRange, mOriginPos.x + maxMoveRange);
        newPosition.y = Mathf.Clamp(newPosition.y, mOriginPos.y - maxMoveRange, mOriginPos.y + maxMoveRange);

        transform.position = newPosition;
    }

    private void Raycast()
    {
        Ray ray = mMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "InteractiveProp")
            {
                hit.transform.GetComponent<InteractiveProps>().Force(1.0f);
            }
        }
    }
}
