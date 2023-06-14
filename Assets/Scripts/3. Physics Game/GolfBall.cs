using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GolfBall : MonoBehaviour
{
    [SerializeField] private Image mPowerIndicator;
    [SerializeField] private TextMeshProUGUI mStatusLabel;
    [SerializeField] private TextMeshProUGUI mMessageLabel;
    [SerializeField] private Transform mDirectionArrow;
    [SerializeField] private Transform mDestinationFlag;
    [SerializeField] private float mDirectionArrowHeight = 2.0f;

    public float mPowerMultiplier = 5f; // 마우스를 누를 때 힘의 증가량
    public float mMaxPower = 100f; // 골프공에 가할 수 있는 최대 힘

    private Rigidbody mRigidbody;
    private Camera mMainCamera;
    private bool mIsMousePressed;
    private float mCurrentPower;

    private Vector3 mOriginPos;

    private int mHitCount = 0;

    private Coroutine? mCoMessage = null;

    private void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mMainCamera = Camera.main;
        mOriginPos = transform.position;

        Init();
    }

    private void Update()
    {
        if(transform.position.y < -1f)
        {
            Init();
            Message("공이 빠졌습니다!");
        }

        if (Input.GetMouseButtonDown(0))
        {
            mIsMousePressed = true;
            mCurrentPower = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            if (mIsMousePressed)
            {
                mCurrentPower += mPowerMultiplier * Time.deltaTime;
                mCurrentPower = Mathf.Clamp(mCurrentPower, 0f, mMaxPower);

                mPowerIndicator.fillAmount = mCurrentPower / mMaxPower;
                mPowerIndicator.color = new Color(1f, 1f - mPowerIndicator.fillAmount, 1f - mPowerIndicator.fillAmount, 1f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ++mHitCount;

            mStatusLabel.text = $"힛 횟수: {mHitCount}";

            mIsMousePressed = false;
            ApplyForce();
        }

        mDirectionArrow.transform.position = transform.position + Vector3.up * mDirectionArrowHeight;
        mDirectionArrow.LookAt(mDestinationFlag);
    }

    private void Init()
    {
        mHitCount = 0;
        transform.position = mOriginPos;
        mStatusLabel.text = $"힛 횟수: {mHitCount}";

        mRigidbody.angularVelocity = mRigidbody.velocity = Vector3.zero;
    }

    private void ApplyForce()
    {
        Vector3 cameraForward = mMainCamera.transform.forward + Vector3.up;
        Vector3 force = cameraForward * mCurrentPower;
        mRigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void Message(string text)
    {
        if(mCoMessage is not null)
            StopCoroutine(mCoMessage);
        mCoMessage = StartCoroutine(CoMessage(text));
    }

    private IEnumerator CoMessage(string text)
    {
        mMessageLabel.text = text;
        yield return new WaitForSeconds(3.0f);

        mMessageLabel.text = "";
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Flag")    
        {
            Message($"{mHitCount}번의 시도 끝에 골인!");
            Init();
        }
    }
}
