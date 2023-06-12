using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSphere : MonoBehaviour
{
    public Vector3 OriginPos {private set; get;}

    private void Awake()
    {
        OriginPos = transform.position;
    }
}
