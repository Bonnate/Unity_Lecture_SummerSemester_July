using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractiveProps : MonoBehaviour
{
    private Rigidbody mRigidbody;
    [SerializeField] private Vector3 mTargetPosition;
    [SerializeField] private float mMoveSpeed = 0.1f;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoMoveToCenter());
    }

    private IEnumerator CoMoveToCenter()
    {
        while (transform.position != mTargetPosition)
        {
            mRigidbody.AddForce((mTargetPosition - transform.position).normalized * mMoveSpeed);

            yield return null;
        }
    }

    public void Force(float power)
    {
        mRigidbody.AddForce((transform.position - mTargetPosition).normalized * power);
    }
}
