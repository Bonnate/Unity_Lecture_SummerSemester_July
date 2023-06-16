using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    private MeshRenderer mMeshRenderer;
    private Material mMat;

    private Color mTargetTopColor;
    private Color mTargetBottomColor;
    private Color mCurrentTopColor;
    private Color mCurrentBottomColor;

    private void Awake()
    {
        mMeshRenderer = GetComponent<MeshRenderer>();
        mMat = mMeshRenderer.sharedMaterial;
    }

    private void Start()
    {
        mCurrentTopColor = GetRandomColor();
        mCurrentBottomColor = GetRandomColor();
        mTargetTopColor = GetRandomColor();
        mTargetBottomColor = GetRandomColor();
    }

    void Update()
    {
        // Color.Lerp를 사용하여 현재 색상을 부드럽게 변경합니다.
        mCurrentTopColor = Color.Lerp(mCurrentTopColor, mTargetTopColor, Time.deltaTime / OptionsManager.Instance.ColorChangeDuration);
        mCurrentBottomColor = Color.Lerp(mCurrentBottomColor, mTargetBottomColor, Time.deltaTime / OptionsManager.Instance.ColorChangeDuration);

        mMat.SetColor("_TopColor", mCurrentTopColor);
        mMat.SetColor("_BottomColor", mCurrentBottomColor);

        // 색상 변경이 완료되었을 때 새로운 무작위 색상을 설정합니다.
        if (Vector4.Distance(mCurrentTopColor, mTargetTopColor) < 0.05f)
        {
            mTargetTopColor = GetRandomColor();
        }

        if (Vector4.Distance(mCurrentBottomColor, mTargetBottomColor) < 0.05f)
        {
            mTargetBottomColor = GetRandomColor();
        }
    }

    private Color GetRandomColor()
    {
        // 무작위로 RGB 값을 생성하여 Color 객체를 반환합니다.
        return new Color(Random.value, Random.value, Random.value) * OptionsManager.Instance.HdrIntensity;
    }
}
