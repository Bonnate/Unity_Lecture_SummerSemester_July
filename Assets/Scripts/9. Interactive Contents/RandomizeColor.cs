using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    private MeshRenderer mMeshRenderer; // MeshRenderer 컴포넌트를 저장할 변수
    private Material mMat; // 사용할 Material을 저장할 변수

    private Color mTargetTopColor; // 상단 색상의 목표값
    private Color mTargetBottomColor; // 하단 색상의 목표값
    private Color mCurrentTopColor; // 현재 상단 색상
    private Color mCurrentBottomColor; // 현재 하단 색상

    private void Awake()
    {
        mMeshRenderer = GetComponent<MeshRenderer>(); // 현재 게임 오브젝트에 부착된 MeshRenderer 컴포넌트를 가져옴
        mMat = mMeshRenderer.sharedMaterial; // MeshRenderer의 공유 Material을 가져옴
    }

    private void Start()
    {
        mCurrentTopColor = GetRandomColor(); // 현재 상단 색상을 무작위로 설정
        mCurrentBottomColor = GetRandomColor(); // 현재 하단 색상을 무작위로 설정
        mTargetTopColor = GetRandomColor(); // 목표 상단 색상을 무작위로 설정
        mTargetBottomColor = GetRandomColor(); // 목표 하단 색상을 무작위로 설정
    }

    void Update()
    {
        bool isClicked = Input.GetMouseButton(0); // 마우스 왼쪽 버튼이 클릭되었는지 여부를 확인

        // Color.Lerp를 사용하여 현재 색상을 부드럽게 변경합니다.
        mCurrentTopColor = Color.Lerp(mCurrentTopColor, mTargetTopColor, Time.deltaTime / OptionsManager.Instance.ColorChangeDuration * (isClicked ? 5f : 1f));
        mCurrentBottomColor = Color.Lerp(mCurrentBottomColor, mTargetBottomColor, Time.deltaTime / OptionsManager.Instance.ColorChangeDuration * (isClicked ? 5f : 1f));

        mMat.SetColor("_TopColor", mCurrentTopColor); // Material의 "_TopColor" 속성을 현재 상단 색상으로 설정
        mMat.SetColor("_BottomColor", mCurrentBottomColor); // Material의 "_BottomColor" 속성을 현재 하단 색상으로 설정

        // 색상 변경이 완료되었을 때 새로운 무작위 색상을 설정합니다.
        if (Vector4.Distance(mCurrentTopColor, mTargetTopColor) < 0.05f)
        {
            mTargetTopColor = GetRandomColor(); // 상단 색상의 목표값을 무작위로 변경
        }

        if (Vector4.Distance(mCurrentBottomColor, mTargetBottomColor) < 0.05f)
        {
            mTargetBottomColor = GetRandomColor(); // 하단 색상의 목표값을 무작위로 변경
        }
    }

    private Color GetRandomColor()
    {
        // 무작위로 RGB 값을 생성하여 Color 객체를 반환합니다.
        return new Color(Random.value, Random.value, Random.value) * OptionsManager.Instance.HdrIntensity;
    }
}
