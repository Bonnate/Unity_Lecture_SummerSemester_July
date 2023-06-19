using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// GolfBall 스크립트는 골프공을 제어하는 게임 로직 스크립트입니다.
public class GolfBall : MonoBehaviour
{
    [SerializeField] private Image mPowerIndicator; // 힘 게이지를 표시할 Image 컴포넌트
    [SerializeField] private TextMeshProUGUI mStatusLabel; // 히트 횟수를 표시할 TextMeshProUGUI 컴포넌트
    [SerializeField] private TextMeshProUGUI mMessageLabel; // 메시지를 표시할 TextMeshProUGUI 컴포넌트
    [SerializeField] private Transform mDirectionArrow; // 이동 방향을 가리킬 화살표의 Transform 컴포넌트
    [SerializeField] private Transform mDestinationFlag; // 목적지 깃발의 Transform 컴포넌트
    [SerializeField] private float mDirectionArrowHeight = 2.0f; // 화살표의 높이

    public float mPowerMultiplier = 5f; // 마우스를 누를 때 힘의 증가량
    public float mMaxPower = 100f; // 골프공에 가할 수 있는 최대 힘

    [Space(50)]
    [Header("먼지 파티클 프리팹")]
    [SerializeField] private GameObject mDustParticlePrefab; // 먼지 파티클의 프리팹

    [Header("컨페티 파티클 프리팹")]
    [SerializeField] private GameObject mConfettiParticlePrefab; // 컨페티 파티클의 프리팹

    private Rigidbody mRigidbody; // Rigidbody 컴포넌트
    private Camera mMainCamera; // 메인 카메라
    private bool mIsMousePressed; // 마우스가 눌렸는지 여부
    private float mCurrentPower; // 현재 힘의 크기

    private Vector3 mOriginPos; // 초기 위치

    private int mHitCount = 0; // 히트 횟수

    private Coroutine? mCoMessage = null; // 메시지 코루틴

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 커서를 잠금 모드로 설정하여 숨김
        Cursor.visible = false; // 커서를 보이지 않도록 설정

        mRigidbody = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옵니다.
        mMainCamera = Camera.main; // 메인카메라를 가져옵니다.
        mOriginPos = transform.position; // 게임 시작 시 현재 위치를 등록합니다.

        Init(); // 골프공을 초기화합니다.
    }

    private void Update()
    {
        // 만약 골프공의 y좌표가 -1보다 작아질경우 물에 빠졌다고 판단합니다.
        if (transform.position.y < -1f)
        {
            // 골프공을 초기화합니다.
            Init();

            // UI 메시지를 출력합니다.
            Message("공이 빠졌습니다!");

            // 효과음을 재생합니다.
            SoundManager.Instance.Play("Water Splash");
        }

        // 만약 사용자가 마우스 왼쪽 클릭을 누르면 다음 로직을 수행합니다.
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 누름 활성화
            mIsMousePressed = true;

            // 골프공에게 부가할 힘을 초기화
            mCurrentPower = 0f;
        }

        // 마우스 버튼을 계속 누르고 있으면 다음 로직을 수행합니다.
        if (Input.GetMouseButton(0))
        {
            if (mIsMousePressed)
            {
                // 골프공에게 부가할 힘을 지속적으로 증가시킵니다.
                mCurrentPower += mPowerMultiplier * Time.deltaTime;
                mCurrentPower = Mathf.Clamp(mCurrentPower, 0f, mMaxPower);

                // 골프공 힘 게이지 UI를 채웁니다.
                mPowerIndicator.fillAmount = mCurrentPower / mMaxPower;
                mPowerIndicator.color = new Color(1f, 1f - mPowerIndicator.fillAmount, 1f - mPowerIndicator.fillAmount, 1f);
            }
        }

        // 마우스 버튼을 뗀 경우 다음 로직을 수행합니다.
        if (Input.GetMouseButtonUp(0))
        {
            // 힛 카운트를 1 증가시킵니다.
            ++mHitCount;

            // UI 라벨에 힛 횟수를 표시합니다.
            mStatusLabel.text = $"힛 횟수: {mHitCount}";

            // 마우스 누름 비활성화
            mIsMousePressed = false;

            // 골프공에 힘을 부가합니다.
            ApplyForce();
        }

        // 지속적으로 골프공이 가야 할 방향(빨간색 화살표)을 표시해줍니다.
        mDirectionArrow.transform.position = transform.position + Vector3.up * mDirectionArrowHeight;
        mDirectionArrow.LookAt(mDestinationFlag);
    }

    // 골프공을 초기화합니다.
    private void Init()
    {
        // 힛 카운트를 0으로 초기화합니다.
        mHitCount = 0;

        // 위치를 초기화합니다.
        transform.position = mOriginPos;

        // 힛 카운트 라벨을 초기화합니다.
        mStatusLabel.text = $"힛 횟수: {mHitCount}";

        // 부가된 힘을 모두 제거합니다.
        mRigidbody.angularVelocity = mRigidbody.velocity = Vector3.zero;
    }

    // 골프공에 힘을 부가합니다.
    private void ApplyForce()
    {
        // 카메라가 바라보는 방향을 얻습니다.
        Vector3 cameraForward = mMainCamera.transform.forward + Vector3.up;

        // 힘을 계산합니다.
        Vector3 force = cameraForward * mCurrentPower;

        // 골프공에 힘을 부가합니다.
        mRigidbody.AddForce(force, ForceMode.Impulse);

        // 사운드를 재생합니다.
        SoundManager.Instance.Play("Hit Ball");
    }

    // UI에 메시지를 띄웁니다.
    private void Message(string text)
    {
        // 현재 코루틴이 실행중인경우 코루틴을 해제합니다.
        if (mCoMessage is not null)
            StopCoroutine(mCoMessage);

        // 새로운 코루틴을 시작하며 메시지를 띄웁니다.
        mCoMessage = StartCoroutine(CoMessage(text));
    }

    // UI에 메시지를 띄우며 3초후 제거합니다.
    private IEnumerator CoMessage(string text)
    {
        // 텍스트를 설정합니다.
        mMessageLabel.text = text;

        // 3초를 기다립니다.
        yield return new WaitForSeconds(3.0f);

        // 텍스트를 제거합니다.
        mMessageLabel.text = "";
    }

    // OnTriggerEnter() 함수는 이 스크립트가 부착된 게임 오브젝트가 다른 Collider와 충돌할 때 호출됩니다.
    private void OnTriggerEnter(Collider other)
    {
        // 만약 충돌한 Collider의 태그가 "Flag"인 경우에 실행합니다.
        if (other.tag == "Flag")
        {
            Message($"{mHitCount}번의 시도 끝에 골인!"); // 플레이어에게 메시지를 표시합니다.
            Init(); // 초기화 작업을 수행합니다.
            GameObject confettiGo = Instantiate(mConfettiParticlePrefab, transform.position, Quaternion.identity); // 충돌 위치에 Confetti 파티클을 생성합니다.
            confettiGo.transform.LookAt(Vector3.up); // Confetti 파티클이 위를 향하도록 방향을 조정합니다.

            SoundManager.Instance.Play("Pop"); // "Pop" 사운드를 재생합니다.
            SoundManager.Instance.Play("Confetti"); // "Confetti" 사운드를 재생합니다.
        }
    }

    // OnCollisionEnter() 함수는 이 스크립트가 부착된 게임 오브젝트가 다른 오브젝트와 충돌할 때 호출됩니다.
    private void OnCollisionEnter(Collision other)
    {
        Instantiate(mDustParticlePrefab, transform.position, Quaternion.identity); // 충돌 위치에 Dust 파티클을 생성합니다.

        // 랜덤한 확률로 다음 사운드를 재생합니다.
        if (Random.value < 0.5f)
            SoundManager.Instance.Play("Swing Bounce"); // "Swing Bounce" 사운드를 재생합니다.
        else
            SoundManager.Instance.Play("Swing Ground"); // "Swing Ground" 사운드를 재생합니다.
    }

}
