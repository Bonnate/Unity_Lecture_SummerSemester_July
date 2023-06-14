using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingSphere : MonoBehaviour
{
    public Vector3 OriginPos {private set; get;}

    private Rigidbody mRigidbody;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();

        OriginPos = transform.position;
    }

    public void MoveToOrigin()
    {
        transform.position = OriginPos;

        mRigidbody.velocity = Vector3.zero;
        mRigidbody.angularVelocity = Vector3.zero;
    }
}

