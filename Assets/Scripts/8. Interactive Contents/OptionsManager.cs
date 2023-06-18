using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OptionsManager 스크립트는 게임의 옵션 설정을 관리합니다.
public class OptionsManager : MonoBehaviour
{
    private static OptionsManager instance;
    public static OptionsManager Instance { get { return instance; } }

    [field: Header("캐릭터 설정")]
    [field: Header("캐릭터를 항상 그 자리에 고정합니다")]
    [field: SerializeField] public bool LockCharacterPosition { private set; get; } = false;
    [field: Header("캐릭터를 항상 같은 회전값으로 고정합니다")]
    [field: SerializeField] public bool LockCharacterRotation { private set; get; } = false;

    [field: Space(50)]
    [field: Header("카메라 설정")]
    [field: Header("카메라 이동 속도")]
    [field: SerializeField] public float MoveSpeed { private set; get; } = 0.01f;
    [field: Header("이동 가능한 최대 범위")]
    [field: SerializeField] public float MaxMoveRange { private set; get; } = 0.25f;

    [field: Space(50)]
    [field: Header("스플라인 설정")]
    [field: Header("스크롤 카메라 이동 속도")]
    [field: SerializeField] public float ScrollSpeed { private set; get; } = 0.1f; // 스크롤 이동 속도 조절 변수
    [field: Header("스크롤 카메라 이동 부드러움 강도")]
    [field: SerializeField] public float LerpSpeed { private set; get; } = 5.0f; // Lerp 속도 조절 변수

    [field: Space(50)]
    [field: Header("배경 설정")]
    [field: Header("HDR 강도")]
    [field: Range(1f, 10f)][field: SerializeField] public float HdrIntensity { private set; get; } = 1.0f;

    [field: Header("색상 변경 시간")]
    [field: Range(0f, 10f)][field: SerializeField] public float ColorChangeDuration { private set; get; } = 1.0f;

    private List<Transform> mCharacterTransforms = new List<Transform>();
    private List<Vector3> mCharacterOriginPositions = new List<Vector3>();
    private List<Vector3> mCharacterOriginRotations = new List<Vector3>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        // 캐릭터의 초기 위치와 회전값을 저장합니다.
        foreach (Animator animator in FindObjectsOfType<Animator>())
        {
            mCharacterTransforms.Add(animator.transform);
            mCharacterOriginPositions.Add(animator.transform.position);
            mCharacterOriginRotations.Add(animator.transform.eulerAngles);
        }
    }

    // LateUpdate는 매 프레임마다 호출되며, 캐릭터의 위치와 회전값을 업데이트합니다.
    void LateUpdate()
    {
        // 캐릭터의 위치를 고정시키는 옵션이 켜져있으면 실행됩니다.
        if (LockCharacterPosition)
            // 현재 Scene의 모든 캐릭터에 대해 하나씩 반복하여 고정합니다.
            for (int i = 0; i < mCharacterTransforms.Count; ++i)
                mCharacterTransforms[i].position = mCharacterOriginPositions[i];

        // 캐릭터의 회전을 고정시키는 옵션이 켜져있으면 실행됩니다.
        if (LockCharacterRotation)
            // 현재 Scene의 모든 캐릭터에 대해 하나씩 반복하여 고정합니다.
            for (int i = 0; i < mCharacterTransforms.Count; ++i)
                mCharacterTransforms[i].eulerAngles = mCharacterOriginRotations[i];
    }
}
