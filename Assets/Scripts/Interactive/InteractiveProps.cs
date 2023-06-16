using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractiveProps : MonoBehaviour
{
    private Rigidbody mRigidbody;
    private Vector3 mOriginPosition;
    [SerializeField] private float mMoveSpeed = 0.1f;

    private float mHitDuration = 0f;

    private void Awake()
    {
        mHitDuration = 1.0f;

        mRigidbody = GetComponent<Rigidbody>();
        mRigidbody.useGravity = false;

        mRigidbody.angularDrag = mRigidbody.drag = 0.25f;

        if (TryGetComponent<Collider>(out _) == false)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        mOriginPosition = transform.position;

        mRigidbody.AddForce(new Vector3(1 - 2 * Random.value, 1 - 2 * Random.value, 1 - 2 * Random.value) * Random.Range(0f, 1f), ForceMode.Impulse);
        mRigidbody.AddTorque(new Vector3(1 - 2 * Random.value, 1 - 2 * Random.value, 1 - 2 * Random.value) * Random.Range(0f, 0.25f), ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoMoveToCenter());
    }

    private IEnumerator CoMoveToCenter()
    {
        while (true)
        {
            mHitDuration -= Time.deltaTime;
            if (mHitDuration > 0f)
            {
                yield return null;
                continue;
            }

            float distanceToOrigin = (mOriginPosition - transform.position).magnitude;

            if (distanceToOrigin < 0.5f)
            {
                // Calculate the deceleration factor based on the remaining distance
                float deceleration = Mathf.Clamp01(distanceToOrigin / 0.1f);

                // Gradually reduce the speed of the object
                mRigidbody.velocity *= deceleration;
            }
            else
            {
                // Apply the regular force towards the origin
                mRigidbody.AddForce((mOriginPosition - transform.position).normalized * mMoveSpeed);
            }

            yield return null;
        }
    }

    public void Force(Vector3 hitPointPos, float power)
    {
        mRigidbody.AddForce((transform.position - hitPointPos).normalized * power);
        mRigidbody.AddTorque((transform.position - hitPointPos).normalized * power * 0.1f);

        mHitDuration = 1.0f;
    }
}
