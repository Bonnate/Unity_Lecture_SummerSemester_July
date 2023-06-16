using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Prop
{
    [field: SerializeField] public GameObject Prefab { private set; get; }
    [field: SerializeField] public int Count { private set; get; }

}

public class InteractivePropsManager : MonoBehaviour
{
    public static readonly float _VerticalRangeValue = 3.0f;
    public static readonly float _VerticalLowestValue = -1.0f;
    public static readonly float _HorizontalRangeValue = 3.0f;
    public static readonly float _NearDepthRangeValue = 1.0f;
    public static readonly float _FarDepthRangeValue = 5.0f;

    [field: SerializeField] public Prop[] Props { private set; get; }

    private void Awake()
    {
        foreach (Prop prop in Props)
        {
            for (int i = 0; i < prop.Count; ++i)
            {
                GameObject newProp = Instantiate(prop.Prefab, transform.position, Quaternion.identity);
                newProp.transform.Translate(Random.Range(-_HorizontalRangeValue, _HorizontalRangeValue), Random.Range(_VerticalLowestValue, _VerticalRangeValue), Random.Range(_NearDepthRangeValue, _FarDepthRangeValue));

                ChangeLayerRecursively(newProp.transform, "Props");
                newProp.transform.tag = "InteractiveProp";

                newProp.AddComponent<InteractiveProps>();
            }
        }
    }

    void ChangeLayerRecursively(Transform parentTransform, string layerName)
    {
        parentTransform.gameObject.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in parentTransform)
        {
            ChangeLayerRecursively(child, layerName);
        }
    }
}
