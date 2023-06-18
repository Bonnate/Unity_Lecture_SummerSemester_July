using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Prop
{
    [field: SerializeField] public GameObject Prefab { private set; get; }
    [field: SerializeField] public int Count { private set; get; }

}

// InteractivePropsManager 스크립트는 상호작용 가능한 속성(InteractiveProps)을 관리합니다.
public class InteractivePropsManager : MonoBehaviour
{
    public static readonly float _VerticalRangeValue = 3.0f; // 상하 이동 범위
    public static readonly float _VerticalLowestValue = -1.0f; // 최하단 위치
    public static readonly float _HorizontalRangeValue = 3.0f; // 좌우 이동 범위
    public static readonly float _NearDepthRangeValue = 1.0f; // 가까운 깊이 범위
    public static readonly float _FarDepthRangeValue = 5.0f; // 먼 깊이 범위

    [field: SerializeField] public Prop[] Props { private set; get; } // 속성 배열

    private void Awake()
    {
        // Awake() 함수는 스크립트가 활성화되면서 최초로 호출되는 함수입니다.

        foreach (Prop prop in Props)
        {
            // Props 배열에서 각각의 속성(Prop)에 대해 반복합니다.

            for (int i = 0; i < prop.Count; ++i)
            {
                // 속성의 개수에 따라 해당 속성을 생성합니다.

                GameObject newProp = Instantiate(prop.Prefab, transform.position, Quaternion.identity);
                // 속성 프리팹을 인스턴스화하고 초기 위치에 생성합니다.

                newProp.transform.Translate(Random.Range(-_HorizontalRangeValue, _HorizontalRangeValue), Random.Range(_VerticalLowestValue, _VerticalRangeValue), Random.Range(_NearDepthRangeValue, _FarDepthRangeValue));
                // 랜덤한 위치를 적용하여 속성을 이동시킵니다.

                ChangeLayerRecursively(newProp.transform, "Props");
                // 속성과 하위 자식 객체의 레이어를 "Props"로 변경합니다.

                newProp.transform.tag = "InteractiveProp";
                // 속성의 태그를 "InteractiveProp"로 설정합니다.

                newProp.AddComponent<InteractiveProps>();
                // 속성에 InteractiveProps 컴포넌트를 추가합니다.
            }
        }
    }

    // 부모 Transform과 하위 자식 객체들의 레이어를 재귀적으로 변경합니다.
    void ChangeLayerRecursively(Transform parentTransform, string layerName)
    {
        parentTransform.gameObject.layer = LayerMask.NameToLayer(layerName);
        // 부모 객체의 레이어를 지정한 레이어로 변경합니다.

        foreach (Transform child in parentTransform)
        {
            ChangeLayerRecursively(child, layerName);
            // 하위 자식 객체에 대해 재귀적으로 ChangeLayerRecursively 함수를 호출하여 레이어를 변경합니다.
        }
    }
}