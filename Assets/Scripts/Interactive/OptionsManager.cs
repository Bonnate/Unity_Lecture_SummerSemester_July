using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        foreach (Animator animator in FindObjectsOfType<Animator>())
        {
            mCharacterTransforms.Add(animator.transform);
            mCharacterOriginPositions.Add(animator.transform.position);
            mCharacterOriginRotations.Add(animator.transform.eulerAngles);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (LockCharacterPosition)
        {
            for (int i = 0; i < mCharacterTransforms.Count; ++i)
            {
                mCharacterTransforms[i].position = mCharacterOriginPositions[i];
            }
        }

        if (LockCharacterRotation)
        {
            for (int i = 0; i < mCharacterTransforms.Count; ++i)
            {
                mCharacterTransforms[i].eulerAngles = mCharacterOriginRotations[i];
            }
        }        
    }
}
