using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MatchPlaneToCamera : MonoBehaviour
{
    private Camera mCamera;

    private void Awake()
    {
        mCamera = Camera.main; // 또는 원하는 카메라를 지정할 수 있습니다.
    }

    private void Start()
    {
        AdjustPlaneSize();
    }

    private void Update()
    {
        if (HasCameraSizeChanged())
        {
            AdjustPlaneSize();
        }
    }

    private void AdjustPlaneSize()
    {
        float distance = Mathf.Abs(mCamera.transform.position.z - transform.position.z);
        float height = 2f * distance * Mathf.Tan(mCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * mCamera.aspect;

        transform.localScale = new Vector3(width, height, 1f);
    }

    private bool HasCameraSizeChanged()
    {
        float currentAspect = mCamera.aspect;
        float targetAspect = transform.localScale.x / transform.localScale.z;

        return !Mathf.Approximately(currentAspect, targetAspect);
    }
}
