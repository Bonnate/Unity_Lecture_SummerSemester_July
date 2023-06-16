using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineAnimateController : MonoBehaviour
{
    private SplineAnimate mSplineAnimate;

    private float targetNormalizedTime;

    private void Awake()
    {
        mSplineAnimate = GetComponent<SplineAnimate>();
    }

    private void Update()
    {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * OptionsManager.Instance.ScrollSpeed;
        targetNormalizedTime = Mathf.Clamp01(targetNormalizedTime + scrollAmount);

        float currentNormalizedTime = mSplineAnimate.NormalizedTime;
        float lerpedNormalizedTime = Mathf.Lerp(currentNormalizedTime, targetNormalizedTime, OptionsManager.Instance.LerpSpeed * Time.deltaTime);

        mSplineAnimate.NormalizedTime = lerpedNormalizedTime;
    }
}
