using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

// SplineAnimateController 스크립트는 SplineAnimate 컴포넌트를 제어하여 스플라인 애니메이션을 조작합니다.
public class SplineAnimateController : MonoBehaviour
{
    private SplineAnimate mSplineAnimate; // SplineAnimate 컴포넌트를 저장할 변수

    private float targetNormalizedTime; // 목표 정규화 시간

    private void Awake()
    {
        mSplineAnimate = GetComponent<SplineAnimate>(); // 스크립트가 부착된 게임 오브젝트의 SplineAnimate 컴포넌트를 가져옵니다.
    }

    // 매 프레임마다 호출되며, 마우스 스크롤 입력에 따라 정규화 시간을 조절합니다.
    private void Update()
    {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * OptionsManager.Instance.ScrollSpeed; // 마우스 스크롤 입력값
        targetNormalizedTime = Mathf.Clamp01(targetNormalizedTime + scrollAmount); // 정규화 시간을 스크롤 입력값에 따라 조절합니다.

        float currentNormalizedTime = mSplineAnimate.NormalizedTime; // 현재 정규화 시간
        float lerpedNormalizedTime = Mathf.Lerp(currentNormalizedTime, targetNormalizedTime, OptionsManager.Instance.LerpSpeed * Time.deltaTime);
        // 현재 정규화 시간과 목표 정규화 시간 사이를 OptionsManager에서 정의된 보간 속도로 보간하여 새로운 정규화 시간을 계산합니다.

        mSplineAnimate.NormalizedTime = lerpedNormalizedTime; // 계산된 정규화 시간을 SplineAnimate 컴포넌트에 적용합니다.
    }
}
